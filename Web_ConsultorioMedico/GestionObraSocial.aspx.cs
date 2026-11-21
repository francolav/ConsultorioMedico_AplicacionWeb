using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace Web_ConsultorioMedico
{
    public partial class GestionObraSocial : System.Web.UI.Page
    {

        ObraSocialNegocio obraSocialNegocio = new ObraSocialNegocio();
        List<ObraSocial> listaObrasSociales = new List<ObraSocial>();

        public bool agregar { get; set; }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    agregar = false;
                    listaObrasSociales = obraSocialNegocio.listar();
                    GvObrasSociales.DataSource = listaObrasSociales;
                    GvObrasSociales.DataBind();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            agregar = true;
        }
        protected void BtnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                agregar = ChkConfirmacion.Checked;
                if (agregar)
                {

                    ObraSocial nuevaObraSocial = new ObraSocial();
                    nuevaObraSocial.Nombre = TxtNombre.Text;
                    obraSocialNegocio.agregar(nuevaObraSocial);
                    Response.Write("<script>alert('Obra Social agregada correctamente.');</script>");

                    Response.Redirect("GestionObraSocial.aspx");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            Button btnEliminar = (Button)sender;
            int idObraSocial = int.Parse(btnEliminar.CommandArgument);
            try
            {
                obraSocialNegocio.eliminar(idObraSocial);
                Response.Write("<script>alert('Obra Social eliminada correctamente.');</script>");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Response.Redirect("GestionObraSocial.aspx");
            }
        }

    }
}