using Acceso_a_Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class TurnoNegocio
    {
        List<Turno> turnos = new List<Turno>();
        AccesoDatos datos = new AccesoDatos();
        public List<Turno> listar(int idMedico, int idEspecialidad)
        {
            try
            {
                datos.setearConsulta("select IdTurno, IdPaciente, IdMedico, idEspecialidad,Fecha, Hora, Estado from Turno where IdMedico = @idMedico  and idEspecialidad = @idEspecialidad;");
                datos.setearParametro("@idmedico", idMedico);
                datos.setearParametro("@idEspecialidad", idEspecialidad);
                datos.ejecutarConsulta();
                while (datos.Lector.Read())
                {
                    Turno aux = new Turno();
                    aux.Id = (int)datos.Lector["IdTurno"];
                    aux.IdPaciente = (int)datos.Lector["IdPaciente"];
                    aux.IdMedico = (int)datos.Lector["IdMedico"];
                    aux.IdEspecialidad = (int)datos.Lector["idEspecialidad"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.Hora = (TimeSpan)datos.Lector["Hora"];
                    aux.Estado = (int)datos.Lector["Estado"];
                    turnos.Add(aux);
                }
                return turnos;
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
            finally
            { datos.cerrarConexion(); }
        }

        public void agregar(Turno nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Turno (IdPaciente, IdMedico, IdEspecialidad, Fecha, Hora, Estado) VALUES (@idPaciente, @idMedico, @idEspecialidad, @fecha, @hora, @estado)");
                datos.setearParametro("@idPaciente", nuevo.IdPaciente);
                datos.setearParametro("@idMedico", nuevo.IdMedico);
                datos.setearParametro("@idEspecialidad", nuevo.IdEspecialidad);
                datos.setearParametro("@fecha", nuevo.Fecha.Date);
                datos.setearParametro("@hora", nuevo.Hora);
                datos.setearParametro("@estado", nuevo.Estado);
                datos.ejecutarAccion();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
