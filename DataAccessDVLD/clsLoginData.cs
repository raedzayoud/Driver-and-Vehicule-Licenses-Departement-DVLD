using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessDVLD
{
    public  class clsLoginData
    {
        public static bool FindByUsernamePassword(string username,string password)
        {
            object isFound;
            SqlConnection conn = new SqlConnection(Connection.connection);
            string query = "select found=1 from Users where username=@username and  password=@password and isActive=1 ";
            SqlCommand sqlCommand = new SqlCommand(query, conn);
            sqlCommand.Parameters.AddWithValue("@username", username);
            sqlCommand.Parameters.AddWithValue("@password", password);
            try
            {
                conn.Open();
                isFound = sqlCommand.ExecuteScalar();
                if (isFound != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            conn.Close();
            return false;


        }


        public static DataTable FindIDByUsernameandPassword(string username, string password)
        {
            DataTable dataTable = new DataTable(); // DataTable to hold results
            SqlConnection conn = new SqlConnection(Connection.connection);
            string query = "SELECT idUser,idPerson FROM Users WHERE username = @username AND password = @password AND isActive = 1";
            SqlCommand sqlCommand = new SqlCommand(query, conn);
            sqlCommand.Parameters.AddWithValue("@username", username);
            sqlCommand.Parameters.AddWithValue("@password", password);

            try
            {
                conn.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                dataTable.Load(reader); // Load the results into the DataTable
            }
            catch (Exception e)
            {
                // Handle exception if needed
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close(); // Ensure connection is closed even if exception occurs
            }

            return dataTable; // Return the populated DataTable
        }




    }
}
