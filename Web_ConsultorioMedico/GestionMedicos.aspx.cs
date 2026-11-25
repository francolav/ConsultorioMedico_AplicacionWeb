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
    public partial class GestionMedicos : System.Web.UI.Page
    {
        MedicoNegocio medicoNegocio = new MedicoNegocio();
        List<Medico> listaMedicos = new List<Medico>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((MasterPage)Master).ConfigurarEncabezado("Médicos", "Alta, baja y modificación de médicos");
                cargarMedicos();
            }
        }

        private void cargarMedicos()
        {
            listaMedicos = medicoNegocio.listarActivos();
            GvMedicos.DataSource = listaMedicos;
            GvMedicos.DataBind();
        }

        protected void GvMedicos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = int.Parse(GvMedicos.SelectedDataKey.Value.ToString());
            Response.Redirect("AltaMedicos.aspx?id=" + id);
        }

        protected void GvMedicos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = int.Parse(GvMedicos.DataKeys[e.RowIndex].Value.ToString());
            medicoNegocio.eliminar(id);
            cargarMedicos();
        }

        protected void GvMedicos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvMedicos.PageIndex = e.NewPageIndex;
            cargarMedicos();
        }

        protected void btnAgregarMedico_Click(object sender, EventArgs e)
        {
            Response.Redirect("AltaMedicos.aspx");
        }
    }
}