using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessDVLD
{
    public class clsTestTypeData
    {
        public static DataTable GetAllTestTypes()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(Connection.connection);
            {
                string query = "select * from TestType;";
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

        public static bool GetTestTypes(int id, ref string title, ref int fees, ref string Description)
        {
            bool isFound = false;
            using (SqlConnection conn = new SqlConnection(Connection.connection))
            {
                // Employ parameterized queries for security and type-safety
                string query = "select Title,Description,Fees from TestType where ID=@id";
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
                                Description= reader["Description"].ToString();                                 // Instead of casting directly to int, use Convert.ToInt32() which handles null values gracefully
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

        public static bool UpdateDataTestTypes(int id, string title, int fees, string Description)
        {
            int result = 0;

            SqlConnection conn = new SqlConnection(Connection.connection);

            string query = @"update TestType set
            Title=@title,
            Description=@description,
            Fees=@fees
            where ID=@id";

            SqlCommand command = new SqlCommand(query, conn);

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@title", title);
            command.Parameters.AddWithValue("@Fees", fees);
            command.Parameters.AddWithValue("@Description", Description);

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

    }
}
