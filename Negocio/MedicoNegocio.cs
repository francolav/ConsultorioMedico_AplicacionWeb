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
        List<Medico> lista = new List<Medico>();
        AccesoDatos datos = new AccesoDatos();

        public List<Medico> listar()
        {
            try
            {
                datos.setearConsulta("select  IdMedico, Apellido, Nombre, Matricula, Dni, Telefono, TelefonoAlternativo,Email,eliminado, IdEspecialidad from VW_MedicosEspecialidades");
                datos.ejecutarConsulta();
                while (datos.Lector.Read())
                {
     

                    Medico aux = new Medico();
                    aux.id = (int)datos.Lector["IdMedico"];
                    int existe = lista.FindIndex(m => m.id == aux.id);
                    if (existe == -1)
                    {
                        aux.Nombre = (string)datos.Lector["Nombre"];
                        aux.Apellido = (string)datos.Lector["Apellido"];
                        aux.Dni = (string)datos.Lector["Dni"];
                        aux.Matricula = (string)datos.Lector["Matricula"];
                        aux.Telefono = (string)datos.Lector["Telefono"];
                        aux.TelefonoAlternativo = (datos.Lector["TelefonoAlternativo"] != DBNull.Value) ? (string)datos.Lector["TelefonoAlternativo"] : "";
                        aux.Email = (datos.Lector["Email"] != DBNull.Value) ? (string)datos.Lector["Email"] : "";
                        aux.IdEspecialidades = new List<int>();
                        aux.IdEspecialidades.Add((int)datos.Lector["IdEspecialidad"]);
                        aux.Eliminado = (bool)datos.Lector["Eliminado"];
                        lista.Add(aux);
                    } else
                    {
                        lista[existe].IdEspecialidades.Add((int)datos.Lector["IdEspecialidad"]);
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
    }
}
