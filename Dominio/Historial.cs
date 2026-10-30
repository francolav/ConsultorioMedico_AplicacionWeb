using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Historial
    {
        public int IdTurno { get; set; }
        public DateTime Fecha { get; set; }
        public string Diagnostico { get; set; }
    }
}
