using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessDVLD
{
    public class clsDriverData
    {
        public static DataTable GetAllDriver()
        {
            // Define the connection string (consider moving this to a configuration file)
            string connectionString = Connection.connection;

            // Define the query to be executed
            string query = "SELECT * FROM Driver";

            // Create a DataTable to hold the query results
            DataTable dataTable = new DataTable();

            // Use a using statement to ensure the SqlConnection is properly disposed of
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    conn.Open();

                    // Create a SqlCommand to execute the query
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Create a SqlDataAdapter to fill the DataTable
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            // Fill the DataTable with the results of the query
                            adapter.Fill(dataTable);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception (log it, rethrow it, or handle it as needed)
                    throw new ApplicationException("An error occurred while retrieving the licenses.", ex);
                }
                // No need to explicitly close the connection, it's handled by the using statement
            }

            // Return the filled DataTable
            return dataTable;
        }
    }
}
