using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DataAccessDVLD
{
    public class clsWrittenTestData
    {
        public static bool UpdateTestTypeto2()
        {
            int result = 0;

            SqlConnection conn = new SqlConnection(Connection.connection);

            string query = @"update Test_Appointments set TestTypes=2 where Result='Pass'";
;
            SqlCommand command = new SqlCommand(query, conn);
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

        public static DataTable GetAllTestAppointementsofTest2(int idApp,int idLicense)
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
                 AND t.TestTypes = 2;";

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

        public static bool UpdatePaidFeesto20ofAppllication()
        {
            int result = 0;

            SqlConnection conn = new SqlConnection(Connection.connection);

            string query = @"update Applications  set paidFees=20  where ApplicationID in(select ApplicationID from LocalDrivingApplications where LocalDrivingLicenseID in(select LocalDrivingLicenseID from Test_Appointments where Result='Pass') ) ;";
            ;
            SqlCommand command = new SqlCommand(query, conn);
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

        public static bool UpdateResulttofail(int idApp)
        {
            int result = 0;

            SqlConnection conn = new SqlConnection(Connection.connection);
            // fix it the error of adding a writing test /* an approch idea delete this line from the database */
            string query = @"UPDATE Test_Appointments 
                 SET Result = 'Fail', isLocked = 0 
                 WHERE LocalDrivingLicneseApplicationID IN 
                       (SELECT LocalDrivingLicneseApplicationID 
                        FROM LocalDrivingApplications 
                        WHERE ApplicationID = @idApp);";
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
        
        public static bool DeleteTestAppointemnts(int idLocal)
        {
            int result = 0;
            SqlConnection conn = new SqlConnection(Connection.connection);
            string query = "delete from Test_Appointments where LocalDrivingLicneseApplicationID=@id";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@id", idLocal);
            try
            {
                conn.Open();
                result = command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                return false;
            }
            return result > 0;
        }

        public static bool GetTestType(int LocalID,ref int TestType )
        {
            bool isFound = false;
            using (SqlConnection conn = new SqlConnection(Connection.connection))
            {
                string query = "select TestTypes from Test_Appointments where LocalDrivingLicneseApplicationID=@LocalID";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@LocalID", LocalID);
                try
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        isFound = true;
                        TestType = Convert.ToInt32(reader["TestTypes"]);
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

            return isFound;
        }

        public static bool UpdatePassedTestVision(int idApp)
        {
            int result = 0;

            SqlConnection conn = new SqlConnection(Connection.connection);

            string query = @"update Applications set PassedTest=2 where ApplicationID=@idApp";

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

        




    }

}
