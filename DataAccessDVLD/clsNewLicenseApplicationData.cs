using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DataAccessDVLD
{
    public class clsNewLicenseApplicationData
    {
        public static void GetFeesofApplicationTypes(ref int fees)
        {
            bool isFound = false;
            SqlConnection conn = new SqlConnection(Connection.connection);
            string query = "select Fees from Applicationtypes where ID=1";
            SqlCommand command = new SqlCommand(query, conn);
            try
            {
                conn.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    isFound = true;
                    fees = (int)Reader["Fees"];

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
        }

        public static bool IsUserOrNot(int idPerson)
        {
            object isFound;
            SqlConnection conn = new SqlConnection(Connection.connection);
            string query = "select found=1 from Users where idPerson=@idPerson";
            SqlCommand sqlCommand = new SqlCommand(query, conn);
            sqlCommand.Parameters.AddWithValue("@idPerson", idPerson);
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

        public static DataTable GetAllLicenseClass()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(Connection.connection);
            {
                string query = "select LicenseType from LicenseClass;";
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

        public static DataTable GetAllLicenseByUser(int idUser)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(Connection.connection))
            {
                string query = "select LicenseClassID from LocalDrivingApplications l inner join Applications a on l.ApplicationID=a.ApplicationID where a.idUser=@idUser;";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idUser", idUser);

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
                    MessageBox.Show("Error retrieving data: " + ex.Message);
                }
            }

            return dt;
        }

        public static DataTable GetallStatus(int idUser)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(Connection.connection))
            {
                string query = "SELECT Status FROM Applications WHERE idUser = @idUser;";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idUser", idUser);
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
                    MessageBox.Show("Error retrieving data: " + ex.Message);
                }
            }

            return dt;



        }

        public static int AddNewLocalLicenses(DateTime ApplicationDate, int idApplicationType, int idUser, string Status)
        {
            using (SqlConnection conn = new SqlConnection(Connection.connection))
            {
                string query = @"INSERT INTO Applications VALUES (@ApplicationDate,@idApplicationType, @idUser, @Status,@paidFees,@PassedTest); SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
                    command.Parameters.AddWithValue("@idApplicationType", idApplicationType);
                    command.Parameters.AddWithValue("@idUser", idUser);
                    command.Parameters.AddWithValue("@Status", Status);
                    command.Parameters.AddWithValue("@paidFees", 10);
                    command.Parameters.AddWithValue("@PassedTest", 0);
                    try
                    {
                        conn.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int index))
                        {
                            return index;
                        }
                        else
                        {
                            // Log the unexpected result
                            Console.WriteLine("Unexpected result returned from query.");
                            return -1;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the exception details
                        Console.WriteLine($"Error occurred while executing query: {ex.Message}");
                        return -1; // Return -1 to indicate failure
                    }
                }
            }


        }

        public static void GetIDOfLicenseClass(ref int id, string licenseClass)
        {
            SqlConnection conn = new SqlConnection(Connection.connection);
            string query = "SELECT id from LicenseClass where LicenseType =@LicenseType";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@LicenseType", licenseClass);
            try
            {
                conn.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    id = (int)Reader["id"];
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

        public static bool GETApplicationByIDApp(int idApp, ref DateTime ApplicationDate,ref int idApplicationType, ref int idUser,ref string Status)
        {
            bool isFound = false;
            SqlConnection conn = new SqlConnection(Connection.connection);
            string query = "select * from Applications where ApplicationID=@id";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@id", idApp);
            try
            {
                conn.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    isFound = true;
                    ApplicationDate = (DateTime)Reader["ApplicationDate"];
                    idApplicationType = (int)Reader["idApplicationType"];
                    idUser = (int)Reader["idUser"];
                    Status = (string)Reader["Status"];

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
}

