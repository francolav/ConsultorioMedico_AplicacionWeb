using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Paciente : Persona
    {
        public int Id { get; set; }
        public List<Historial> historial { get; set; }
        public ObraSocial ObraSocial { get; set; }
        public bool eliminado { get; set; }

        public Paciente()
        {
            ObraSocial = new ObraSocial();
            historial = new List<Historial>();
        }



    }
}
