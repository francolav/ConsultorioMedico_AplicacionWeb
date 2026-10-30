using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Acceso_a_Datos
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;
        public SqlDataReader Lector { get { return lector; } }

    public AccesoDatos()//se asigna la bbdd y se instancia command
        {
            conexion = new SqlConnection("server=lav-pons.ddns.net,51208;database=ConsultorioMedico_appWeb;user id=ConsultorioMedicoWeb;password=Programacion3;TrustServerCertificate=true");
            comando = new SqlCommand();
        }


    public void setearConsulta(string consulta)//se setea la consulta a la bbdd x parametro
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }


        public void ejecutarConsulta()
        {
            comando.Connection = conexion;//se asigna la conexion a la bbdd
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();//a lector se le asigna la lectura traida de la bbdd de lo establecido en la consulta
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ejecutarAccion()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void setearParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);

        }

        public void cerrarConexion()
        {
            if (lector != null)// xej en caso de que hagamos un alta puede llegar a ser null xq no usamos lector y solo cierra conexion
            {
                lector.Close();
            }
            conexion.Close();


        }
        public object ejecutarScalar()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                return comando.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
