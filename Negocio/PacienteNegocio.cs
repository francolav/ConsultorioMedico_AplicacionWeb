using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Acceso_a_Datos;

namespace Negocio
{
    public class PacienteNegocio
    {
        List<Paciente> lista = new List<Paciente>();
        AccesoDatos datos = new AccesoDatos();

        public List<Paciente> listar()
        {
            try
            {
                datos.setearConsulta("select idPaciente, idObraSocial, Dni, Apellido, Nombre, Telefono, TelefonoAlternativo, Email, FechaNacimiento from Paciente where Eliminado = 0");
                datos.ejecutarConsulta();
                while (datos.Lector.Read())
                {
                    Paciente aux = new Paciente();
                    aux.Id = (int)datos.Lector["IdPaciente"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.Dni = (string)datos.Lector["Dni"];
                    aux.Telefono = (string)datos.Lector["Telefono"];
                    aux.TelefonoAlternativo = (datos.Lector["TelefonoAlternativo"] != DBNull.Value) ? (string)datos.Lector["TelefonoAlternativo"] : "";
                    aux.Email = (datos.Lector["Email"] != DBNull.Value) ? (string)datos.Lector["Email"] : "";
                    aux.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];
                    aux.ObraSocial = new ObraSocial();
                    aux.ObraSocial.Id = (int)datos.Lector["idObraSocial"];
                    lista.Add(aux);
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

        public void agregar(Paciente nuevo)
        {
            try
            {
                datos.setearConsulta("insert into Paciente (idObraSocial, Dni, Apellido, Nombre, Telefono, TelefonoAlternativo, Email, FechaNacimiento) values (@idObraSocial, @Dni, @Apellido, @Nombre, @Telefono, @TelefonoAlternativo, @Email, @FechaNacimiento)");
                datos.setearParametro("@idObraSocial", nuevo.ObraSocial.Id);
                datos.setearParametro("@Dni", nuevo.Dni);
                datos.setearParametro("@Apellido", nuevo.Apellido);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Telefono", nuevo.Telefono);
                datos.setearParametro("@TelefonoAlternativo", nuevo.TelefonoAlternativo);
                datos.setearParametro("@Email", nuevo.Email);
                datos.setearParametro("@FechaNacimiento", nuevo.FechaNacimiento);
                datos.ejecutarAccion();
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

        public void modificar(Paciente paciente)
        {
            try
            {
                datos.setearConsulta("update Paciente set idObraSocial = @idObraSocial, Dni = @Dni, Apellido = @Apellido, Nombre = @Nombre, Telefono = @Telefono, TelefonoAlternativo = @TelefonoAlternativo, Email = @Email, FechaNacimiento = @FechaNacimiento where IdPaciente = @IdPaciente");
                datos.setearParametro("@idObraSocial", paciente.ObraSocial.Id);
                datos.setearParametro("@Dni", paciente.Dni);
                datos.setearParametro("@Apellido", paciente.Apellido);
                datos.setearParametro("@Nombre", paciente.Nombre);
                datos.setearParametro("@Telefono", paciente.Telefono);
                datos.setearParametro("@TelefonoAlternativo", paciente.TelefonoAlternativo);
                datos.setearParametro("@Email", paciente.Email);
                datos.setearParametro("@FechaNacimiento", paciente.FechaNacimiento);
                datos.setearParametro("@IdPaciente", paciente.Id);
                datos.ejecutarAccion();
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

        public void eliminar(int id)
        {
            try
            {
                datos.setearConsulta("update Paciente set Eliminado = 1 where IdPaciente = @IdPaciente");
                datos.setearParametro("@IdPaciente", id);
                datos.ejecutarAccion();
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

        public void reactivar(int id)
        {
            try
            {
                datos.setearConsulta("update Paciente set Eliminado = 0 where IdPaciente = @IdPaciente");
                datos.setearParametro("@IdPaciente", id);
                datos.ejecutarAccion();
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
    }
}
