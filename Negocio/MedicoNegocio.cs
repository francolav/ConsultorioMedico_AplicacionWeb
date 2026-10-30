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
                datos.setearConsulta("select IdMedico,Nombre,Apellido,Dni,Matricula,Telefono,TelefonoAlternativo,Email,Eliminado from Medico");
                datos.ejecutarConsulta();
                while (datos.Lector.Read())
                {
                    Medico aux = new Medico();
                    aux.id = (int)datos.Lector["IdMedico"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.Dni = (string)datos.Lector["Dni"];
                    aux.Matricula = (string)datos.Lector["Matricula"];
                    aux.Telefono = (string)datos.Lector["Telefono"];
                    //aux.TelefonoAlternativo = (string)datos.Lector["TelefonoAlternativo"];
                   // aux.Email = (string)datos.Lector["Email"];
                    aux.Eliminado = (bool)datos.Lector["Eliminado"];
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
