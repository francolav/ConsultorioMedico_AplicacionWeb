using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acceso_a_Datos;

namespace Negocio
{
    public class AgendaMedicoNegocio
    {
        List<AgendaMedico> agendaMedicos = new List<AgendaMedico>();
        AccesoDatos datos = new AccesoDatos();
        public List<AgendaMedico> cargarAgenda(int idMedico, int idEspecialidad)
        {
            try
            {
                datos.setearConsulta("select IdAgendaMedico, DiaAtencion, HoraInicio, HoraFin, DuracionTurnos from AgendaMedico where IdMedico = @idMedico and IdEspecialidad = @idEspecialidad and Eliminado=0\r\n");
                datos.setearParametro("@idmedico", idMedico);
                datos.setearParametro("@idEspecialidad", idEspecialidad);
                datos.ejecutarConsulta();
                while (datos.Lector.Read())
                {
                    AgendaMedico aux = new AgendaMedico();
                    aux.IdAgenda = (int)datos.Lector["IdAgendaMedico"];
                    aux.Dia = (int)datos.Lector["DiaAtencion"];
                    aux.HoraInicio = (TimeSpan)datos.Lector["HoraInicio"];
                    aux.HoraFin = (TimeSpan)datos.Lector["HoraFin"];
                    aux.IdEspecialidad = idEspecialidad;
                    aux.IdMedico = idMedico;
                    aux.DuracionTurno = (TimeSpan)datos.Lector["DuracionTurnos"];
                    agendaMedicos.Add(aux);
                }
                return agendaMedicos;
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
            finally
            { datos.cerrarConexion(); }
        }
        public List<Turno> listarFecha(int idMedico, int idEspecialidad, DateTime fecha )
        {
            List<Turno> turnos = new List<Turno>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT IdTurno, IdPaciente, IdMedico, IdEspecialidad, Fecha, Hora, Estado FROM Turno WHERE IdMedico = @idMedico AND IdEspecialidad = @idEspecialidad AND Estado = 1 AND CAST(Fecha AS DATE) = @fecha");
                datos.setearParametro("@idMedico", idMedico);
                datos.setearParametro("@idEspecialidad", idEspecialidad);
                datos.setearParametro("@fecha", fecha.Date);
                datos.ejecutarConsulta();
                while (datos.Lector.Read())
                {
                    Turno aux = new Turno();
                    aux.Id = (int)datos.Lector["IdTurno"];
                    aux.IdPaciente = (int)datos.Lector["IdPaciente"];
                    aux.IdMedico = (int)datos.Lector["IdMedico"];
                    aux.IdEspecialidad = (int)datos.Lector["IdEspecialidad"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.Hora = (TimeSpan)datos.Lector["Hora"];
                    aux.Estado = (int)datos.Lector["Estado"];
                    turnos.Add(aux);
                }
                return turnos;
            }
            finally { datos.cerrarConexion(); }
        }
    }
}
