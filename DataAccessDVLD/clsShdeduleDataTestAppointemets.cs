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
    public class clsShdeduleDataTestAppointemets
    {
        public static bool AddNewAppointements(int TestTypes, int LocalDrivingLicenseID, DateTime AppointementsDate, int PaidFees, int UserID, int isLocked)
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
                    command.Parameters.AddWithValue("@RetakeTetsID",DBNull.Value); 
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

        public static bool updateRetakeTestID(int idRetakeTest)
        {
            int result = 0;

            SqlConnection conn = new SqlConnection(Connection.connection);

            string query = @"UPDATE Test_Appointments SET 
                RetakeTetsID=@idRetakeTest
                WHERE TestAppointementsID = @TestAppointementsID;";

            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.Add("@idRetakeTest", idRetakeTest);
            command.Parameters.Add("@TestAppointementsID", idRetakeTest);
            try
            {
                conn.Open();
                result = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                return false;
            }
            return result > 0;



        }

        public static int SelectLastTestAppointementsID()
        {
            int TestAppointementsID = -1;

            using (SqlConnection conn = new SqlConnection(Connection.connection))
            {
                string query = "SELECT MAX(TestAppointementsID) AS LastTestAppointementsID FROM Test_Appointments;";
                SqlCommand command = new SqlCommand(query, conn);
                try
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read() && !reader.IsDBNull(0)) // Check if there is a result and it's not null
                    {
                        TestAppointementsID = reader.GetInt32(0); // Get the value from the first column
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception
                    Console.WriteLine(ex.Message);
                }
            }
            return TestAppointementsID;
        }

        public static int SelectLastTestAppointementsIDBYTESTAPOINTEMENTS(int LocalID)
        {
            int TestAppointementsID = -1;

            using (SqlConnection conn = new SqlConnection(Connection.connection))
            {
                string query = "select max(TestAppointementsID) from Test_Appointments t inner join LocalDrivingApplications l on t.LocalDrivingLicneseApplicationID=l.LocalDrivingLicenseID where l.LocalDrivingLicenseID=@idLocal;";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add("@idLocal", LocalID);
                try
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read() && !reader.IsDBNull(0)) // Check if there is a result and it's not null
                    {
                        TestAppointementsID = reader.GetInt32(0); // Get the value from the first column
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception
                    Console.WriteLine(ex.Message);
                }
            }
            return TestAppointementsID;


        }

        public static int GetFees(int TestType)
        {
            int Fees = 0;
            SqlConnection conn = new SqlConnection(Connection.connection);
            string query = "select Fees from TestType where ID=@TestID";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@TestID", TestType);
            try
            {
                conn.Open();
                SqlDataReader sqlDataReader = command.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    Fees = (int)sqlDataReader["Fees"];
                }

            }
            catch (Exception e)
            {
                return -1;
            }
            conn.Close();
            return Fees;


        }

        public static DataTable GetAllAppointement(int  idLicense, int idApp)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(Connection.connection);
            {
                string query = "select * from Test_Appointments t inner join LocalDrivingApplications l on t.LocalDrivingLicneseApplicationID=l.LocalDrivingLicenseID where l.LicenseClassID=@idLicense and l.ApplicationID=@idApp;";
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

        public static bool isFound(int idApp)
        {
            object isFound;
            SqlConnection conn = new SqlConnection(Connection.connection);
            string query = "select found=1 from Test_Appointments t inner join LocalDrivingApplications l on t.LocalDrivingLicneseApplicationID=l.LocalDrivingLicenseID where l.ApplicationID=@idApp;";
            SqlCommand sqlCommand = new SqlCommand(query, conn);
            sqlCommand.Parameters.AddWithValue("@idApp", idApp);
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

        public static bool UpdateApponitemnts(DateTime AppointementDate, int TestAppointementsID)
        {
            int result = 0;

            SqlConnection conn = new SqlConnection(Connection.connection);

            string query = @"UPDATE Test_Appointments SET 
                  AppointementDate = @AppointementDate 
                WHERE TestAppointementsID = @TestAppointementsID;";

            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.Add("@AppointementDate", AppointementDate);
            command.Parameters.Add("@TestAppointementsID", TestAppointementsID);
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

        public static bool updateResultNote(int testAppointmentID, string note, string results)
        {
            int result = 0;

            SqlConnection conn = new SqlConnection(Connection.connection);

            string query = @"UPDATE Test_Appointments SET 
                  Result = @results,
                  Notes=@note,
                  isLocaked=1
                  WHERE TestAppointementsID = @testAppointmentID;";

            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.Add("@results", results);
            command.Parameters.Add("@note", note);
            command.Parameters.Add("@testAppointmentID", testAppointmentID);
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

        public static bool GetResult(int TestAppointementsID, ref string Result)
        {
            SqlConnection conn = new SqlConnection(Connection.connection);
            {
                bool isFound = false;
                string query = "select Result from Test_Appointments where TestAppointementsID=@TestAppointementsID;";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@TestAppointementsID", TestAppointementsID);
                try
                {
                    conn.Open();
                    SqlDataReader Reader = command.ExecuteReader();
                    if (Reader.Read())
                    {
                        isFound = true;
                        Result = (string)Reader["Result"];
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

        }

        public static bool GETLocaked(int TestAppointementsID)
        {
            bool isLockaed;

            SqlConnection conn = new SqlConnection(Connection.connection);
            {
                string query = "select isLocaked from Test_Appointments where TestAppointementsID=@TestAppointementsID;";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@TestAppointementsID", TestAppointementsID);
                try
                {
                    conn.Open();
                    SqlDataReader Reader = command.ExecuteReader();
                    if (Reader.Read())
                    {
                        isLockaed = (bool)Reader["isLocaked"];
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    // Console.WriteLine(ex.Message);
                    return false;
                }
                conn.Close();
                return isLockaed;
            }
        }

        public static string GetResult(int idTest)
        {
            string Result;

            SqlConnection conn = new SqlConnection(Connection.connection);
            {
                string query = "select Result from Test_Appointments where TestAppointementsID=@idTest;";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@idTest", idTest);
                try
                {
                    conn.Open();
                    SqlDataReader Reader = command.ExecuteReader();
                    if (Reader.Read())
                    {
                        Result = (string)Reader["Result"];
                    }
                    else
                    {
                        return "";
                    }
                }
                catch (Exception ex)
                {
                    // Console.WriteLine(ex.Message);
                    return "";
                }
                conn.Close();
                return Result;
            }

        }
        public static DataTable GetResults(int idTest)
        {
            DataTable resultTable = new DataTable();

            using (SqlConnection conn = new SqlConnection(Connection.connection))
            {
                string query = "SELECT Result FROM Test_Appointments WHERE LocalDrivingLicneseApplicationID = @idTest";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@idTest", idTest);

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            resultTable.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle the exception as needed, e.g., log the error
                        // For this example, we'll return an empty DataTable in case of an error
                        return new DataTable();
                    }
                }
            }

            return resultTable;
        }

        public static int GetFeesofRetakeTest(int TestAppointementsID) {

            int fees=0;

            SqlConnection conn = new SqlConnection(Connection.connection);
            {
                string query = "select PaisFees from Test_Appointments where TestAppointementsID=@idTest;";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@idTest", TestAppointementsID);
                try
                {
                    conn.Open();
                    SqlDataReader Reader = command.ExecuteReader();
                    if (Reader.Read())
                    {
                        fees = (int)Reader["PaisFees"];
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
                return fees;
            }



        }

        public static int GetLocalIDByAppID(int idApp)
        {
            int LocalID;

            SqlConnection conn = new SqlConnection(Connection.connection);
            {
                string query = "select LocalDrivingLicenseID from LocalDrivingApplications where ApplicationID=@idApp;";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@idApp", idApp);
                try
                {
                    conn.Open();
                    SqlDataReader Reader = command.ExecuteReader();
                    if (Reader.Read())
                    {
                        LocalID = (int)Reader["LocalDrivingLicenseID"];
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
                return LocalID;
            }


        }

        public static int GetPassedTest(int _idApp)
        {
            int PassedTest;

            SqlConnection conn = new SqlConnection(Connection.connection);
            {
                string query = "select PassedTest from Applications where ApplicationID=@idApp;";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@idApp", _idApp);
                try
                {
                    conn.Open();
                    SqlDataReader Reader = command.ExecuteReader();
                    if (Reader.Read())
                    {
                        PassedTest = (int)Reader["PassedTest"];
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
                return PassedTest;
            }


        }
      
        // Updated TestTypes=2 in wrriten Test
        public static bool UpdatePassedTestVision(int idApp)
        {
            int result = 0;

            SqlConnection conn = new SqlConnection(Connection.connection);

            string query = @"update Applications set PassedTest=1 where ApplicationID=@idApp";

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


    }
}
