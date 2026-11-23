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

        public void agregar(Especialidad nuevaEspecialidad)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into especialidad (Nombre) values (@Nombre)");
                datos.setearParametro("@Nombre", nuevaEspecialidad.Nombre);
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

        public void modificar(Especialidad especialidad)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update especialidad set Nombre = @Nombre where IdEspecialidad = @Id");
                datos.setearParametro("@Nombre", especialidad.Nombre);
                datos.setearParametro("@Id", especialidad.Id);
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

        public void eliminar(int idEspecialidad)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update especialidad set Eliminado = 1 where IdEspecialidad = @Id");
                datos.setearParametro("@Id", idEspecialidad);
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
