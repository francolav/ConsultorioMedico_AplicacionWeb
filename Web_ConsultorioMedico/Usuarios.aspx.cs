using System;
using System.Collections.Generic;
using Dominio;
using Negocio;

namespace Web_ConsultorioMedico
{
    public partial class Usuarios : System.Web.UI.Page
    {
        private readonly UsuariosNegocio usuariosNegocio = new UsuariosNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((MasterPage)Master).ConfigurarEncabezado("Usuarios", "Restablecimiento de contraseñas");
                cargarUsuarios();
            }
        }

        private void cargarUsuarios()
        {
            List<Usuario> usuarios = usuariosNegocio.listar();
            gvUsuarios.DataSource = usuarios;
            gvUsuarios.DataBind();
        }

        protected void gvUsuarios_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Restablecer")
                return;

            int idUsuario;
            if (!int.TryParse(Convert.ToString(e.CommandArgument), out idUsuario))
                throw new InvalidOperationException("El identificador del usuario no es válido.");

            usuariosNegocio.restablecerPassword(idUsuario);
            lblResultado.Text = "Contraseña restablecida a 1234. El usuario deberá cambiarla al iniciar sesión.";
            lblResultado.Visible = true;
            cargarUsuarios();
        }
    }
}
