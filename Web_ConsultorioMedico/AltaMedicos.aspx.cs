using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_ConsultorioMedico
{
    public partial class AltaMedicos : System.Web.UI.Page
    {
        MedicoNegocio mediconegocio = new MedicoNegocio();
        List<Medico> listaMedicos = new List<Medico>();
        EspecialidadNegocio especialidadNegocio = new EspecialidadNegocio();
        List<Especialidad> listaEspecialidades = new List<Especialidad>();

        Medico medicoEncontrado = new Medico();

        public bool ConfirmarEliminarMedico { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ConfirmarEliminarMedico = false;

                    listaEspecialidades = especialidadNegocio.listar();
                    DdlEspecialidad.DataSource = listaEspecialidades;
                    DdlEspecialidad.DataTextField = "Nombre";
                    DdlEspecialidad.DataValueField = "Id";
                    DdlEspecialidad.DataBind();

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
            medicoEncontrado = (Medico)Session["MedicoEncontrado"];
            if (medicoEncontrado != null)
            {
                mediconegocio.modificar(medicoEncontrado);
                Response.Write("<script>alert('Médico modificado correctamente');</script>");
            }
            else
            {
                Medico nuevoMedico = new Medico();
                nuevoMedico.Nombre = TxNombre.Text;
                nuevoMedico.Apellido = TxApellido.Text;
                nuevoMedico.Dni = TxDniMedico.Text;
                nuevoMedico.Telefono = TxTelefono.Text;
                nuevoMedico.TelefonoAlternativo = TxTelefonoAlternativo.Text;
                nuevoMedico.Email = TxEmail.Text;
                nuevoMedico.Matricula = TxMatricula.Text;
                //nuevoMedico.IdEspecialidades = int.Parse(DdlEspecialidad.SelectedValue);
                mediconegocio.agregar(nuevoMedico);
                Response.Write("<script>alert('Médico agregado correctamente');</script>");
            }
        }

        protected void BtnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("GestionMedicos.aspx");
        }

        protected void TxDniMedico_TextChange(object sender, EventArgs e)
        {

            if (TxDniMedico.Text != "")
            {
                try
                {

                    List<Medico> listaMedicos = new List<Medico>();
                    listaMedicos = mediconegocio.listar();
                    string dni = TxDniMedico.Text;
                    medicoEncontrado = listaMedicos.Find(m => m.Dni == dni);
                    if (medicoEncontrado != null)
                    {
                        TxNombre.Text = medicoEncontrado.Nombre;
                        TxApellido.Text = medicoEncontrado.Apellido;
                        TxTelefono.Text = medicoEncontrado.Telefono.ToString();
                        TxTelefonoAlternativo.Text = medicoEncontrado.TelefonoAlternativo.ToString();
                        TxEmail.Text = medicoEncontrado.Email;
                        TxMatricula.Text = medicoEncontrado.Matricula.ToString();
                       // DdlEspecialidad.SelectedValue = medicoEncontrado.IdEspecialidades.ToString();
                        Session["MedicoEncontrado"] = medicoEncontrado;
                        BtnGuardar.Text = "Modificar";
                        BtnEliminar.Enabled = true;
                    }
                    else
                    {
                        TxNombre.Text = "";
                        TxApellido.Text = "";
                        TxMatricula.Text = "";
                        TxTelefono.Text = "";
                        TxEmail.Text = "";
                        TxNombre.ReadOnly = false;
                        TxApellido.ReadOnly = false;
                        TxMatricula.ReadOnly = false;
                        TxTelefono.ReadOnly = false;
                        TxEmail.ReadOnly = false;
                        DdlEspecialidad.SelectedValue = "";
                        DdlEspecialidad.Enabled = true;
                        Session["MedicoEncontrado"] = null;
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
            ConfirmarEliminarMedico = true;
        }

        protected void CheckBoxConfirmarEliminar_CheckedChanged(object sender, EventArgs e)
        {
            ConfirmarEliminarMedico = CheckBoxConfirmarEliminar.Checked;
        }

        protected void BtnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            medicoEncontrado = (Medico)Session["MedicoEncontrado"];
            if (medicoEncontrado != null)
            {
                mediconegocio.eliminar(medicoEncontrado.id);
                Response.Write("<script>alert('Médico eliminado correctamente');</script>");
                TxNombre.Text = "";
                TxApellido.Text = "";
                TxMatricula.Text = "";
                TxTelefono.Text = "";
                TxEmail.Text = "";
                DdlEspecialidad.SelectedValue = "";
                Session["MedicoEncontrado"] = null;
                BtnGuardar.Text = "Guardar Nuevo";
                BtnEliminar.Enabled = false;
            }
        }
    }
}