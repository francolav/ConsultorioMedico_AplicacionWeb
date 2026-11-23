using Negocio;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_ConsultorioMedico
{
    public partial class PacientesLista : System.Web.UI.Page
    {
        PacienteNegocio pacienteNegocio = new PacienteNegocio();
        List<Paciente> listaPacientes = new List<Paciente>();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarActivos();
                gvPacientesInactivos.Visible = false;
                Master.ConfigurarEncabezado("Pacientes", "Listado Pacientes");
            }
        }

        private void cargarActivos()
        {
            List<Paciente> listaPacientes = pacienteNegocio.listarActivos();
            gvListadoPaciente.DataSource = listaPacientes;
            gvListadoPaciente.DataBind();
        }

        private void cargarInactivos()
        {
            
            List<Paciente> listaPacientes = pacienteNegocio.listarInactivos();
            gvPacientesInactivos.DataSource = listaPacientes;
            gvPacientesInactivos.DataBind();
        }

        protected void gvListadoPaciente_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvListadoPaciente.PageIndex = e.NewPageIndex;
            cargarActivos();
        }


        protected void gvListadoPaciente_SelectedIndexChanged(object sender, EventArgs e)
        {
            string idPaciente = gvListadoPaciente.SelectedDataKey.Value.ToString();
            Response.Redirect("Pacientes.aspx?id=" + idPaciente);
        }


        protected void gvListadoPaciente_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int idPaciente = int.Parse(gvListadoPaciente.DataKeys[e.RowIndex].Value.ToString());

            pacienteNegocio.eliminar(idPaciente);
            Response.Redirect("PacientesLista.aspx");

        }

        protected void gvPacientesInactivos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idPaciente = int.Parse(gvPacientesInactivos.SelectedDataKey.Value.ToString());
            pacienteNegocio.reactivar(idPaciente);
            Response.Redirect("PacientesLista.aspx");

        }

        protected void gvPacientesInactivos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvListadoPaciente.PageIndex = e.NewPageIndex;
            cargarInactivos();
            
        }

        protected void btnMostrar_Click(object sender, EventArgs e)
        {
            if (gvPacientesInactivos.Visible)
            {
                gvPacientesInactivos.Visible = false;


            }
            else
            {
                gvPacientesInactivos.Visible = true;
                cargarInactivos();
                
            }
        }
    }
}

