using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acceso_a_Datos;

namespace Negocio
{
    public class EspecialidadNegocio
    {
        List<Especialidad> lista = new List<Especialidad>();
        AccesoDatos datos = new AccesoDatos();
        
        public List<Especialidad> listar()
        {

            try
            {
                datos.setearConsulta("select IdEspecialidad, Nombre from especialidad where Eliminado = 0");
                datos.ejecutarConsulta();
                while (datos.Lector.Read())
                {
                    Especialidad aux = new Especialidad();
                    aux.Id = (int)datos.Lector["IdEspecialidad"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
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
