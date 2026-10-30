//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Dominio;


//namespace Negocio
//{

//    public class ImagenNegocio
//    {
        
//        public void nuevaImagen(Imagen nuevaImagen)
//        {
//            AccesoDatos datos = new AccesoDatos();

//            try
//            {
//                datos.setearConsulta("Insert into IMAGENES (IdArticulo,ImagenUrl) values (@idArticulo, @url)");
//                datos.setearParametro("@idArticulo", nuevaImagen.IdArticulo);
//                datos.setearParametro("@url", nuevaImagen.UrlImagen);

//                datos.ejecutarAccion();
//            }
//            catch (Exception)
//            {

//                throw;
//            }
//            finally
//            {
//                datos.cerrarConexion();
//            }



//        }

//       /* public List<Imagen> listarImagenes(int idArticulo)
//        {
//            datos.setearConsulta("select ImagenUrl from IMAGENES where @idArticulo=IdArticulo");
//            datos.setearParametro("@idArticulo", idArticulo);

//        }
//       */
//    }
//}
