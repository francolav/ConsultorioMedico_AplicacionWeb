using Negocio;
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
        List<Dominio.Paciente> listaPacientes = new List<Dominio.Paciente>();


        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                listaPacientes = pacienteNegocio.listar();
                gvListadoPaciente.DataSource = listaPacientes;
                gvListadoPaciente.DataBind();
            }
        }

        
    }

}