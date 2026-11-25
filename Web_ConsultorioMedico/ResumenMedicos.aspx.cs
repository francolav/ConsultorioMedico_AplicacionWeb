using System;
using System.Collections.Generic;
using System.Linq;
using Dominio;
using Negocio;

namespace Web_ConsultorioMedico
{
    public partial class ResumenMedicos : System.Web.UI.Page
    {
        private readonly MedicoNegocio medicoNegocio = new MedicoNegocio();
        private readonly TurnoNegocio turnoNegocio = new TurnoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            ((MasterPage)Master).ConfigurarEncabezado("Resumen médico", "Resumen de tus próximos turnos");
            Usuario usuario = SeguridadWeb.UsuarioActual(Context);
            int idMedico = medicoNegocio.obtenerIdPorUsuario(usuario.IdUsuario);
            if (idMedico == 0)
            {
                lblMensaje.Text = "La cuenta no está asociada a un médico activo.";
                lblMensaje.Visible = true;
                return;
            }

            List<Turno> turnos = turnoNegocio.listarPorMedico(idMedico);
            DateTime hoy = DateTime.Today;
            lblTurnosHoy.Text = turnos.Count(t => t.Fecha.Date == hoy && t.Estado == 1).ToString();
            lblTurnosProximos.Text = turnos.Count(t => t.Fecha.Date > hoy && t.Fecha.Date <= hoy.AddDays(7) && t.Estado == 1).ToString();
        }
    }
}
