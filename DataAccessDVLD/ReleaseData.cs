using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessDVLD
{
    public class ReleaseData
    {
        public static int InsertData(int DetainID, bool isRelease, DateTime ReleaseDate)
        {
            string connectionString = Connection.connection; // Assuming Connection.connection is your connection string

            // Define the query with parameters
            string query = "INSERT INTO ReleaseApp VALUES (@DetainID, @isRelease, @ReleaseDate); SELECT SCOPE_IDENTITY();";

            try
            {
                // Create and open the connection within a using statement to ensure it's disposed of properly
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Create the command with the query and connection
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add the parameters to the command with explicit types
                        cmd.Parameters.Add("@DetainID", SqlDbType.Int).Value = DetainID;
                        cmd.Parameters.Add("@isRelease", SqlDbType.Bit).Value = isRelease;
                        cmd.Parameters.Add("@ReleaseDate", SqlDbType.DateTime).Value = ReleaseDate;

                        // Open the connection
                        conn.Open();

                        // Execute the command and retrieve the inserted ID
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

        public static DataTable GetAllRelease(int DetainID)
        {
            // Create a new DataTable to hold the query results
            DataTable dt = new DataTable();

            string connectionString = Connection.connection; // Assuming Connection.connection is your connection string

            // Define the query string
            string query = "SELECT * FROM ReleaseApp WHERE DetainID = @DetainID";

            try
            {
                // Create and open the connection within a using statement to ensure it's disposed of properly
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Create the command with the query and connection
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add the parameter to the command
                        cmd.Parameters.Add("@DetainID", SqlDbType.Int).Value = DetainID;

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

    }
}
