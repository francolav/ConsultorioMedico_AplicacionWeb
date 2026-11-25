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
    public partial class AltaTurno : System.Web.UI.Page
    {
        MedicoNegocio mediconegocio = new MedicoNegocio();
        List<Medico> listaMedicos = new List<Medico>();
        EspecialidadNegocio especialidadNegocio = new EspecialidadNegocio();
        List<Especialidad> listaEspecialidades = new List<Especialidad>();
        ObraSocialNegocio obraSocialNegocio = new ObraSocialNegocio();
        List<ObraSocial> listaObrasSociales = new List<ObraSocial>();
        AgendaMedicoNegocio agendaNegocio = new AgendaMedicoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.ConfigurarEncabezado("Gestion de Turnos", "Alta Nuevo Turno");

                listaEspecialidades = especialidadNegocio.listar();
                DdlEspecialidad.DataSource = listaEspecialidades;
                DdlEspecialidad.DataTextField = "Nombre";
                DdlEspecialidad.DataValueField = "Id";
                DdlEspecialidad.DataBind();

                listaObrasSociales = obraSocialNegocio.listar();
                DdlObraSocial.DataSource = listaObrasSociales;
                DdlObraSocial.DataTextField = "Nombre";
                DdlObraSocial.DataValueField = "Id";
                DdlObraSocial.DataBind();

            }
        }


        protected void BtnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Recepcion.aspx");
        }

        protected void TxDniPaciente_TextChange(object sender, EventArgs e)
        {

            if (TxDniPaciente.Text != "")
            {
                try
                {
                    PacienteNegocio pacientenegocio = new PacienteNegocio();
                    List<Paciente> listaPacientes = new List<Paciente>();
                    listaPacientes = pacientenegocio.listar();
                    string dni = TxDniPaciente.Text;
                    Paciente pacienteEncontrado = listaPacientes.Find(p => p.Dni == dni);
                    if (pacienteEncontrado != null)
                    {
                        TxNombre.Text = pacienteEncontrado.Nombre;
                        TxNombre.ReadOnly = true;
                        TxApellido.Text = pacienteEncontrado.Apellido;
                        TxApellido.ReadOnly = true;
                        TxFechaNacimiento.Text = pacienteEncontrado.FechaNacimiento.ToString("dd/MM/yyyy");
                        TxFechaNacimiento.ReadOnly = true;
                        TxTelefono.Text = pacienteEncontrado.Telefono.ToString();
                        TxTelefono.ReadOnly = true;
                        TxTelefonoAlternativo.Text = pacienteEncontrado.TelefonoAlternativo.ToString();
                        TxTelefonoAlternativo.ReadOnly = true;
                        TxEmail.Text = pacienteEncontrado.Email;
                        TxEmail.ReadOnly = true;
                        DdlObraSocial.SelectedValue = (pacienteEncontrado.ObraSocial.Id.ToString() != "") ? pacienteEncontrado.ObraSocial.Id.ToString() : "";
                        DdlObraSocial.Enabled = false;
                    }
                    else
                    {
                        TxNombre.Text = "";
                        TxApellido.Text = "";
                        TxFechaNacimiento.Text = "";
                        TxTelefono.Text = "";
                        TxEmail.Text = "";
                        TxNombre.ReadOnly = false;
                        TxApellido.ReadOnly = false;
                        TxFechaNacimiento.ReadOnly = false;
                        TxTelefono.ReadOnly = false;
                        TxEmail.ReadOnly = false;
                        DdlObraSocial.SelectedValue = "";
                        DdlObraSocial.Enabled = true;
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        protected void DdlEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            DdlMedico.Items.Clear();
            DdlMedico.Items.Add(new ListItem("Seleccione.....", ""));
            TxSeleccionarFecha.Text = "";
            hfDiasHabilitados.Value = "[]";

            if (DdlEspecialidad.SelectedValue != "")
            {

                int idEspecialidad = int.Parse(DdlEspecialidad.SelectedValue);
                listaMedicos = mediconegocio.listar();
                List<Medico> medicosFiltrados = listaMedicos.FindAll(m => m.IdEspecialidades.Contains(idEspecialidad));
                DdlMedico.DataSource = medicosFiltrados;
                DdlMedico.DataTextField = "NombreCompleto";
                DdlMedico.DataValueField = "Id";
                DdlMedico.DataBind();
                DdlMedico.Items.Insert(0, new ListItem("Seleccione.....", ""));
            }
            else
            {
                DdlMedico.Items.Clear();

            }


        }

        protected void DdlMedico_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxSeleccionarFecha.Text = "";
            hfDiasHabilitados.Value = "[]";

            if (string.IsNullOrEmpty(DdlMedico.SelectedValue)) return;


            int idMedico = int.Parse(DdlMedico.SelectedValue);
            int idEspecialidad = int.Parse(DdlEspecialidad.SelectedValue);
            List<AgendaMedico> agenda = agendaNegocio.cargarAgenda(idMedico, idEspecialidad);


            List<int> diasHabilitados = agenda.Select(a => a.Dia).Distinct().ToList();
            // No deja elegir fechas pasadas
            TxSeleccionarFecha.Attributes["min"] = DateTime.Today.ToString("yyyy-MM-dd");

            hfDiasHabilitados.Value = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(diasHabilitados);

            //guardo la agenda, para evaluar horarios..
            Session["Agenda"] = agenda;



        }
    }
}