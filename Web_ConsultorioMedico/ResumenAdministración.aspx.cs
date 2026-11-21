using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Interfaz_de_usuario;

namespace Interfaz_de_usuario
{
    public partial class ResumenAdministración : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.ConfigurarEncabezado(
                    "Resumen",
                    "La jornada de hoy y los lugares disponibles para quienes llegan sin turno.",
                    "Ver agenda",
                    "+ Nuevo turno"
                );
            }
        }

    }
}   