using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Turno
    {
        public int Id { get; set; }
        public Paciente paciente { get; set; }
        public Medico medico { get; set; }
        
        public DateTime Fecha { get; set; }
        public DateTime Hora { get; set; }
        public bool estado { get; set; }


        public Turno()
        {
            paciente = new Paciente();
            medico = new Medico();
        }
    }
}
