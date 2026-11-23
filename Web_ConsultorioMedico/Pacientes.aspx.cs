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
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                  
                    Master.ConfigurarEncabezado("Gestion Pacientes", "Alta, Baja y Modificacion Pacientes");

                    listaObrasSociales = obraSocialNegocio.listar();
                    DdlObraSocial.DataSource = listaObrasSociales;
                    DdlObraSocial.DataTextField = "Nombre";
                    DdlObraSocial.DataValueField = "Id";
                    DdlObraSocial.DataBind();

                    if (Request.QueryString["id"] != null)
                    {
                        List<Paciente> listaPacientes = new List<Paciente>();
                        listaPacientes = pacientenegocio.listarActivos();
                        int id = int.Parse(Request.QueryString["id"]);
                        pacienteEncontrado = listaPacientes.Find(p => p.Id == id);
                        if (pacienteEncontrado != null)
                        {
                            cargarCampos(pacienteEncontrado);
                            Session["PacienteEncontrado"] = pacienteEncontrado;
                            BtnGuardar.Text = "Modificar";
                            
                        }
                    }

                    

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
                if (pacienteEncontrado.eliminado)
                {
                    pacientenegocio.reactivar(pacienteEncontrado.Id);
                }
                pacientenegocio.modificar(pacienteEncontrado);
                Response.Write("<script>alert('Paciente modificado correctamente');</script>");
                Response.Redirect("PacientesLista.aspx");
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
                Response.Redirect("PacientesLista.aspx");
            }
        }

        protected void BtnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("PacientesLista.aspx");
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
                        if (pacienteEncontrado.eliminado)
                        {
                            cargarCampos(pacienteEncontrado);
                            Session["PacienteEncontrado"] = pacienteEncontrado;
                            BtnGuardar.Text = "Reactivar y Modificar";
                            lbAvisoInactivo.Text = "Paciente: " + pacienteEncontrado.nombreCompleto + ". Se encontrava inactivo, al guardarlo nuevamente se reactivara";

                        } else
                        {
                            cargarCampos(pacienteEncontrado);
                            Session["PacienteEncontrado"] = pacienteEncontrado;
                            BtnGuardar.Text = "Modificar";

                        }
                       
                    }
                    else
                    {
                        limpiarCampos();
                        Session["PacienteEncontrado"] = null;
                        BtnGuardar.Text = "Guardar Nuevo";
                        
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        
        protected void limpiarCampos()
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

        }

        protected void cargarCampos(Paciente p)
        {
            TxNombre.Text = p.Nombre;
            TxApellido.Text = p.Apellido;
            TxFechaNacimiento.Text = p.FechaNacimiento.ToString("dd/MM/yyyy");
            TxTelefono.Text = p.Telefono.ToString();
            TxTelefonoAlternativo.Text = p.TelefonoAlternativo.ToString();
            TxEmail.Text = p.Email;
            DdlObraSocial.SelectedValue = (p.ObraSocial.Id.ToString() != "") ? p.ObraSocial.Id.ToString() : "";

        }
    }
}

