using System;
using Dominio;
using Negocio;
using Web_ConsultorioMedico;

namespace Interfaz_de_usuario
{
    public partial class CambiarPassword : System.Web.UI.Page
    {
        private readonly UsuariosNegocio usuariosNegocio = new UsuariosNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = SeguridadWeb.UsuarioActual(Context);
            if (usuario == null)
            {
                Response.Redirect("~/Login.aspx", true);
                return;
            }

            if (!usuario.requiereCambioPassword)
                Response.Redirect(SeguridadWeb.PaginaInicio(usuario), true);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Usuario usuario = SeguridadWeb.UsuarioActual(Context);
            if (usuario == null || !usuario.requiereCambioPassword)
            {
                Response.Redirect("~/Login.aspx", true);
                return;
            }

            string nuevaPassword = txtNuevaPassword.Text;
            if (nuevaPassword.Length < 8)
            {
                mostrarError("La contraseña debe tener al menos 8 caracteres.");
                return;
            }

            if (!string.Equals(nuevaPassword, txtConfirmarPassword.Text, StringComparison.Ordinal))
            {
                mostrarError("Las contraseñas no coinciden.");
                return;
            }

            usuariosNegocio.cambiarPasswordTemporal(usuario.IdUsuario, nuevaPassword);
            usuario.requiereCambioPassword = false;
            Session[SeguridadWeb.SessionUsuario] = usuario;
            Response.Redirect(SeguridadWeb.PaginaInicio(usuario), true);
        }

        private void mostrarError(string mensaje)
        {
            lblError.Text = mensaje;
            lblError.Visible = true;
        }
    }
}
