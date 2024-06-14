using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Net;

namespace DataAccessDVLD
{
    public class clsApplicationTypesData
    {
        public static DataTable GetAllApplicationTypes()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(Connection.connection);
            {
                string query = "select * from ApplicationTypes;";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                dt.Load(reader);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle the exception appropriately (e.g., throw, log)
                        Console.WriteLine("Error retrieving data: " + ex.Message);
                        throw; // Rethrow the exception to propagate it up the call stack
                    }
                }
            }
            conn.Close();
            return dt;
        }

        public static bool GetApplication(int id, ref string title, ref int fees)
        {
            bool isFound = false;
            using (SqlConnection conn = new SqlConnection(Connection.connection))
            {
                // Employ parameterized queries for security and type-safety
                string query = "select Title,Fees from ApplicationTypes where ID=@id";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                title = reader["Title"].ToString(); // Assurez-vous que "Title" est une chaîne
                                                                    // Instead of casting directly to int, use Convert.ToInt32() which handles null values gracefully
                                fees = Convert.ToInt32(reader["Fees"]);
                                isFound = true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log or display the exception for debugging
                        isFound = false; // Rethrow to allow handling at a higher level
                    }
                }
            }
            return isFound;
        }

        public static bool UpdateDataApplicationTypes(int id, string title, int fees)
        {
            int result = 0;

            SqlConnection conn = new SqlConnection(Connection.connection);

            string query = @"update ApplicationTypes set
             Title=@title,
             Fees=@Fees
             where ID=@id";
            SqlCommand command = new SqlCommand(query, conn);

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@title", title);
            command.Parameters.AddWithValue("@Fees", fees);

            try
            {
                conn.Open();
                result = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                // Console.WriteLine(ex.Message);
                return false;
            }
            return result > 0;


        }

        public static bool GetFeesApplication(int idApp,ref int Fees)
        {
            bool isFound = false;
            SqlConnection conn = new SqlConnection(Connection.connection);
            string query = "select paidFees from Applications where ApplicationID=@idApp;";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@idApp", idApp);
            try
            {
                conn.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    isFound = true;
                   Fees = (int)Reader["paidFees"];
                  
                }
                else
                {
                    isFound = false;
                }

            }
            catch (Exception ex)
            {
                // Console.WriteLine(ex.Message);
                isFound = false;
            }
            conn.Close();
            return isFound;


        }
       
        public static int GetFeesApplicationTypes(int idApp)
        {
            int Fees = 0;
            SqlConnection conn = new SqlConnection(Connection.connection);
            string query = "select Fees from ApplicationTypes where ID=@idApp;";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@idApp", idApp);
            try
            {
                conn.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    Fees = (int)Reader["Fees"];
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine(ex.Message);
                return -1;
            }
            conn.Close();
            return Fees;


        }




    }
}
