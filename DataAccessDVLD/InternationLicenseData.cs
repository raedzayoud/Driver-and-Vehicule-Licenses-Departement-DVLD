using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DataAccessDVLD
{
    public class InternationLicenseData
    {

        public static bool FoundInternationalLicense(int idApp)
        {
            string connectionString = Connection.connection;

            string query = "SELECT COUNT(1) FROM InternationalLicense WHERE ApplicationID = @idApp;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idApp", idApp);

                    try
                    {
                        conn.Open();
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                    catch (Exception ex)
                    {
                        // Handle the exception (logging, rethrowing, etc.)
                        throw new Exception("An error occurred while checking for the international license.", ex);
                    }
                }
            }
        }

        public static int InsertInternationalLicense(int idApp, DateTime issueDate, DateTime expirationDate)
        {
            string connectionString = Connection.connection;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO InternationalLicense  VALUES (@idApp, @issueDate, @expirationDate); SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@idApp", idApp);
                    command.Parameters.AddWithValue("@issueDate", issueDate);
                    command.Parameters.AddWithValue("@expirationDate", expirationDate);

                    try
                    {
                        conn.Open();
                        // ExecuteScalar returns the value of the first column of the first row in the result set
                        object result = command.ExecuteScalar();

                        // Check if the result is not null and can be converted to an integer
                        if (result != null && int.TryParse(result.ToString(), out int newId))
                        {
                            return newId;
                        }
                        else
                        {
                            // Return -1 to indicate that insertion failed
                            return -1;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the exception details
                        Console.WriteLine($"Error occurred while executing query: {ex.Message}");
                        // Optionally rethrow or handle the exception as needed
                        // throw;
                        // Return -1 to indicate failure
                        return -1;
                    }
                }
            }
        }

        public static DataTable GetAllInternationalLicenseSameUser(int idUser)
        {
            DataTable dt = new DataTable();
            string connectionString = Connection.connection;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM InternationalLicense l INNER JOIN Applications a ON l.ApplicationID = a.ApplicationID WHERE a.idUser = @idUser;";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@idUser", idUser);

                    try
                    {
                        conn.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the exception details
                        Console.WriteLine($"Error occurred while executing query: {ex.Message}");
                        // Optionally, rethrow the exception or handle it as needed
                        throw;
                    }
                }
            }
            return dt;
        }

        public static DataTable GetAllInternationalLicense()
        {
            DataTable dt = new DataTable();
            string connectionString = Connection.connection;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM InternationalLicense;";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                   
                    try
                    {
                        conn.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the exception details
                        Console.WriteLine($"Error occurred while executing query: {ex.Message}");
                        // Optionally, rethrow the exception or handle it as needed
                        throw;
                    }
                }
            }
            return dt;
        }

        public static int GetInternationalLicense(int idApp)
        {
            string connectionString = Connection.connection;
            string query = "SELECT InternationalLicenseID FROM InternationalLicense WHERE ApplicationID = @idApp";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@idApp", idApp);

                    try
                    {
                        conn.Open();
                        object result = command.ExecuteScalar();

                        // Check if the result is not null and can be converted to an integer
                        if (result != null && int.TryParse(result.ToString(), out int internationalLicenseID))
                        {
                            return internationalLicenseID;
                        }
                        else
                        {
                            // Return -1 to indicate that no matching record was found
                            return -1;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the exception details
                        Console.WriteLine($"Error occurred while executing query: {ex.Message}");
                        // Optionally, rethrow the exception or handle it as needed
                        throw;
                    }
                }
            }
        }



    }
}
