using System;
using System.Collections.Generic;
using Dominio;
using Negocio;

namespace Web_ConsultorioMedico
{
    public partial class Agenda : System.Web.UI.Page
    {
        private readonly MedicoNegocio medicoNegocio = new MedicoNegocio();
        private readonly TurnoNegocio turnoNegocio = new TurnoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            ((MasterPage)Master).ConfigurarEncabezado("Agenda", "Tus turnos asignados");
            Usuario usuario = SeguridadWeb.UsuarioActual(Context);
            int idMedico = medicoNegocio.obtenerIdPorUsuario(usuario.IdUsuario);
            if (idMedico == 0)
            {
                lblMensaje.Text = "La cuenta no está asociada a un médico activo.";
                lblMensaje.Visible = true;
                return;
            }

            List<Turno> turnos = turnoNegocio.listarPorMedico(idMedico);
            gvAgenda.DataSource = turnos;
            gvAgenda.DataBind();
        }
    }
}
