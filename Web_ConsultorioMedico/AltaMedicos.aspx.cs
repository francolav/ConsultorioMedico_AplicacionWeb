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
        private readonly UsuariosNegocio usuariosNegocio = new UsuariosNegocio();

        public bool ConfirmarEliminarMedico { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ConfirmarEliminarMedico = false;
                    Session["MedicoEncontrado"] = null;
                    PnlUsuarioMedico.Visible = true;

                    listaEspecialidades = especialidadNegocio.listar();
                    DdlEspecialidad.DataSource = listaEspecialidades;
                    DdlEspecialidad.DataTextField = "Nombre";
                    DdlEspecialidad.DataValueField = "Id";
                    DdlEspecialidad.DataBind();

                    BtnEliminar.Enabled = false;

                    if (Request.QueryString["id"] != null)
                    {
                        int id = int.Parse(Request.QueryString["id"]);
                        medicoEncontrado = mediconegocio.listar().Find(m => m.id == id);
                        if (medicoEncontrado != null)
                        {
                            cargarCampos(medicoEncontrado);
                            PnlUsuarioMedico.Visible = false;
                            Session["MedicoEncontrado"] = medicoEncontrado;
                            BtnGuardar.Text = medicoEncontrado.Eliminado ? "Reactivar y Modificar" : "Modificar";
                            BtnEliminar.Enabled = !medicoEncontrado.Eliminado;
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
            if (!validarFormulario())
                return;

            medicoEncontrado = (Medico)Session["MedicoEncontrado"];
            if (medicoEncontrado != null)
            {
                copiarCampos(medicoEncontrado);
                if (medicoEncontrado.Eliminado)
                    mediconegocio.reactivar(medicoEncontrado.id);
                mediconegocio.modificar(medicoEncontrado);
                Response.Redirect("GestionMedicos.aspx");
            }
            else
            {
                if (!validarDisponibilidadUsername())
                    return;

                int idRolMedico = usuariosNegocio.obtenerIdRolMedico();
                Medico nuevoMedico = new Medico();
                copiarCampos(nuevoMedico);
                mediconegocio.agregar(nuevoMedico);
                usuariosNegocio.altaUsuarioMedico(nuevoMedico.id, TxUsername.Text.Trim(), idRolMedico);
                Response.Redirect("GestionMedicos.aspx");
            }
        }

        private bool validarFormulario()
        {
            if (string.IsNullOrWhiteSpace(TxDniMedico.Text) || string.IsNullOrWhiteSpace(TxNombre.Text) ||
                string.IsNullOrWhiteSpace(TxApellido.Text) || string.IsNullOrWhiteSpace(TxTelefono.Text) ||
                string.IsNullOrWhiteSpace(TxMatricula.Text) || string.IsNullOrWhiteSpace(DdlEspecialidad.SelectedValue))
            {
                MostrarMensaje("Complete DNI, nombre, apellido, teléfono, matrícula y especialidad.");
                return false;
            }
            return true;
        }

        private bool validarDisponibilidadUsername()
        {
            string username = TxUsername.Text.Trim();
            if (string.IsNullOrWhiteSpace(username))
            {
                LbDisponibilidadUsername.CssClass = "form-error";
                LbDisponibilidadUsername.Text = "Ingresá un nombre de usuario.";
                return false;
            }

            if (!usuariosNegocio.usernameDisponible(username))
            {
                LbDisponibilidadUsername.CssClass = "form-error";
                LbDisponibilidadUsername.Text = "Ese nombre de usuario ya está en uso. Elegí otro.";
                return false;
            }

            LbDisponibilidadUsername.CssClass = "form-exito";
            LbDisponibilidadUsername.Text = "El nombre de usuario está disponible.";
            return true;
        }

        protected void TxUsername_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxUsername.Text))
            {
                LbDisponibilidadUsername.CssClass = "form-ayuda";
                LbDisponibilidadUsername.Text = string.Empty;
                return;
            }

            validarDisponibilidadUsername();
        }

        private void copiarCampos(Medico medico)
        {
            medico.Nombre = TxNombre.Text.Trim();
            medico.Apellido = TxApellido.Text.Trim();
            medico.Dni = TxDniMedico.Text.Trim();
            medico.Telefono = TxTelefono.Text.Trim();
            medico.TelefonoAlternativo = TxTelefonoAlternativo.Text.Trim();
            medico.Email = TxEmail.Text.Trim();
            medico.Matricula = TxMatricula.Text.Trim();
            medico.IdEspecialidades = new List<int> { int.Parse(DdlEspecialidad.SelectedValue) };
        }

        private void cargarCampos(Medico medico)
        {
            TxDniMedico.Text = medico.Dni;
            TxNombre.Text = medico.Nombre;
            TxApellido.Text = medico.Apellido;
            TxTelefono.Text = medico.Telefono;
            TxTelefonoAlternativo.Text = medico.TelefonoAlternativo;
            TxEmail.Text = medico.Email;
            TxMatricula.Text = medico.Matricula;
            if (medico.IdEspecialidades != null && medico.IdEspecialidades.Count > 0)
                DdlEspecialidad.SelectedValue = medico.IdEspecialidades[0].ToString();
        }

        private void MostrarMensaje(string mensaje)
        {
            string script = "alert(" + System.Web.HttpUtility.JavaScriptStringEncode(mensaje, true) + ");";
            ScriptManager.RegisterStartupScript(this, GetType(), "mensaje", script, true);
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
                        cargarCampos(medicoEncontrado);
                        PnlUsuarioMedico.Visible = false;
                        Session["MedicoEncontrado"] = medicoEncontrado;
                        BtnGuardar.Text = medicoEncontrado.Eliminado ? "Reactivar y Modificar" : "Modificar";
                        BtnEliminar.Enabled = !medicoEncontrado.Eliminado;
                    }
                    else
                    {
                        PnlUsuarioMedico.Visible = true;
                        TxUsername.Text = string.Empty;
                        LbDisponibilidadUsername.Text = string.Empty;
                        LbDisponibilidadUsername.CssClass = "form-ayuda";
                        TxNombre.Text = "";
                        TxApellido.Text = "";
                        TxMatricula.Text = "";
                        TxTelefono.Text = "";
                        TxTelefonoAlternativo.Text = "";
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