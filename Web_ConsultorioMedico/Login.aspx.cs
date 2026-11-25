using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;
using Web_ConsultorioMedico;

namespace Interfaz_de_usuario
{
    public partial class Login : System.Web.UI.Page
    {
        private readonly UsuariosNegocio usuariosNegocio = new UsuariosNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Usuario usuarioActual = SeguridadWeb.UsuarioActual(Context);
                if (usuarioActual != null)
                    Response.Redirect(usuarioActual.requiereCambioPassword ? "~/CambiarPassword.aspx" : SeguridadWeb.PaginaInicio(usuarioActual), true);
            }

            pnlPrimerAcceso.Visible = ViewState["MostrarPrimerAcceso"] != null && (bool)ViewState["MostrarPrimerAcceso"];
            pnlRecuperarClave.Visible = ViewState["MostrarRecuperarClave"] != null && (bool)ViewState["MostrarRecuperarClave"];
        }

        protected void btnMostrarRecuperarClave_Click(object sender, EventArgs e)
        {
            ViewState["MostrarRecuperarClave"] = true;
            pnlRecuperarClave.Visible = true;
        }

        protected void btnMostrarPrimerAcceso_Click(object sender, EventArgs e)
        {
            ViewState["MostrarPrimerAcceso"] = true;
            pnlPrimerAcceso.Visible = true;
        }

        protected void btnCompletarPrimerAcceso_Click(object sender, EventArgs e)
        {
            ViewState["MostrarPrimerAcceso"] = true;
            pnlPrimerAcceso.Visible = true;
            lblPrimerAccesoMensaje.Visible = true;
            lblPrimerAccesoMensaje.CssClass = "login-error";

            string nuevaPassword = txtPrimerAccesoPassword.Text;
            if (string.IsNullOrWhiteSpace(txtPrimerAccesoUsuario.Text) || string.IsNullOrEmpty(txtClaveTemporal.Text))
            {
                lblPrimerAccesoMensaje.Text = "Ingresá tu usuario y contraseña temporal.";
                return;
            }

            if (nuevaPassword.Length < 8)
            {
                lblPrimerAccesoMensaje.Text = "La nueva contraseña debe tener al menos 8 caracteres.";
                return;
            }

            if (!string.Equals(nuevaPassword, txtPrimerAccesoConfirmacion.Text, StringComparison.Ordinal))
            {
                lblPrimerAccesoMensaje.Text = "Las nuevas contraseñas no coinciden.";
                return;
            }

            if (!usuariosNegocio.completarPrimerAcceso(txtPrimerAccesoUsuario.Text.Trim(), txtClaveTemporal.Text, nuevaPassword))
            {
                lblPrimerAccesoMensaje.Text = "No se pudo validar la cuenta o la contraseña temporal. Verificá los datos o contactá a Administración.";
                return;
            }

            txtUsuario.Text = txtPrimerAccesoUsuario.Text.Trim();
            txtPassword.Text = string.Empty;
            lblPrimerAccesoMensaje.CssClass = "login-success";
            lblPrimerAccesoMensaje.Text = "Contraseña actualizada. Ya podés iniciar sesión con tu nueva contraseña.";
            txtClaveTemporal.Text = string.Empty;
            txtPrimerAccesoPassword.Text = string.Empty;
            txtPrimerAccesoConfirmacion.Text = string.Empty;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Usuario usuario = usuariosNegocio.autenticar(txtUsuario.Text, txtPassword.Text);
            string categoriaUsuario = usuario == null || usuario.Rol == null
                ? string.Empty
                : SeguridadWeb.CategoriaRol(usuario.Rol.Nombre);
            string categoriaSeleccionada = SeguridadWeb.CategoriaRol(hfRolSeleccionado.Value);

            if (usuario == null || string.IsNullOrEmpty(categoriaUsuario) || categoriaUsuario != categoriaSeleccionada)
            {
                lblError.Text = "Usuario, contraseña o categoría incorrectos.";
                lblError.Visible = true;
                return;
            }

            Session.Clear();
            Session[SeguridadWeb.SessionUsuario] = usuario;
            if (usuario.requiereCambioPassword)
                Response.Redirect("~/CambiarPassword.aspx", true);

            Response.Redirect(SeguridadWeb.PaginaInicio(usuario), true);
        }
    }
}
