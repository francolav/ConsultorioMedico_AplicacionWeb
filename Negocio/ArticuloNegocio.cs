//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Dominio;
//using Acceso_a_Datos;


//namespace Negocio

//{
//    public class ArticuloNegocio
//    {
   
//        public List<Articulo> listarArticulos()
//        {
//            List<Articulo> lista = new List<Articulo>();
//            AccesoDatos datos = new AccesoDatos();


//            try
//            {
//                datos.setearConsulta("Select A.Id, Codigo, Nombre, A.Descripcion,A.IdMarca, A.IdCategoria,M.Descripcion as Marca,C.Descripcion as Categoria, Precio, ImagenUrl from ARTICULOS A, MARCAS M, CATEGORIAS C, IMAGENES I Where A.IdMarca=M.Id And A.IdCategoria=C.Id And A.Id=I.IdArticulo ");
//                datos.setearParametro("@idCategoria", 1);
//                datos.ejecutarConsulta();

//                while (datos.Lector.Read())
//                {

//                    bool bArticulo = false;

//                    foreach (Articulo item in lista)
//                    {
//                        if (item.Id == (int)datos.Lector["Id"])
//                        {
//                            bArticulo = true;
//                            item.agregarImagen((string)datos.Lector["ImagenUrl"]);
//                        }
//                    }

//                    if (!bArticulo)
//                    {

//                        Articulo aux = new Articulo();
//                        aux.Id = (int)datos.Lector["Id"];
//                        aux.Codigo = (string)datos.Lector["Codigo"];
//                        aux.Nombre = (string)datos.Lector["Nombre"];
//                        aux.Descripcion = (string)datos.Lector["Descripcion"];
//                        aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
//                        aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
//                        aux.Marca.Id = (int)datos.Lector["IdMarca"];
//                        aux.Marca.Descripcion = (string)datos.Lector["Marca"];
//                        aux.Precio = (decimal)datos.Lector["Precio"];
//                        aux.agregarImagen((string)datos.Lector["ImagenUrl"]);
//                        lista.Add(aux);
//                    }

//                }

//                return lista;

//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
//            finally
//            {
//                datos.cerrarConexion();
//            }

//        }

//        public void nuevoArticulo(Articulo nuevo)
//        {
//            AccesoDatos datos = new AccesoDatos();

//            try
//            {
//                datos.setearConsulta("Insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio)OUTPUT INSERTED.Id values (@codigo,@nombre, @descripcion, @idMarca, @idCategoria, @precio)");
//                datos.setearParametro("@codigo", nuevo.Codigo);
//                datos.setearParametro("@nombre", nuevo.Nombre);
//                datos.setearParametro("@descripcion", nuevo.Descripcion);
//                datos.setearParametro("@idMarca", nuevo.Marca.Id);
//                datos.setearParametro("@idCategoria", nuevo.Categoria.Id);
//                datos.setearParametro("@precio", nuevo.Precio);
//                nuevo.Id = (int)datos.ejecutarScalar();
//            }
//            catch (Exception ex)
//            {

//                throw;
//            }
//            finally
//            {
//                datos.cerrarConexion();
//            }



//        }

//        public void modificarArticulo(Articulo modificado)
//        {
//            AccesoDatos datos = new AccesoDatos();

//            try
//            {
//                datos.setearConsulta("update ARTICULOS set Codigo=@codigo, Nombre=@nombre, Descripcion=@descripcion, IdMarca=@idMarca, IdCategoria=@idCategoria, Precio=@precio Where Id=@id ");

//                datos.setearParametro("@codigo", modificado.Codigo);
//                datos.setearParametro("@nombre", modificado.Nombre);
//                datos.setearParametro("@descripcion", modificado.Descripcion);
//                datos.setearParametro("@idMarca", modificado.Marca.Id);
//                datos.setearParametro("@idCategoria", modificado.Categoria.Id);
//                datos.setearParametro("@precio", modificado.Precio);
//                datos.setearParametro("@id", modificado.Id);
//                datos.ejecutarAccion();
//            }
//            catch (Exception ex)
//            {

//                throw; //Se rompe aca

//            }
//            finally
//            {
//                datos.cerrarConexion();
//            }



//        }

//        public void eliminarArticulo(Articulo articulo)
//        {
//            AccesoDatos datos = new AccesoDatos();

//            try
//            {
//                datos.setearConsulta("Delete from ARTICULOS Where @id=Id");
//                datos.setearParametro("@id", articulo.Id);
//                datos.ejecutarAccion();

//            }
//            catch (Exception ex)
//            {

//                throw;
//            }
//            finally
//            {
//                datos.cerrarConexion();
//            }

//        }

//        public List<Articulo> filtrarArticulos(string campo, string criterio, string filtro)
//        {
//            List<Articulo> lista = new List<Articulo>();
//            AccesoDatos datos = new AccesoDatos();
//            try
//            {
//                string consulta = "Select A.Id, Codigo, Nombre, A.Descripcion,A.IdMarca, A.IdCategoria,M.Descripcion as Marca,C.Descripcion as Categoria, Precio, ImagenUrl from ARTICULOS A, MARCAS M, CATEGORIAS C, IMAGENES I Where A.IdMarca=M.Id And A.IdCategoria=C.Id And A.Id=I.IdArticulo And ";
//                if (campo == "Nombre")
//                {
//                    switch (criterio)
//                    {
//                        case "Comienza con":
//                            consulta += "A.Nombre like '" + filtro + "%'";
//                            break;
//                        case "Termina con":
//                            consulta += "A.Nombre like '%" + filtro + "'";
//                            break;
//                        case "Contiene":
//                            consulta += "A.Nombre like '%" + filtro + "%'";
//                            break;
//                    }
//                }
//                else if (campo == "Marca")
//                {
//                    switch (criterio)
//                    {
//                        case "Comienza con":
//                            consulta += "M.Descripcion like '" + filtro + "%'";
//                            break;
//                        case "Termina con":
//                            consulta += "M.Descripcion like '%" + filtro + "'";
//                            break;
//                        case "Contiene":
//                            consulta += "M.Descripcion like '%" + filtro + "%'";
//                            break;
//                    }
//                }
//                else if (campo == "Precio")
//                {
//                    switch (criterio)
//                    {
//                        case "Mayor a":
//                            consulta += "A.Precio > " + filtro;
//                            break;
//                        case "Menor a":
//                            consulta += "A.Precio < " + filtro;
//                            break;
//                        case "Igual a":
//                            consulta += "A.Precio = " + filtro;
//                            break;
//                    }
//                }
//                datos.setearConsulta(consulta);
//                datos.ejecutarConsulta();
//                while (datos.Lector.Read())
//                {
//                    bool bArticulo = false;
//                    foreach (Articulo item in lista)
//                    {
//                        if (item.Id == (int)datos.Lector["Id"])
//                        {
//                            bArticulo = true;
//                            item.agregarImagen((string)datos.Lector["ImagenUrl"]);
//                        }
//                    }
//                    if (!bArticulo)
//                    {

//                        Articulo aux = new Articulo();
//                        aux.Id = (int)datos.Lector["Id"];
//                        aux.Codigo = (string)datos.Lector["Codigo"];
//                        aux.Nombre = (string)datos.Lector["Nombre"];
//                        aux.Descripcion = (string)datos.Lector["Descripcion"];
//                        aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
//                        aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
//                        aux.Marca.Id = (int)datos.Lector["IdMarca"];
//                        aux.Marca.Descripcion = (string)datos.Lector["Marca"];
//                        aux.Precio = (decimal)datos.Lector["Precio"];
//                        aux.agregarImagen((string)datos.Lector["ImagenUrl"]);
//                        lista.Add(aux);
//                    }
//                }

//                return lista;

//            }
//            catch (Exception ex)
//            {

//                throw;
//            }
//            finally
//            {
//                datos.cerrarConexion();
//            }


//        }
//        public List<Articulo> listarArticulosId(int idCategoria)
//        {
//            List<Articulo> lista = new List<Articulo>();
//            AccesoDatos datos = new AccesoDatos();


//            try
//            {
//                datos.setearConsulta("Select A.Id, Codigo, Nombre, A.Descripcion,A.IdMarca, A.IdCategoria,M.Descripcion as Marca,C.Descripcion as Categoria, Precio, ImagenUrl from ARTICULOS A, MARCAS M, CATEGORIAS C, IMAGENES I Where A.IdMarca=M.Id And A.IdCategoria=C.Id And A.Id=I.IdArticulo ");
//                datos.setearParametro("@idCategoria", 1);
//                datos.ejecutarConsulta();

//                while (datos.Lector.Read())
//                {

//                    bool bArticulo = false;

//                    foreach (Articulo item in lista)
//                    {
//                        if (item.Id == (int)datos.Lector["Id"])
//                        {
//                            bArticulo = true;
//                            item.agregarImagen((string)datos.Lector["ImagenUrl"]);
//                        }
//                    }

//                    if (!bArticulo)
//                    {

//                        Articulo aux = new Articulo();
//                        aux.Id = (int)datos.Lector["Id"];
//                        aux.Codigo = (string)datos.Lector["Codigo"];
//                        aux.Nombre = (string)datos.Lector["Nombre"];
//                        aux.Descripcion = (string)datos.Lector["Descripcion"];
//                        aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
//                        aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
//                        aux.Marca.Id = (int)datos.Lector["IdMarca"];
//                        aux.Marca.Descripcion = (string)datos.Lector["Marca"];
//                        aux.Precio = (decimal)datos.Lector["Precio"];
//                        aux.agregarImagen((string)datos.Lector["ImagenUrl"]);
//                        lista.Add(aux);
//                    }

//                }

//                return lista;

//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
//            finally
//            {
//                datos.cerrarConexion();
//            }

//        }

//        public void eliminarArticulo(int id)
//        {
//            throw new NotImplementedException();
//        }
//    }

//}