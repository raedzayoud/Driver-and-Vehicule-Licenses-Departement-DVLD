using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessDVLD
{
    // Update to Note in License and insert the idApp 
    public class clsLicenseData
    {
        public static int InsertData(string note, int idApp, DateTime issueDate, DateTime expirationDate, bool isActive, string issueReason, bool isDetainted)
        {
            string connectionString = Connection.connection; // Assuming Connection.connection is your connection string

            // Define the query with parameters and a statement to return the new identity value
            string query = "INSERT INTO License " +
                           "VALUES (@AppID, @IssueDate, @ExpirationDate, @Note, @IsActive, @IssueReason, @IsDetainted); " +
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
                        cmd.Parameters.Add("@AppID", SqlDbType.Int).Value = idApp;
                        cmd.Parameters.Add("@IssueDate", SqlDbType.DateTime).Value = issueDate;
                        cmd.Parameters.Add("@ExpirationDate", SqlDbType.DateTime).Value = expirationDate;
                        cmd.Parameters.Add("@Note", SqlDbType.NVarChar).Value = note;
                        cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = isActive;
                        cmd.Parameters.Add("@IssueReason", SqlDbType.NVarChar).Value = issueReason;
                        cmd.Parameters.Add("@IsDetainted", SqlDbType.Bit).Value = isDetainted;

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

        public static int GetLicenseID(int idApp)
        {
            int licenseID = -1; // default value if no license is found

            // Use using statement to ensure proper disposal of SqlConnection
            using (SqlConnection conn = new SqlConnection(Connection.connection))
            {
                conn.Open(); // Open the connection

                // Create the SqlCommand with parameters
                string query = "SELECT LicenseID FROM License WHERE AppID = @idApp;";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@idApp", idApp);

                // Execute the query
                SqlDataReader reader = command.ExecuteReader();

                // Check if there are rows returned
                if (reader.Read())
                {
                    // Retrieve the LicenseID from the first row
                    licenseID = reader.GetInt32(0);
                }

                // Close the reader
                reader.Close();
            }

            return licenseID;
        }

        public static DateTime GetAppointmentDate(int idLocal)
        {
            DateTime appointmentDate = DateTime.MinValue; // default value if no appointment date is found

            // Use using statement to ensure proper disposal of SqlConnection
            using (SqlConnection conn = new SqlConnection(Connection.connection))
            {
                conn.Open(); // Open the connection

                // Create the SqlCommand with parameters
                string query = "SELECT AppointementDate FROM Test_Appointments t INNER JOIN LocalDrivingApplications l ON t.LocalDrivingLicneseApplicationID = l.LocalDrivingLicenseID WHERE l.LocalDrivingLicenseID = @idLocal;";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@idLocal", idLocal);

                // Execute the query
                SqlDataReader reader = command.ExecuteReader();

                // Check if there are rows returned
                if (reader.Read())
                {
                    // Retrieve the appointment date from the first row
                    appointmentDate = reader.GetDateTime(0);
                }

                // Close the reader
                reader.Close();
            }

            return appointmentDate;
        }

        public static bool DeleteLicenseFromApplication(int idApp)
        {
            // Flag to track whether deletion was successful
            bool isDeleted = false;

            // Use using statement to ensure proper disposal of SqlConnection
            using (SqlConnection conn = new SqlConnection(Connection.connection))
            {
                // Open the connection
                conn.Open();

                // Create the SqlCommand with parameters
                string query = "DELETE FROM License WHERE AppID = @idApp;";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@idApp", idApp);

                // Execute the query
                int rowsAffected = command.ExecuteNonQuery();

                // Check if any rows were affected (deletion was successful)
                if (rowsAffected > 0)
                {
                    isDeleted = true;
                }
            }

            return isDeleted;
        }

        public static DataTable GetAllLicenseSameUser(int idUser)
        {
            // Define the connection string and the query
            string connectionString = Connection.connection;
            string query = "select * from License l inner join Applications a on l.AppID=a.ApplicationID where a.idUser=@idUser;";

            // Create a new DataTable to hold the results
            DataTable dataTable = new DataTable();

            // Use using statements to ensure resources are disposed of properly
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    sqlConnection.Open();

                    // Create a SqlCommand object
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        // Add the parameter to the command
                        sqlCommand.Parameters.AddWithValue("@idUser", idUser);

                        // Create a SqlDataAdapter to fill the DataTable
                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                        {
                            // Fill the DataTable with the results
                            sqlDataAdapter.Fill(dataTable);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions (you can log the error or rethrow the exception as needed)
                    throw new Exception("An error occurred while fetching the licenses.", ex);
                }
            }

            // Return the filled DataTable
            return dataTable;
        }

        public static DataTable GetAllLicense()
        {
            // Define the connection string and the query
            string connectionString = Connection.connection;
            string query = "select * from License;";

            // Create a new DataTable to hold the results
            DataTable dataTable = new DataTable();

            // Use using statements to ensure resources are disposed of properly
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    sqlConnection.Open();

                    // Create a SqlCommand object
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        // Create a SqlDataAdapter to fill the DataTable
                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                        {
                            // Fill the DataTable with the results
                            sqlDataAdapter.Fill(dataTable);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions (you can log the error or rethrow the exception as needed)
                    throw new Exception("An error occurred while fetching the licenses.", ex);
                }
            }

            // Return the filled DataTable
            return dataTable;
        }

        public static DataTable GetLicenseByAppId(int idApp)
        {
            // Define the connection string and the query
            string connectionString = Connection.connection;
            string query = "SELECT * FROM License WHERE AppID = @idApp and isActive=1;";

            // Create a new DataTable to hold the results
            DataTable dataTable = new DataTable();

            // Use using statements to ensure resources are disposed of properly
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    sqlConnection.Open();

                    // Create a SqlCommand object
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        // Add the parameter to the SqlCommand
                        sqlCommand.Parameters.AddWithValue("@idApp", idApp);

                        // Create a SqlDataAdapter to fill the DataTable
                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                        {
                            // Fill the DataTable with the results
                            sqlDataAdapter.Fill(dataTable);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions (you can log the error or rethrow the exception as needed)
                    throw new Exception("An error occurred while fetching the licenses.", ex);
                }
            }

            // Return the filled DataTable
            return dataTable;
        }

        public static DataTable GetLicenseByAppIdAnyActive(int idApp)
        {

            // Define the connection string and the query
            string connectionString = Connection.connection;
            string query = "SELECT * FROM License WHERE AppID = @idApp;";

            // Create a new DataTable to hold the results
            DataTable dataTable = new DataTable();

            // Use using statements to ensure resources are disposed of properly
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    sqlConnection.Open();

                    // Create a SqlCommand object
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        // Add the parameter to the SqlCommand
                        sqlCommand.Parameters.AddWithValue("@idApp", idApp);

                        // Create a SqlDataAdapter to fill the DataTable
                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                        {
                            // Fill the DataTable with the results
                            sqlDataAdapter.Fill(dataTable);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions (you can log the error or rethrow the exception as needed)
                    throw new Exception("An error occurred while fetching the licenses.", ex);
                }
            }
        

            // Return the filled DataTable
            return dataTable;
        

        }

        public static bool UpdateisActivetoFalse(int idApp)
        {
            string connectionString = Connection.connection;
            string query = "UPDATE License SET isActive = @isActive WHERE AppID = @idApp";

            try
            {
                // Using statements ensure the connection and command are properly disposed of
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Set the parameters
                        cmd.Parameters.AddWithValue("@isActive", false);
                        cmd.Parameters.AddWithValue("@idApp", idApp);

                        // Open the connection
                        conn.Open();

                        // Execute the command
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Return true if one or more rows were updated
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false; // Return false to indicate failure
            }
        }


        public static DataTable GetLicenseByLicenseID(int idLicense)
        {
            using (SqlConnection conn = new SqlConnection(Connection.connection))
            {
                string query = "SELECT * FROM License WHERE LicenseID=@idLic";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idLic", idLicense);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt;
                }
            }
        }

        public static bool UpdateDetaintotrue(int idLicense)
        {
            string connectionString = Connection.connection;
            string query = "UPDATE License SET isDetainted = @isActive WHERE LicenseID = @LicenseID";

            try
            {
                // Using statements ensure the connection and command are properly disposed of
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Set the parameters
                        cmd.Parameters.AddWithValue("@isActive", true);
                        cmd.Parameters.AddWithValue("@LicenseID", idLicense);  // Correct parameter name

                        // Open the connection
                        conn.Open();

                        // Execute the command
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Return true if one or more rows were updated
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false; // Return false to indicate failure
            }
        }

        public static bool UpdateDetaintofalse(int idLicense)
        {
            string connectionString = Connection.connection;
            string query = "UPDATE License SET isDetainted = @isActive WHERE LicenseID = @LicenseID";

            try
            {
                // Using statements ensure the connection and command are properly disposed of
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Set the parameters
                        cmd.Parameters.AddWithValue("@isActive", false);
                        cmd.Parameters.AddWithValue("@LicenseID", idLicense);  // Correct parameter name

                        // Open the connection
                        conn.Open();

                        // Execute the command
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Return true if one or more rows were updated
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false; // Return false to indicate failure
            }
        }



    }
}


