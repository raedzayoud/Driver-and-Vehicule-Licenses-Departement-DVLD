using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DataAccessDVLD
{
    public class clsStreetData
    {

        public static int GetFeesTestType3()
        {
            int Fees = -1;
            using (SqlConnection conn = new SqlConnection(Connection.connection))
            {
                string query = "select Fees from TestType where ID=3;";
                SqlCommand command = new SqlCommand(query, conn);
                try
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        //   isFound = true;
                        Fees = Convert.ToInt32(reader["Fees"]);
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception, log it, or throw it further.
                    Console.WriteLine("Error: " + ex.Message);
                    // Consider throwing the exception further so that the caller knows about the error.
                    // throw;
                }
            }

            return Fees;
        }

        public static bool InsertDataAppointements(int TestTypes, int LocalDrivingLicenseID, DateTime AppointementsDate, int PaidFees, int UserID, int isLocked)
        {
            using (SqlConnection conn = new SqlConnection(Connection.connection))
            {
                string query = @"INSERT INTO Test_Appointments VALUES (@TestTypes,@LocalDrivingLicenseID,@AppointementsDate,@PaidFees,@UserID,@isLocked,@Result,@Notes,@RetakeTetsID);";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@TestTypes", TestTypes);
                    command.Parameters.AddWithValue("@LocalDrivingLicenseID", LocalDrivingLicenseID);
                    command.Parameters.AddWithValue("@AppointementsDate", AppointementsDate);
                    command.Parameters.AddWithValue("@PaidFees", PaidFees);
                    command.Parameters.AddWithValue("@UserID", UserID);
                    command.Parameters.AddWithValue("@isLocked", isLocked);
                    command.Parameters.AddWithValue("@Result", DBNull.Value);
                    command.Parameters.AddWithValue("@Notes", DBNull.Value);
                    command.Parameters.AddWithValue("@RetakeTetsID", DBNull.Value);
                    try
                    {
                        conn.Open();
                        object result = command.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        // Log the exception details
                        Console.WriteLine($"Error occurred while executing query: {ex.Message}");
                        return false; // Return -1 to indicate failure
                    }
                }
                conn.Close();
                return true;
            }


        }

        public static DataTable GetAllTestAppointementsofTest3(int idApp, int idLicense)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(Connection.connection);
            {
             string query = @"SELECT * 
             FROM Test_Appointments t 
             INNER JOIN LocalDrivingApplications l 
             ON t.LocalDrivingLicneseApplicationID = l.LocalDrivingLicenseID 
             WHERE l.LicenseClassID = @idLicense 
             AND l.ApplicationID = @idApp 
             AND t.TestTypes = 3;";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@idLicense", idLicense);
                cmd.Parameters.Add("@idApp", idApp);
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
            conn.Close();
            return dt;


        }

        public static bool UpdatePassedTestVision(int idApp)
        {
            int result = 0;

            SqlConnection conn = new SqlConnection(Connection.connection);

            string query = @"update Applications set PassedTest=3 where ApplicationID=@idApp";

            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.Add("@idApp", idApp);
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

        public static bool UpdateTestStreet(int idApp)
        {
            string connectionString = Connection.connection;
            string query = "update Applications set paidFees = 35 where ApplicationID = @idApp";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idApp", idApp);

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0; // Return true if at least one row was affected
                }
                catch (Exception ex)
                {
                    // Log the exception or handle it as appropriate for your application
                    Console.WriteLine("An error occurred: " + ex.Message);
                    return false;
                }
            }
        }


    }
}
