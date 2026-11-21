using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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

        }
    }
}