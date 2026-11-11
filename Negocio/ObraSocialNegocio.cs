using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Acceso_a_Datos;

namespace Negocio
{
    public class ObraSocialNegocio
    {
        public List<ObraSocial> listar()
        {
            List<ObraSocial> lista = new List<ObraSocial>();
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("select IdObraSocial, Nombre from obraSocial where Eliminado = 0");
                datos.ejecutarConsulta();
                while (datos.Lector.Read())
                {
                    ObraSocial aux = new ObraSocial();
                    aux.Id = (int)datos.Lector["IdObraSocial"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void agregar(ObraSocial nuevaObraSocial)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into obraSocial (Nombre) values (@Nombre)");
                datos.setearParametro("@Nombre", nuevaObraSocial.Nombre);
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

        public void modificar(ObraSocial obraSocial)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update obraSocial set Nombre = @Nombre where IdObraSocial = @Id");
                datos.setearParametro("@Nombre", obraSocial.Nombre);
                datos.setearParametro("@Id", obraSocial.Id);
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

        public void eliminar(int idObraSocial)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update obraSocial set Eliminado = 1 where IdObraSocial = @Id");
                datos.setearParametro("@Id", idObraSocial);
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
