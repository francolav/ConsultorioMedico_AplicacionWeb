using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;


namespace Web_ConsultorioMedico
{
    public partial class Pacientes : System.Web.UI.Page
    {

        MedicoNegocio mediconegocio = new MedicoNegocio();
        List<Medico> listaMedicos = new List<Medico>();
        EspecialidadNegocio especialidadNegocio = new EspecialidadNegocio();
        List<Especialidad> listaEspecialidades = new List<Especialidad>();
        ObraSocialNegocio obraSocialNegocio = new ObraSocialNegocio();
        List<ObraSocial> listaObrasSociales = new List<ObraSocial>();
        Paciente pacienteEncontrado = new Paciente();
        PacienteNegocio pacientenegocio = new PacienteNegocio();

        public bool ConfirmarEliminar { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ConfirmarEliminar = false;
                    Master.ConfigurarEncabezado("Gestion Pacientes", "Alta, Baja y Modificacion Pacientes");
                    
                    listaObrasSociales = obraSocialNegocio.listar();
                    DdlObraSocial.DataSource = listaObrasSociales;
                    DdlObraSocial.DataTextField = "Nombre";
                    DdlObraSocial.DataValueField = "Id";
                    DdlObraSocial.DataBind();

                    BtnEliminar.Enabled = false;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            pacienteEncontrado = (Paciente)Session["PacienteEncontrado"];
            if (pacienteEncontrado != null)
            {
                pacientenegocio.modificar(pacienteEncontrado);
                Response.Write("<script>alert('Paciente modificado correctamente');</script>");
            }
            else
            {
                Paciente nuevoPaciente = new Paciente();
                nuevoPaciente.Nombre = TxNombre.Text;
                nuevoPaciente.Apellido = TxApellido.Text;
                nuevoPaciente.Dni = TxDniPaciente.Text;
                nuevoPaciente.Telefono = TxTelefono.Text;
                nuevoPaciente.TelefonoAlternativo = TxTelefonoAlternativo.Text;
                nuevoPaciente.Email = TxEmail.Text;
                nuevoPaciente.FechaNacimiento = DateTime.Parse(TxFechaNacimiento.Text);
                nuevoPaciente.ObraSocial.Id = int.Parse(DdlObraSocial.SelectedValue);
                pacientenegocio.agregar(nuevoPaciente);
                Response.Write("<script>alert('Paciente agregado correctamente');</script>");
            }
        }

        protected void BtnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Recepcion.aspx");
        }

        protected void TxDniPaciente_TextChange(object sender, EventArgs e)
        {

            if (TxDniPaciente.Text != "")
            {
                try
                {
                    
                    List<Paciente> listaPacientes = new List<Paciente>();
                    listaPacientes = pacientenegocio.listar();
                    string dni = TxDniPaciente.Text;
                    pacienteEncontrado = listaPacientes.Find(p => p.Dni == dni);
                    if (pacienteEncontrado != null)
                    {
                        TxNombre.Text = pacienteEncontrado.Nombre;
                        TxApellido.Text = pacienteEncontrado.Apellido;
                        TxFechaNacimiento.Text = pacienteEncontrado.FechaNacimiento.ToString("dd/MM/yyyy");
                        TxTelefono.Text = pacienteEncontrado.Telefono.ToString();
                        TxTelefonoAlternativo.Text = pacienteEncontrado.TelefonoAlternativo.ToString();
                        TxEmail.Text = pacienteEncontrado.Email;
                        DdlObraSocial.SelectedValue = (pacienteEncontrado.ObraSocial.Id.ToString() != "") ? pacienteEncontrado.ObraSocial.Id.ToString() : "";

                        Session["PacienteEncontrado"] = pacienteEncontrado;
                        BtnGuardar.Text = "Modificar";
                        BtnEliminar.Enabled = true;
                    }
                    else
                    {
                        TxNombre.Text = "";
                        TxApellido.Text = "";
                        TxFechaNacimiento.Text = "";
                        TxTelefono.Text = "";
                        TxEmail.Text = "";
                        TxNombre.ReadOnly = false;
                        TxApellido.ReadOnly = false;
                        TxFechaNacimiento.ReadOnly = false;
                        TxTelefono.ReadOnly = false;
                        TxEmail.ReadOnly = false;
                        DdlObraSocial.SelectedValue = "";
                        DdlObraSocial.Enabled = true;
                        Session["PacienteEncontrado"] = null;
                        BtnGuardar.Text = "Guardar Nuevo";
                        BtnEliminar.Enabled = false;
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            ConfirmarEliminar = true;
        }

        protected void CheckBoxConfirmarEliminar_CheckedChanged(object sender, EventArgs e)
        {
            ConfirmarEliminar = CheckBoxConfirmarEliminar.Checked;
        }

        protected void BtnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            pacienteEncontrado = (Paciente)Session["PacienteEncontrado"];
            if (pacienteEncontrado != null)
            {
                pacientenegocio.eliminar(pacienteEncontrado.Id);
                Response.Write("<script>alert('Paciente eliminado correctamente');</script>");
                TxNombre.Text = "";
                TxApellido.Text = "";
                TxFechaNacimiento.Text = "";
                TxTelefono.Text = "";
                TxEmail.Text = "";
                DdlObraSocial.SelectedValue = "";
                Session["PacienteEncontrado"] = null;
                BtnGuardar.Text = "Guardar Nuevo";
                BtnEliminar.Enabled = false;
            }
        }

    }
}

