using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessDVLD
{
    public class DetainData
    {
        public static int InsertData(int LicenseID, DateTime DebutDate, int FineFees)
        {
            string connectionString = Connection.connection; // Assuming Connection.connection is your connection string

            // Define the query with parameters and a statement to return the new identity value
            string query = "INSERT INTO Detain (LicenseID, DebutDate, FineFees) " +
                           "VALUES (@LicenseID, @DebutDate, @FineFees); " +
                           "SELECT SCOPE_IDENTITY();";

            try
            {
                // Create and open the connection within a using statement to ensure it's disposed of properly
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Create the command with the query and connection
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add the parameters to the command with explicit types
                        cmd.Parameters.Add("@LicenseID", SqlDbType.Int).Value = LicenseID;
                        cmd.Parameters.Add("@DebutDate", SqlDbType.DateTime).Value = DebutDate;
                        cmd.Parameters.Add("@FineFees", SqlDbType.Int).Value = FineFees;

                        // Open the connection
                        conn.Open();

                        // Execute the command and get the inserted ID
                        object result = cmd.ExecuteScalar();

                        // Check if the result is valid and return the new ID
                        if (result != null && int.TryParse(result.ToString(), out int newId))
                        {
                            return newId;
                        }
                        else
                        {
                            // Log the unexpected result
                            Console.WriteLine("Unexpected result returned from query.");
                            return -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"An error occurred: {ex.Message}");
                return -1; // Return -1 to indicate failure
            }
        }

        public static DataTable GetInfoDetainLicense(int idLicense)
        {
            // Create a new DataTable to hold the query results
            DataTable dt = new DataTable();

            // Define the connection string (assumed to be in Connection.connection)
            string connectionString = Connection.connection;

            // Define the query string with a parameter placeholder
            string query = "SELECT top 1 * FROM Detain WHERE LicenseID =@idLicense order by DetainID desc ";

            try
            {
                // Create and open the connection within a using statement to ensure it's disposed of properly
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Create the command with the query and connection
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add the parameter to the command
                        cmd.Parameters.Add("@idLicense", SqlDbType.Int).Value = idLicense;

                        // Create a SqlDataAdapter to execute the command and fill the DataTable
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            // Open the connection
                            conn.Open();

                            // Fill the DataTable with the results of the query
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception details (for example, to a file or logging system)
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            // Return the filled DataTable
            return dt;
        }

        public static DataTable GetAllDetain()
        {
            // Create a new DataTable to hold the query results
            DataTable dt = new DataTable();

            string connectionString = Connection.connection; // Assuming Connection.connection is your connection string

            // Define the query string
            string query = "select * from Detain";

            try
            {
                // Create and open the connection within a using statement to ensure it's disposed of properly
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Create the command with the query and connection
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Create a SqlDataAdapter to execute the command and fill the DataTable
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            // Open the connection
                            conn.Open();

                            // Fill the DataTable with the results of the query
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            // Return the filled DataTable
            return dt;
        }

        public static bool IsFoundActive(int idLicense)
        {
            string connectionString = Connection.connection; // Assuming Connection.connection is your connection string
            string query = "SELECT isActive FROM License WHERE LicenseID = @id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", idLicense);
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            return Convert.ToBoolean(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception as needed (e.g., log it)
                    Console.WriteLine(ex.Message);
                }
            }

            return false; // Return false if not found or in case of an error
        }





    }
}
