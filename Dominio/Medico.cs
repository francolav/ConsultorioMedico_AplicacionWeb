using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;


namespace Dominio
{
    public class Medico: Persona
    {
        public int id { get; set; }
        public int Id { get { return id; } set { id = value; } }
        public List<int> IdEspecialidades { get; set; }
        public string Matricula { get; set; }
        public List<AgendaMedico> agenda { get; set; }
        public bool Eliminado { get; set; }
        public string NombreCompleto { get { return Nombre + " " + Apellido; } }

        public Medico()
        {
            IdEspecialidades = new List<int>();
            agenda = new List<AgendaMedico>();
        }

       

    }
}
