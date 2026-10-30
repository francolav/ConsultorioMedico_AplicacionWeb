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
        public List<Especialidad> Especialidades { get; set; }
        public string Matricula { get; set; }
        public List<AgendaMedico> agenda { get; set; }
        public bool Eliminado { get; set; }

        public Medico()  
        {
             List<Especialidad> especialidades = new List<Especialidad>();
            agenda = new List<AgendaMedico>();
        }
    }
}
