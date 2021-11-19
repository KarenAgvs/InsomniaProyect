using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WebApplication1Crud.Models
{
    public class database
    {
        private MySqlConnection connection;

        public database()
        {
            connection = new MySqlConnection("datasource = localhost; port = 3306; username = root; password=; database = bdfactura; SSLMode = none");
        }

        public string MySqlOperations(string sql)
        {
            string resultado ="";
            try
            {
                connection.Open();

                MySqlCommand comand = new MySqlCommand(sql, connection);
                int resultCount = comand.ExecuteNonQuery();

                if (resultCount > -1)
                {
                    resultado = "Success";
                }
                else
                {
                    resultado = "Failed";

                }

                connection.Close();
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }

            return resultado;
        }
    
        public DataTable getData(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                MySqlCommand query = new MySqlCommand(sql,connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(query);
                adapter.Fill(dt);
                connection.Close();
                adapter.Dispose();
            }
            catch
            {
                dt=null;
            }
            return dt;
        }
    }
}
