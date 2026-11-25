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
            if (!IsPostBack && SeguridadWeb.UsuarioActual(Context) != null)
                Response.Redirect(SeguridadWeb.PaginaInicio(SeguridadWeb.UsuarioActual(Context)), true);
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
            Response.Redirect(SeguridadWeb.PaginaInicio(usuario), true);
        }
    }
}
