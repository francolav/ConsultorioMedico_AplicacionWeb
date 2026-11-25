using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Web_ConsultorioMedico;

namespace Interfaz_de_usuario
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        public void ConfigurarEncabezado(string titulo, string subtitulo, string textoSecundario = null, string textoPrimario = null)
        {
            litPageTitle.Text = titulo;
            litPaginaActual.Text = titulo; 
            litPageSubtitle.Text = subtitulo;

            if (!string.IsNullOrEmpty(textoSecundario))
            {
                litBtnSecundario.Text = textoSecundario;
                btnSecundario.Visible = true;
            }
            else
            {
                btnSecundario.Visible = false;
            }

            if (!string.IsNullOrEmpty(textoPrimario))
            {
                litBtnPrimario.Text = textoPrimario;
                btnPrimario.Visible = true;
            }
            else
            {
                btnPrimario.Visible = false;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = SeguridadWeb.UsuarioActual(Context);
            if (!SeguridadWeb.AutorizarPagina(Context, Request.AppRelativeCurrentExecutionFilePath))
                Response.Redirect(usuario == null ? "~/Login.aspx" : SeguridadWeb.PaginaInicio(usuario), true);

            string categoria = SeguridadWeb.CategoriaRol(usuario.Rol.Nombre);
            pnlAdministracionMenu.Visible = categoria == "Administracion";
            pnlMedicoMenu.Visible = categoria == "Medico";
            lblNombreUsuario.Text = usuario.Username;
            lblRolUsuario.Text = usuario.Rol.Nombre;
            litIniciales.Text = usuario.Username.Length > 1 ? usuario.Username.Substring(0, 2).ToUpperInvariant() : usuario.Username.ToUpperInvariant();
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login.aspx", true);
        }
    }
}