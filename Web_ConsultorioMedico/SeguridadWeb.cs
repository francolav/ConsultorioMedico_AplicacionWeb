using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using Dominio;

namespace Web_ConsultorioMedico
{
    public static class SeguridadWeb
    {
        public const string SessionUsuario = "UsuarioAutenticado";

        public static string CategoriaRol(string nombreRol)
        {
            if (string.IsNullOrWhiteSpace(nombreRol))
                return string.Empty;

            string normalizado = nombreRol.Normalize(NormalizationForm.FormD);
            StringBuilder resultado = new StringBuilder();
            foreach (char caracter in normalizado)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(caracter) != UnicodeCategory.NonSpacingMark)
                    resultado.Append(caracter);
            }

            string rol = resultado.ToString().Normalize(NormalizationForm.FormC).Trim().ToLowerInvariant();
            if (rol == "medico")
                return "Medico";
            if (rol == "administracion" || rol == "administrador" || rol == "admin")
                return "Administracion";
            return string.Empty;
        }

        public static Usuario UsuarioActual(HttpContext context)
        {
            return context.Session == null ? null : context.Session[SessionUsuario] as Usuario;
        }

        public static bool AutorizarPagina(HttpContext context, string pagina)
        {
            Usuario usuario = UsuarioActual(context);
            if (usuario == null || usuario.Rol == null)
                return false;

            string categoria = CategoriaRol(usuario.Rol.Nombre);
            string nombrePagina = Path.GetFileName(pagina);
            if (usuario.requiereCambioPassword)
                return nombrePagina == "CambiarPassword.aspx";

            if (categoria == "Administracion")
            {
                return nombrePagina == "GestionMedicos.aspx" || nombrePagina == "AltaMedicos.aspx" ||
                    nombrePagina == "PacientesLista.aspx" || nombrePagina == "Pacientes.aspx" ||
                    nombrePagina == "Recepcion.aspx" || nombrePagina == "GestionTurno.aspx" ||
                    nombrePagina == "GestionObraSocial.aspx" || nombrePagina == "Usuarios.aspx";
            }

            return categoria == "Medico" &&
                (nombrePagina == "ResumenMedicos.aspx" || nombrePagina == "Agenda.aspx");
        }

        public static string PaginaInicio(Usuario usuario)
        {
            string categoria = usuario == null || usuario.Rol == null ? string.Empty : CategoriaRol(usuario.Rol.Nombre);
            if (categoria == "Administracion")
                return "~/GestionMedicos.aspx";
            if (categoria == "Medico")
                return "~/ResumenMedicos.aspx";
            return "~/Login.aspx";
        }
    }
}
