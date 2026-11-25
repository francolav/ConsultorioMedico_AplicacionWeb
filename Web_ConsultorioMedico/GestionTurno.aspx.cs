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
        TurnoNegocio turnoNegocio = new TurnoNegocio();
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
                        Session["pacienteEncontrado"] = pacienteEncontrado;
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
                        Session["pacienteEncontrado"] = null;
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

        protected void TxSeleccionarFecha_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DdlHorario.Items.Clear();
                DdlHorario.Items.Add(new ListItem("Seleccione horario...", ""));

                if (string.IsNullOrEmpty(TxSeleccionarFecha.Text)
                    || Session["Agenda"] == null
                    || string.IsNullOrEmpty(DdlMedico.SelectedValue)) return;

                DateTime fechaSeleccionada = DateTime.ParseExact(TxSeleccionarFecha.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                int idMedico = int.Parse(DdlMedico.SelectedValue);
                int diaSemana = (int)fechaSeleccionada.DayOfWeek; // 0=Domingo ... 6=Sábado
                

                List<AgendaMedico> agenda = (List<AgendaMedico>)Session["Agenda"];
                AgendaMedico agendaDelDia = agenda.FirstOrDefault(a => a.Dia == diaSemana);
                if (agendaDelDia == null) return;
                // 1. Traemos todos los turnos y nos quedamos solo con los ocupados de ese médico ese día
                List<Turno> todosLosTurnos = turnoNegocio.listar(agendaDelDia.IdMedico, agendaDelDia.IdEspecialidad);
                List<TimeSpan> horariosOcupados = todosLosTurnos
                    .Where(t => t.IdMedico == idMedico
                             && t.Fecha.Date == fechaSeleccionada.Date
                             && t.Estado == 1) // solo los activos bloquean
                    .Select(t => t.Hora)
                    .ToList();

                // 2. Generamos los horarios de la agenda y saltamos los ocupados
                TimeSpan horaActual = agendaDelDia.HoraInicio;
                while (horaActual < agendaDelDia.HoraFin)
                {
                    // Si esa hora ya está ocupada, no la mostramos
                    if (!horariosOcupados.Contains(horaActual))
                    {
                        string horaTexto = horaActual.ToString(@"hh\:mm");
                        DdlHorario.Items.Add(new ListItem(horaTexto, horaTexto));
                    }
                    horaActual = horaActual.Add(agendaDelDia.DuracionTurno);
                }

                if (DdlHorario.Items.Count == 0)
                {
                    DdlHorario.Items.Clear();
                    DdlHorario.Items.Add(new ListItem("No hay horarios libres ese día", ""));
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }

        protected void BtnGuardarTurno_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. VALIDACIONES: si falta algo, no seguimos
                if (Session["pacienteEncontrado"] == null)
                {
                    Response.Write("<script>alert('Debe Seleccionar un paciente primero....');</script>");
                    return;
                }
                if (string.IsNullOrEmpty(DdlMedico.SelectedValue) || string.IsNullOrEmpty(DdlEspecialidad.SelectedValue))
                {
                    Response.Write("<script>alert('Seleccioná médico y especialidad....');</script>");
                    
                    return;
                }
                if (string.IsNullOrEmpty(TxSeleccionarFecha.Text) || string.IsNullOrEmpty(DdlHorario.SelectedValue))
                {
                    Response.Write("<script>alert('Seleccioná fecha y horario....');</script>");
                    
                    return;
                }

                Paciente paciente = (Paciente)Session["pacienteEncontrado"];
                DateTime fecha = DateTime.ParseExact(TxSeleccionarFecha.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                TimeSpan hora = TimeSpan.Parse(DdlHorario.SelectedValue);

                Turno nuevo = new Turno();
                nuevo.IdPaciente = paciente.Id;
                nuevo.IdMedico = int.Parse(DdlMedico.SelectedValue);
                nuevo.IdEspecialidad = int.Parse(DdlEspecialidad.SelectedValue);
                nuevo.Fecha = fecha;
                nuevo.Hora = hora; 
                nuevo.Estado = 1; 

                turnoNegocio.agregar(nuevo);

                Response.Write($"<script>alert('¡Turno guardado! Paciente: {paciente.Nombre} - {fecha.ToShortDateString()} a las {hora:hh\\:mm}');</script>");


            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}