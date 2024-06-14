using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
namespace DataAccessDVLD
{
    public class LocalDrivingLicenseApplicationData
    {
        public static DataTable GetAllLocalDriving()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(Connection.connection);
            {
                string query = "select * from Applications a inner join Users u on a.idUser=u.idUser inner join People p on u.idPerson=p.idperson;";
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

        public static void GetLicenseClassById(int id, ref string licenseClass)
        {
            string connectionString = Connection.connection;
            string query = "select LicenseType from LicenseClass where id=@idLicense;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@idLicense", id);

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                licenseClass = reader["LicenseType"].ToString();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        // Log the exception, e.g., Console.WriteLine(e.Message);
                        // For this example, we're just returning without changing the licenseClass variable.
                    }
                }
            }
        }

        public static void GetLicenseClassById1(int id, ref string licenseClass)
        {
            SqlConnection conn = new SqlConnection(Connection.connection);
            string query = "select LicenseType from LicenseClass where id=@id";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@id", id);
            try
            {
                conn.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    licenseClass = (string)Reader["LicenseType"];
                }
            }
            catch (Exception e)
            {
                return;
            }
            finally
            {
                conn.Close();
            }
        }

        public static bool ModifySatatus(int applicationID)
        {
            int result = 0;

            SqlConnection conn = new SqlConnection(Connection.connection);

            string query = @"update Applications set
                Status=@Status
               where ApplicationID=@id";
            SqlCommand command = new SqlCommand(query, conn);

            command.Parameters.AddWithValue("@id", applicationID);
            command.Parameters.AddWithValue("@Status", "Cancelled");
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

        public static DataTable SearchApplication(string s, string Data)
        {
            string query = "";
            SqlCommand cmd = null; // Inicializar cmd fuera del bloque if
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(Connection.connection);
            try
            {
                conn.Open();
                if (s == "NationalNo")
                {
                    query = "select * from Applications a inner join LocalDrivingApplications l on a.ApplicationID=l.ApplicationID inner join Users u on a.idUser=u.idUser inner join People p on u.idPerson=p.idperson where p.NationalNo=@Data;"; // Consulta SQL con un parámetro
                    cmd = new SqlCommand(query, conn); // Usando el bloque para garantizar la liberación de recursos

                    // La conversion a réussi, vous pouvez maintenant utiliser id comme valeur du paramètre
                    cmd.Parameters.AddWithValue("@Data", Data); // Changed parameter name to match SQL query

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            dt.Load(reader);
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                // Handle the exception appropriately (e.g., throw, log)
                Console.WriteLine("Error retrieving data: " + ex.Message);
                throw; // Rethrow the exception to propagate it up the call stack
            }
            finally
            {
                conn.Close(); // Asegurar que la conexión se cierre en todas las condiciones
            }

            return dt;
        }

        public static DataTable GetAllLocalLicense(int idApp)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(Connection.connection);
            string query = "SELECT * FROM Applications WHERE ApplicationID = @idApp;";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@idApp", idApp);

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
            finally
            {
                conn.Close(); // Ensure connection is closed even if an exception occurs
            }

            return dt;
        }

        public static bool AddLocal(int ApplicationID, int LicenseID)
        {
            bool mri9il = false;
            using (SqlConnection conn = new SqlConnection(Connection.connection))
            {
                string query = @"INSERT INTO LocalDrivingApplications VALUES (@ApplicationID,@LicenseClassID);";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                    command.Parameters.AddWithValue("@LicenseClassID", LicenseID);
                    try
                    {
                        conn.Open();
                        object result = command.ExecuteScalar();
                        mri9il = true;

                    }
                    catch (Exception ex)
                    {
                        mri9il = false;
                        // Log the exception details
                        Console.WriteLine($"Error occurred while executing query: {ex.Message}");
                        // Return -1 to indicate failure
                    }
                }
            }

            return mri9il;
        }
       
        public static DataTable GetAllLocalDrivingLicense()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(Connection.connection);
            {
                string query = "select * from LocalDrivingApplications;";
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

        public static DataTable GetApplication(int idApp)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Connection.connection))
            {
                try
                {
                    conn.Open();

                    string query = "select * from LocalDrivingApplications where ApplicationID = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", idApp);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    // Log or handle the specific SQL exception
                    Console.WriteLine("Error retrieving data: " + ex.Message);
                    throw; // Rethrow the exception for further handling
                }
            }

            return dt;
        }

        public static void GetIdofLicenseByIdApp(int idApp,ref int idLicense)
        {
            SqlConnection conn = new SqlConnection(Connection.connection);
            string query = "select LicenseClassID from LocalDrivingApplications where ApplicationID=@idApp";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@idApp", idApp);
            try
            {
                conn.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    idLicense = (int)Reader["LicenseClassID"];
                }
            }
            catch (Exception e)
            {
                return;
            }
            finally
            {
                conn.Close();
            }

        }

        public static int  GetLocalDrivingIDByIdApp(int idApp)
        {
            int LocalDrivingLicenseID = 0;
            SqlConnection conn = new SqlConnection(Connection.connection);
            string query = "select LocalDrivingLicenseID from  LocalDrivingApplications where ApplicationID=@idApp";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@idApp", idApp);
            try
            {
                conn.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    LocalDrivingLicenseID = (int)Reader["LocalDrivingLicenseID"];
                    conn.Close();
                    return LocalDrivingLicenseID;
                }
            }
            catch (Exception e)
            {
                return -1;
            }
            finally
            {
                conn.Close();
            }

            return LocalDrivingLicenseID;

        }

        //First Delete;
        public static bool DeleteApplicationfromTestAppointements(int idLocal)
       {
        // Connection string - replace with your actual database connection details
        string connectionString = Connection.connection;

        // Query to delete the record
        string query = "DELETE FROM Test_Appointments WHERE LocalDrivingLicneseApplicationID = @idLocal;";

        // Use a try-catch block to handle potential database exceptions
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    // Add the parameter to the command
                    command.Parameters.AddWithValue("@idLocal", idLocal);

                    // Execute the command and check the number of affected rows
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        catch (Exception ex)
        {
            // Log the exception or handle it as needed
            Console.WriteLine("An error occurred: " + ex.Message);
            return false;
        }
    }

        //Second Delete

        public static bool DeleteApplicationfromLocal(int idLocal)
        {
            // Connection string - replace with your actual database connection details
            string connectionString = Connection.connection;

            // Query to delete the record
            string query = "delete from LocalDrivingApplications where LocalDrivingLicenseID=@idLocal;";

            // Use a try-catch block to handle potential database exceptions
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        // Add the parameter to the command
                        command.Parameters.AddWithValue("@idLocal", idLocal);

                        // Execute the command and check the number of affected rows
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
        }

        //Third Delete

        public static bool DeleteApplication(int idApp)
        {
            // Connection string - replace with your actual database connection details
            string connectionString = Connection.connection;

            // Query to delete the record
            string query = "delete from Applications where ApplicationID=@idApp;";

            // Use a try-catch block to handle potential database exceptions
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        // Add the parameter to the command
                        command.Parameters.AddWithValue("@idApp", idApp);

                        // Execute the command and check the number of affected rows
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
        }


        //Update Status to completed
        public static bool UpdateStatusToCompleted(int idApp, int idPerson)
        {
            string connectionString = Connection.connection; // Assuming Connection.connection is your connection string

            // Define the queries for updating Applications and inserting into Drivers within a transaction
            string updateQuery = "UPDATE Applications SET Status = 'Completed' WHERE ApplicationID = @idApp";
            string insertQuery = "INSERT INTO Driver VALUES (@idPerson, @idActive)";

            try
            {
                // Create a new SqlConnection within a using statement to ensure it's properly disposed of
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Open the connection
                    conn.Open();

                    // Begin a new transaction
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        // Execute the update query within the transaction
                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn, transaction))
                        {
                            updateCmd.Parameters.AddWithValue("@idApp", idApp);
                            int rowsUpdated = updateCmd.ExecuteNonQuery();

                            // Check if any rows were updated
                            if (rowsUpdated == 0)
                            {
                                // Rollback the transaction and return false
                                transaction.Rollback();
                                return false;
                            }
                        }

                        // Execute the insert query within the transaction
                        using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn, transaction))
                        {
                            insertCmd.Parameters.AddWithValue("@idPerson", idPerson);
                            insertCmd.Parameters.AddWithValue("@idActive", 1);
                            insertCmd.ExecuteNonQuery();
                        }

                        // Commit the transaction
                        transaction.Commit();

                        // Return true to indicate success
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // Log the exception
                        Console.WriteLine("An error occurred: " + ex.Message);

                        // Rollback the transaction
                        transaction.Rollback();

                        // Return false to indicate failure
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("An error occurred: " + ex.Message);

                // Return false to indicate failure
                return false;
            }
        }

        //Get Status 
        public static string GetStatusById(int idApp)
        {
            string connectionString = Connection.connection; // Assuming Connection.connection is your connection string
            string query = "SELECT Status FROM Applications WHERE ApplicationID = @idApp";

            try
            {
                // Create and open the connection within a using statement to ensure it's disposed of properly
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Create the command with the query and connection
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add the parameter to the command
                        cmd.Parameters.AddWithValue("@idApp", idApp);

                        // Open the connection
                        conn.Open();

                        // Execute the command and retrieve the status
                        object result = cmd.ExecuteScalar();

                        // Check if the result is not null and return the status
                        if (result != null)
                        {
                            return result.ToString();
                        }
                        else
                        {
                            // Log the unexpected result
                            Console.WriteLine("No status found for the given ApplicationID.");
                            return null; // or return a specific value indicating not found, e.g., "Not Found"
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine("An error occurred: " + ex.Message);
                return null; // or return a specific value indicating an error, e.g., "Error"
            }
        }

        public static int GetIDPersonByIdApp(int idApp)
        {
            int idPerson = -1; // default value if no person is found

            // Use using statement to ensure proper disposal of SqlConnection
            using (SqlConnection conn = new SqlConnection(Connection.connection))
            {
                // Open the connection
                conn.Open();

                // Create the SqlCommand with parameters
                string query = "SELECT idPerson FROM Users u INNER JOIN Applications a ON u.idUser = a.idUser WHERE a.ApplicationID = @idApp;";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@idApp", idApp);

                // Execute the query
                SqlDataReader reader = command.ExecuteReader();

                // Check if there are rows returned
                if (reader.Read())
                {
                    // Retrieve the idPerson from the first row
                    idPerson = reader.GetInt32(0);
                }

                // Close the reader
                reader.Close();
            }

            return idPerson;
        }


    }

}