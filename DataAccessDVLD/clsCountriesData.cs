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
    public  class clsCountriesData
    {
        public static DataTable GetAllCountries()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(Connection.connection))
            {
                string query = "select * from Countries;";
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
                        // Handle the exception appropriately (e.g., log)
                        Console.WriteLine("Error retrieving data: " + ex.Message);
                        throw; // Rethrow the exception to propagate it up the call stack
                    }
                }

                conn.Close();
                return dt;
            }
        }

        public static bool GetIDOftheCountry(string name, ref int id)
        {
            bool isFound = false;
            SqlConnection conn = new SqlConnection(Connection.connection);
            string query = "select CountryID from Countries where countryName=@name";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@name", name);
            try
            {
                conn.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    isFound = true;
                    id = (int)Reader["CountryID"];
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


        public static bool FindCountryNameByID(int id , ref string name)
        {
            bool isFound = false;
            SqlConnection conn = new SqlConnection(Connection.connection);
            string query = "select * from Countries where CountryID=@id";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@id", id);
            try
            {
                conn.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    isFound = true;
                    name = (string)Reader["countryName"];
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
