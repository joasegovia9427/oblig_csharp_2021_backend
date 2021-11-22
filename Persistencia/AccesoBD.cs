using System;
using System.Data.SqlClient;
using System.Data;
using Persistencia.Utils;

namespace Persistencia
{
    public class AccesoBD
    {

        public AccesoBD()
        {
        }

        public static bool EjecutarQuerySinRetorno(String query, SqlConnection con)
        {
            try
            {
                con.Open();
                SqlCommand command= con.CreateCommand();
                command = new SqlCommand(query, con);
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (con != null)
                {
                    con.Close();
                }
                return false;
            }
            
            if (con != null)
            {
                con.Close();
            }
            return true;
        }

        public static int EjecutarQueryRetornoID(String query, SqlConnection con)
        {
            try
            {
                con.Open();
                SqlCommand command = con.CreateCommand();
                command = new SqlCommand(query, con);
                int id = Convert.ToInt32(command.ExecuteScalar());

                if (con != null)
                {
                    con.Close();
                }
                return id; 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (con != null)
                {
                    con.Close();
                }
                return 0;
            }
        }

        public static DataTable EjecutarQueryConRetorno(String query, String nombreEntidad, SqlConnection con)
        {
            try
            {
                con.Open();
                SqlCommand command = con.CreateCommand();
                command = new SqlCommand(query, con);
                command.ExecuteNonQuery();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;

                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, nombreEntidad);
                if (con != null)
                {
                    con.Close();
                }
                return dataSet.Tables[0];
            }
            catch (Exception ex)
            {
                if (con != null)
                {
                    Console.WriteLine(ex.Message);
                    con.Close();
                }
                return null;
            }
        }

        public static bool ExistenValoresEnTabla(String entidad, SqlConnection con)
        {
            DataTable result = EjecutarQueryConRetorno(QueryBuilder.FindAll(entidad), entidad, con);
            if (result.Rows.Count == 0)
            {
                return false;
            }
            return true;
        }



    }


}
