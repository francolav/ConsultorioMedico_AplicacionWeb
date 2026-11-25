using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acceso_a_Datos;
using Dominio;

namespace Negocio
{
    public class MedicoNegocio
    {
        public List<Medico> listar()
        {
            return listar(null);
        }

        public List<Medico> listarActivos()
        {
            return listar(false);
        }

        public List<Medico> listarInactivos()
        {
            return listar(true);
        }

        private List<Medico> listar(bool? eliminado)
        {
            List<Medico> lista = new List<Medico>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT m.IdMedico, m.Apellido, m.Nombre, m.Matricula, m.Dni, m.Telefono, m.TelefonoAlternativo, m.Email, m.Eliminado, es.IdEspecialidad " +
                    "FROM Medico m LEFT JOIN Especialidades ee ON ee.IdMedico = m.IdMedico " +
                    "LEFT JOIN Especialidad es ON es.IdEspecialidad = ee.IdEspecialidad ";

                if (eliminado.HasValue)
                    consulta += "WHERE m.Eliminado = @Eliminado ";

                consulta += "ORDER BY m.Apellido, m.Nombre";
                datos.setearConsulta(consulta);
                if (eliminado.HasValue)
                    datos.setearParametro("@Eliminado", eliminado.Value);

                datos.ejecutarConsulta();
                while (datos.Lector.Read())
                {
                    int id = (int)datos.Lector["IdMedico"];
                    Medico aux = lista.Find(m => m.id == id);
                    if (aux == null)
                    {
                        aux = new Medico();
                        aux.id = id;
                        aux.Nombre = (string)datos.Lector["Nombre"];
                        aux.Apellido = (string)datos.Lector["Apellido"];
                        aux.Dni = (string)datos.Lector["Dni"];
                        aux.Matricula = datos.Lector["Matricula"] != DBNull.Value ? (string)datos.Lector["Matricula"] : "";
                        aux.Telefono = (string)datos.Lector["Telefono"];
                        aux.TelefonoAlternativo = datos.Lector["TelefonoAlternativo"] != DBNull.Value ? (string)datos.Lector["TelefonoAlternativo"] : "";
                        aux.Email = datos.Lector["Email"] != DBNull.Value ? (string)datos.Lector["Email"] : "";
                        aux.Eliminado = datos.Lector["Eliminado"] != DBNull.Value && (bool)datos.Lector["Eliminado"];
                        lista.Add(aux);
                    }

                    if (datos.Lector["IdEspecialidad"] != DBNull.Value)
                    {
                        int idEspecialidad = (int)datos.Lector["IdEspecialidad"];
                        if (!aux.IdEspecialidades.Contains(idEspecialidad))
                            aux.IdEspecialidades.Add(idEspecialidad);
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void agregar(Medico nuevo)
        {
            AccesoDatos datosMedico = new AccesoDatos();
            try
            {
                datosMedico.setearConsulta("INSERT INTO Medico (Nombre, Apellido, Matricula, Dni, Telefono, TelefonoAlternativo, Email, Eliminado) VALUES (@Nombre, @Apellido, @Matricula, @Dni, @Telefono, @TelefonoAlternativo, @Email, 0); SELECT CAST(SCOPE_IDENTITY() AS int);");
                datosMedico.setearParametro("@Nombre", nuevo.Nombre);
                datosMedico.setearParametro("@Apellido", nuevo.Apellido);
                datosMedico.setearParametro("@Matricula", nuevo.Matricula);
                datosMedico.setearParametro("@Dni", nuevo.Dni);
                datosMedico.setearParametro("@Telefono", nuevo.Telefono);
                datosMedico.setearParametro("@TelefonoAlternativo", nuevo.TelefonoAlternativo);
                datosMedico.setearParametro("@Email", nuevo.Email);
                object resultado = datosMedico.ejecutarScalar();
                nuevo.id = Convert.ToInt32(resultado);

                guardarEspecialidades(nuevo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datosMedico.cerrarConexion();
            }
        }

        public void modificar(Medico medico)
        {
            AccesoDatos datosMedico = new AccesoDatos();
            try
            {
                datosMedico.setearConsulta("UPDATE Medico SET Nombre = @Nombre, Apellido = @Apellido, Matricula = @Matricula, Dni = @Dni, Telefono = @Telefono, TelefonoAlternativo = @TelefonoAlternativo, Email = @Email WHERE IdMedico = @Id");
                datosMedico.setearParametro("@Nombre", medico.Nombre);
                datosMedico.setearParametro("@Apellido", medico.Apellido);
                datosMedico.setearParametro("@Matricula", medico.Matricula);
                datosMedico.setearParametro("@Dni", medico.Dni);
                datosMedico.setearParametro("@Telefono", medico.Telefono);
                datosMedico.setearParametro("@TelefonoAlternativo", medico.TelefonoAlternativo);
                datosMedico.setearParametro("@Email", medico.Email);
                datosMedico.setearParametro("@Id", medico.id);
                datosMedico.ejecutarAccion();

                guardarEspecialidades(medico);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datosMedico.cerrarConexion();
            }
        }

        private void guardarEspecialidades(Medico medico)
        {
            AccesoDatos datosEspecialidades = new AccesoDatos();
            try
            {
                datosEspecialidades.setearConsulta("DELETE FROM Especialidades WHERE IdMedico = @IdMedico");
                datosEspecialidades.setearParametro("@IdMedico", medico.id);
                datosEspecialidades.ejecutarAccion();

                foreach (int idEspecialidad in medico.IdEspecialidades)
                {
                    datosEspecialidades.cerrarConexion();
                    datosEspecialidades = new AccesoDatos();
                    datosEspecialidades.setearConsulta("INSERT INTO Especialidades (IdMedico, IdEspecialidad) VALUES (@IdMedico, @IdEspecialidad)");
                    datosEspecialidades.setearParametro("@IdMedico", medico.id);
                    datosEspecialidades.setearParametro("@IdEspecialidad", idEspecialidad);
                    datosEspecialidades.ejecutarAccion();
                }
            }
            finally
            {
                datosEspecialidades.cerrarConexion();
            }
        }

        public void eliminar(int id)
        {
            AccesoDatos datosMedico = new AccesoDatos();
            try
            {
                datosMedico.setearConsulta("UPDATE Medico SET Eliminado = 1 WHERE IdMedico = @Id");
                datosMedico.setearParametro("@Id", id);
                datosMedico.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datosMedico.cerrarConexion();
            }
        }

        public int obtenerIdPorUsuario(int idUsuario)
        {
            AccesoDatos datosMedico = new AccesoDatos();
            try
            {
                datosMedico.setearConsulta("SELECT IdMedico FROM Medico WHERE IdUsuario = @IdUsuario AND Eliminado = 0");
                datosMedico.setearParametro("@IdUsuario", idUsuario);
                object resultado = datosMedico.ejecutarScalar();
                return resultado == null || resultado == DBNull.Value ? 0 : Convert.ToInt32(resultado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datosMedico.cerrarConexion();
            }
        }

        public void reactivar(int id)
        {
            AccesoDatos datosMedico = new AccesoDatos();
            try
            {
                datosMedico.setearConsulta("UPDATE Medico SET Eliminado = 0 WHERE IdMedico = @Id");
                datosMedico.setearParametro("@Id", id);
                datosMedico.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datosMedico.cerrarConexion();
            }
        }
    }
}
