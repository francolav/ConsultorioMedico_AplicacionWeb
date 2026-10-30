using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class AgendaMedico
    {
        public int IdAgenda { get; set; }
        public int Dia { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public int DuracionTurno { get; set; }




    }
}
