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
                    }
                    else
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

        public void agregar(Medico nuevo)
        {
            try
            {
                datos.setearConsulta("insert into Medicos (Nombre, Apellido, Matricula, Dni, Telefono, TelefonoAlternativo, Email) values (@Nombre, @Apellido, @Matricula, @Dni, @Telefono, @TelefonoAlternativo, @Email)");
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Apellido", nuevo.Apellido);
                datos.setearParametro("@Matricula", nuevo.Matricula);
                datos.setearParametro("@Dni", nuevo.Dni);
                datos.setearParametro("@Telefono", nuevo.Telefono);
                datos.setearParametro("@TelefonoAlternativo", nuevo.TelefonoAlternativo);
                datos.setearParametro("@Email", nuevo.Email);
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

        public void modificar(Medico medico)
        {
            try
            {
                datos.setearConsulta("update Medicos set Nombre = @Nombre, Apellido = @Apellido, Matricula = @Matricula, Dni = @Dni, Telefono = @Telefono, TelefonoAlternativo = @TelefonoAlternativo, Email = @Email where IdMedico = @Id");
                datos.setearParametro("@Nombre", medico.Nombre);
                datos.setearParametro("@Apellido", medico.Apellido);
                datos.setearParametro("@Matricula", medico.Matricula);
                datos.setearParametro("@Dni", medico.Dni);
                datos.setearParametro("@Telefono", medico.Telefono);
                datos.setearParametro("@TelefonoAlternativo", medico.TelefonoAlternativo);
                datos.setearParametro("@Email", medico.Email);
                datos.setearParametro("@Id", medico.id);
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
                datos.setearConsulta("update Medicos set Eliminado = 1 where IdMedico = @Id");
                datos.setearParametro("@Id", id);
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
