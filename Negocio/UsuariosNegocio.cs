using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Acceso_a_Datos;
using Dominio;

namespace Negocio
{
    public class UsuariosNegocio
    {
        public List<Usuario> listar()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("select IdUsuario, Username, PasswordHash, requiereCambioPassword, Activo, FechaCreacion, Eliminado from usuario where Eliminado = 0");
                datos.ejecutarConsulta();
                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();
                    aux.IdUsuario = (int)datos.Lector["IdUsuario"];
                    aux.Username = (string)datos.Lector["Username"];
                    aux.PasswordHash = datos.Lector["PasswordHash"] != DBNull.Value ? (string)datos.Lector["PasswordHash"] : "";
                    aux.requiereCambioPassword = (bool)datos.Lector["requiereCambioPassword"];
                    aux.Activo = (bool)datos.Lector["Activo"];
                    aux.FechaCreacion = (DateTime)datos.Lector["FechaCreacion"];
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

        public Usuario buscarPorUsername(string username)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
                    select u.IdUsuario, u.Username, u.PasswordHash, u.IdRol,
                           u.requiereCambioPassword, u.Activo, u.FechaCreacion,
                           r.Nombre as NombreRol
                    from usuario u
                    inner join Rol r on r.IdRol = u.IdRol
                    where u.Username = @Username and u.Eliminado = 0");
                datos.setearParametro("@Username", username);
                datos.ejecutarConsulta();

                if (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();
                    aux.IdUsuario = (int)datos.Lector["IdUsuario"];
                    aux.Username = (string)datos.Lector["Username"];
                    aux.PasswordHash = datos.Lector["PasswordHash"] != DBNull.Value ? (string)datos.Lector["PasswordHash"] : "";
                    aux.requiereCambioPassword = (bool)datos.Lector["requiereCambioPassword"];
                    aux.Activo = (bool)datos.Lector["Activo"];
                    aux.FechaCreacion = (DateTime)datos.Lector["FechaCreacion"];
                    aux.Rol = new Rol
                    {
                        Id = (int)datos.Lector["IdRol"],
                        Nombre = (string)datos.Lector["NombreRol"]
                    };
                    return aux;
                }

                return null;
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

        public Usuario autenticar(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(password))
                return null;

            Usuario usuario = buscarPorUsername(username.Trim());
            if (usuario == null || !usuario.Activo || !verificarPassword(password, usuario.PasswordHash))
                return null;

            return usuario;
        }

        private bool verificarPassword(string password, string passwordHash)
        {
            if (string.IsNullOrEmpty(passwordHash))
                return false;

            byte[] hashGuardado;
            try
            {
                hashGuardado = Convert.FromBase64String(passwordHash);
            }
            catch (FormatException)
            {
                return false;
            }

            if (hashGuardado.Length != 48)
                return false;

            byte[] salt = new byte[16];
            byte[] hashEsperado = new byte[32];
            Buffer.BlockCopy(hashGuardado, 0, salt, 0, salt.Length);
            Buffer.BlockCopy(hashGuardado, salt.Length, hashEsperado, 0, hashEsperado.Length);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
            {
                byte[] hashCalculado = pbkdf2.GetBytes(hashEsperado.Length);
                int diferencia = 0;
                for (int i = 0; i < hashEsperado.Length; i++)
                    diferencia |= hashEsperado[i] ^ hashCalculado[i];
                return diferencia == 0;
            }
        }

        public void agregar(Usuario nuevoUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into usuario (Username, PasswordHash, IdRol, requiereCambioPassword, Activo, FechaCreacion) values (@Username, @PasswordHash, @IdRol, @requiereCambioPassword, @Activo, @FechaCreacion)");
                datos.setearParametro("@Username", nuevoUsuario.Username);
                datos.setearParametro("@PasswordHash", nuevoUsuario.PasswordHash);
                datos.setearParametro("@requiereCambioPassword", nuevoUsuario.requiereCambioPassword);
                datos.setearParametro("@Activo", nuevoUsuario.Activo);
                datos.setearParametro("@FechaCreacion", nuevoUsuario.FechaCreacion);
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

        public void modificar(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update usuario set Username = @Username, PasswordHash = @PasswordHash, IdRol = @IdRol, requiereCambioPassword = @requiereCambioPassword, Activo = @Activo, FechaCreacion = @FechaCreacion where IdUsuario = @Id");
                datos.setearParametro("@Username", usuario.Username);
                datos.setearParametro("@PasswordHash", usuario.PasswordHash);
                datos.setearParametro("@requiereCambioPassword", usuario.requiereCambioPassword);
                datos.setearParametro("@Activo", usuario.Activo);
                datos.setearParametro("@FechaCreacion", usuario.FechaCreacion);
                datos.setearParametro("@Id", usuario.IdUsuario);
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

        public void eliminar(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update usuario set Eliminado = 1 where IdUsuario = @Id");
                datos.setearParametro("@Id", idUsuario);
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

        public void altaUsuarioMedico(int idMedico, string username, string claveTemporal, int idRolMedico)
        {
            if (idMedico <= 0)
                throw new ArgumentException("Debe seleccionar un médico válido.");

            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("El nombre de usuario es obligatorio.");

            if (string.IsNullOrWhiteSpace(claveTemporal))
                throw new ArgumentException("La clave temporal es obligatoria.");

            if (buscarPorUsername(username.Trim()) != null)
                throw new InvalidOperationException("Ese nombre de usuario ya está en uso.");

            string hashTemporal = hashearPassword(claveTemporal);
            int nuevoIdUsuario = 0;

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
                    insert into Usuario (Username, PasswordHash, IdRol, requiereCambioPassword, Activo, Eliminado)
                    values (@Username, @PasswordHash, @IdRol, 1, 1, 0);
                    select cast(scope_identity() as int);");
                datos.setearParametro("@Username", username.Trim());
                datos.setearParametro("@PasswordHash", hashTemporal);
                datos.setearParametro("@IdRol", idRolMedico);

                datos.ejecutarConsulta();
                if (datos.Lector.Read())
                {
                    nuevoIdUsuario = (int)datos.Lector[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

            if (nuevoIdUsuario <= 0)
                throw new Exception("No se pudo obtener el ID del nuevo usuario.");

            AccesoDatos datosMedico = new AccesoDatos();
            try
            {
                datosMedico.setearConsulta("update Medico set IdUsuario = @IdUsuario where IdMedico = @IdMedico");
                datosMedico.setearParametro("@IdUsuario", nuevoIdUsuario);
                datosMedico.setearParametro("@IdMedico", idMedico);
                datosMedico.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datosMedico.cerrarConexion();
            }
        }

        private string hashearPassword(string password)
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(32);
                byte[] hashBytes = new byte[48];
                Buffer.BlockCopy(salt, 0, hashBytes, 0, 16);
                Buffer.BlockCopy(hash, 0, hashBytes, 16, 32);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}