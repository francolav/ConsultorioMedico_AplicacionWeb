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
    }
}
