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
        public const string PasswordTemporalInicial = "1234";

        public List<Usuario> listar()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("select IdUsuario, Username, PasswordHash, RequiereCambioClave, Activo, FechaCreacion, Eliminado from usuario where Eliminado = 0");
                datos.ejecutarConsulta();
                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();
                    aux.IdUsuario = (int)datos.Lector["IdUsuario"];
                    aux.Username = (string)datos.Lector["Username"];
                    aux.PasswordHash = datos.Lector["PasswordHash"] != DBNull.Value ? (string)datos.Lector["PasswordHash"] : "";
                    aux.requiereCambioPassword = (bool)datos.Lector["RequiereCambioClave"];
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
                           u.RequiereCambioClave, u.Activo, u.FechaCreacion,
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
                    aux.requiereCambioPassword = (bool)datos.Lector["RequiereCambioClave"];
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

        public bool completarPrimerAcceso(string username, string claveTemporal, string nuevaPassword)
        {
            if (string.IsNullOrWhiteSpace(username) || !string.Equals(claveTemporal, PasswordTemporalInicial, StringComparison.Ordinal) || string.IsNullOrEmpty(nuevaPassword))
                return false;

            Usuario usuario = buscarPorUsername(username.Trim());
            if (usuario == null || !usuario.Activo || !usuario.requiereCambioPassword)
                return false;

            string nuevoHash = hashearPassword(nuevaPassword);
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Usuario SET PasswordHash = @NuevoHash, RequiereCambioClave = 0 WHERE IdUsuario = @IdUsuario AND PasswordHash = @PasswordTemporal AND Activo = 1 AND Eliminado = 0 AND RequiereCambioClave = 1; SELECT @@ROWCOUNT;");
                datos.setearParametro("@NuevoHash", nuevoHash);
                datos.setearParametro("@IdUsuario", usuario.IdUsuario);
                datos.setearParametro("@PasswordTemporal", usuario.PasswordHash);
                object filasActualizadas = datos.ejecutarScalar();
                return filasActualizadas != null && Convert.ToInt32(filasActualizadas) == 1;
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

        public void restablecerPassword(int idUsuario)
        {
            if (idUsuario <= 0)
                throw new ArgumentException("El usuario no es válido.", "idUsuario");

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Usuario SET PasswordHash = @PasswordHash, RequiereCambioClave = 1 WHERE IdUsuario = @IdUsuario AND Activo = 1 AND Eliminado = 0; SELECT @@ROWCOUNT;");
                datos.setearParametro("@PasswordHash", hashearPassword(PasswordTemporalInicial));
                datos.setearParametro("@IdUsuario", idUsuario);
                object filasActualizadas = datos.ejecutarScalar();
                if (filasActualizadas == null || Convert.ToInt32(filasActualizadas) != 1)
                    throw new InvalidOperationException("No se pudo restablecer la contraseña de esa cuenta activa.");
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

        public void cambiarPasswordTemporal(int idUsuario, string nuevaPassword)
        {
            if (idUsuario <= 0)
                throw new ArgumentException("El usuario no es válido.", "idUsuario");
            if (string.IsNullOrWhiteSpace(nuevaPassword))
                throw new ArgumentException("La nueva contraseña es obligatoria.", "nuevaPassword");

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Usuario SET PasswordHash = @PasswordHash, RequiereCambioClave = 0 WHERE IdUsuario = @IdUsuario AND Activo = 1 AND Eliminado = 0 AND RequiereCambioClave = 1; SELECT @@ROWCOUNT;");
                datos.setearParametro("@PasswordHash", hashearPassword(nuevaPassword));
                datos.setearParametro("@IdUsuario", idUsuario);
                object filasActualizadas = datos.ejecutarScalar();
                if (filasActualizadas == null || Convert.ToInt32(filasActualizadas) != 1)
                    throw new InvalidOperationException("No se pudo actualizar la contraseña temporal de la cuenta.");
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

        public void agregar(Usuario nuevoUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into usuario (Username, PasswordHash, IdRol, RequiereCambioClave, Activo, FechaCreacion) values (@Username, @PasswordHash, @IdRol, @requiereCambioPassword, @Activo, @FechaCreacion)");
                string passwordHash = nuevoUsuario.requiereCambioPassword
                    ? hashearPassword(PasswordTemporalInicial)
                    : nuevoUsuario.PasswordHash;
                datos.setearParametro("@Username", nuevoUsuario.Username);
                datos.setearParametro("@PasswordHash", passwordHash);
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
                datos.setearConsulta("update usuario set Username = @Username, PasswordHash = @PasswordHash, IdRol = @IdRol, RequiereCambioClave = @requiereCambioPassword, Activo = @Activo, FechaCreacion = @FechaCreacion where IdUsuario = @Id");
                string passwordHash = usuario.requiereCambioPassword
                    ? hashearPassword(PasswordTemporalInicial)
                    : usuario.PasswordHash;
                datos.setearParametro("@Username", usuario.Username);
                datos.setearParametro("@PasswordHash", passwordHash);
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

        public bool usernameDisponible(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return false;

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(1) FROM Usuario WHERE Username = @Username");
                datos.setearParametro("@Username", username.Trim());
                object resultado = datos.ejecutarScalar();
                return resultado != null && Convert.ToInt32(resultado) == 0;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public int obtenerIdRolMedico()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT TOP 1 IdRol FROM Rol WHERE LOWER(REPLACE(Nombre, N'é', N'e')) = N'medico'");
                object resultado = datos.ejecutarScalar();
                if (resultado == null || resultado == DBNull.Value)
                    throw new InvalidOperationException("No se encontró el rol de médico en la base de datos.");

                return Convert.ToInt32(resultado);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void altaUsuarioMedico(int idMedico, string username, int idRolMedico)
        {
            if (idMedico <= 0)
                throw new ArgumentException("Debe seleccionar un médico válido.");

            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("El nombre de usuario es obligatorio.");

            string hashTemporal = hashearPassword(PasswordTemporalInicial);
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
                    SET XACT_ABORT ON;
                    DECLARE @IdUsuario int;
                    BEGIN TRY
                        BEGIN TRANSACTION;

                        IF EXISTS (SELECT 1 FROM Usuario WITH (UPDLOCK, HOLDLOCK) WHERE Username = @Username)
                        BEGIN
                            ;THROW 50001, N'Ese nombre de usuario ya está en uso.', 1;
                        END;

                        INSERT INTO Usuario (Username, PasswordHash, IdRol, RequiereCambioClave, Activo, Eliminado)
                        VALUES (@Username, @PasswordHash, @IdRol, 1, 1, 0);
                        SET @IdUsuario = CONVERT(int, SCOPE_IDENTITY());

                        UPDATE Medico SET IdUsuario = @IdUsuario WHERE IdMedico = @IdMedico;
                        IF @@ROWCOUNT <> 1
                        BEGIN
                            ;THROW 50002, N'No se encontró el médico para asociar el usuario.', 1;
                        END;

                        COMMIT TRANSACTION;
                        SELECT @IdUsuario;
                    END TRY
                    BEGIN CATCH
                        IF @@TRANCOUNT > 0
                            ROLLBACK TRANSACTION;
                        THROW;
                    END CATCH;");
                datos.setearParametro("@Username", username.Trim());
                datos.setearParametro("@PasswordHash", hashTemporal);
                datos.setearParametro("@IdRol", idRolMedico);
                datos.setearParametro("@IdMedico", idMedico);
                datos.ejecutarConsulta();
                if (!datos.Lector.Read())
                    throw new InvalidOperationException("No se pudo crear la cuenta del médico.");
            }
            finally
            {
                datos.cerrarConexion();
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