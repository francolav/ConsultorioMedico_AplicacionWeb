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
    public partial class Recepcion : System.Web.UI.Page
    {
        List<Medico> listaMedicos = new List<Medico>();

      

        protected void Page_Load(object sender, EventArgs e)
        {
            MedicoNegocio negocio = new MedicoNegocio();

            List<Medico> listaMedicos = negocio.listar();
            GvMedicosTurno.DataSource = listaMedicos;
            GvMedicosTurno.DataBind();

            

            if (!IsPostBack)
                PnConsultarTurno.Style["display"] = "none";
        }

        protected void BtnNuevoTurno_Click(object sender, EventArgs e)
        {
            Response.Redirect("GestionTurno.aspx");
        }
        protected void BtnConsultarTurnos_Click(object sender, EventArgs e)
        {

            if (PnConsultarTurno.Style["display"] == "block")
            {
                PnConsultarTurno.Style["display"] = "none";
            }
            else
            {
                PnConsultarTurno.Style["display"] = "block";
            }

        }
    }
}