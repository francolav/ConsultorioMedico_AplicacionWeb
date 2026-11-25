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
                datos.setearConsulta("select IdAgendaMedico, DiaAtencion, HoraInicio, HoraFin from AgendaMedico where IdMedico = @idMedico and IdEspecialidad = @idEspecialidad and Eliminado=0\r\n");
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
    }
}
