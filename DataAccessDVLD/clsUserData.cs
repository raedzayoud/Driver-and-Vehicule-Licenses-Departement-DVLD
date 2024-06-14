using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace DataAccessDVLD
{
    public class clsUserData
    {

        public static bool GetUserByID(int idUser, ref int personID, ref string username,
   ref string password, ref int isActive, ref string fullName)
        {
            bool isFound = false;
            using (SqlConnection conn = new SqlConnection(Connection.connection))
            {
                string query = "SELECT * FROM Users WHERE idUser = @idUser";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@idUser", idUser);

                try
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        isFound = true;
                        personID = Convert.ToInt32(reader["idPerson"]);
                        username = Convert.ToString(reader["username"]);
                        password = Convert.ToString(reader["password"]);
                        isActive = Convert.ToInt32(reader["isActive"]);
                        fullName = Convert.ToString(reader["FullName"]);
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


        public static bool GetUserByIDPerson(ref int idUser, int personID, ref string username,
  ref string password, ref int isActive, ref string fullName)
        {
            bool isFound = false;
            using (SqlConnection conn = new SqlConnection(Connection.connection))
            {
                string query = "SELECT * FROM Users WHERE idPerson = @idPerson";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@idPerson", personID);

                try
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        isFound = true;
                        idUser = Convert.ToInt32(reader["idUser"]);
                        username = Convert.ToString(reader["username"]);
                        password = Convert.ToString(reader["password"]);
                        isActive = Convert.ToInt32(reader["isActive"]);
                        fullName = Convert.ToString(reader["FullName"]);
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



        public static int AddNewUser(int idPerson, string username, string password, int isActive, string fullName)
        {
            // Using a using statement ensures proper disposal of resources
            using (SqlConnection conn = new SqlConnection(Connection.connection))
            {
                string query = @"INSERT INTO  Users  VALUES (@idPerson, @username, @password, @isActive, @fullName); SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@idPerson", idPerson);
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@isActive", isActive);
                    command.Parameters.AddWithValue("@fullName", fullName);
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


            public static bool UpdateUser(int idUser, string username,
           string password, int bit,string fullname)
        {
            int result = 0;

            SqlConnection conn = new SqlConnection(Connection.connection);

            string query = @"update Users set
               username=@username,
               password=@password,
               isActive=@bit,
               fullName=@fullName
               where idUser=@idUser";
            SqlCommand command = new SqlCommand(query, conn);

            command.Parameters.AddWithValue("@idUser", idUser);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);
            command.Parameters.AddWithValue("@bit", bit);
            command.Parameters.AddWithValue("@fullName", fullname);
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

        public static bool Updatepassword(int idUser, string password)
        {
            int result = 0;

            SqlConnection conn = new SqlConnection(Connection.connection);

            string query = @"update Users set
               password=@password
               where idUser=@idUser";
            SqlCommand command = new SqlCommand(query, conn);

            command.Parameters.AddWithValue("@idUser", idUser);
            command.Parameters.AddWithValue("@password", password);
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

        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(Connection.connection);
            string query = "select idUser,idPerson,FullName,Username,isActive from Users";
            SqlCommand cmd = new SqlCommand(query, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
                //connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            conn.Close();
            return dt;
        }

        public static bool deleteUser(int id)
        {
            int result = 0;
            SqlConnection conn = new SqlConnection(Connection.connection);
            string query = "delete from Users where idUser=@id";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@id", id);
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

        public static bool isExistUser(int id)
        {
            object isFound;
            SqlConnection conn = new SqlConnection(Connection.connection);
            string query = "select found=1 from Users where idUser=@id";
            SqlCommand sqlCommand = new SqlCommand(query, conn);
            sqlCommand.Parameters.AddWithValue("@id", id);
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

        public static DataTable SearchData(string s, string Data)
        {
            string query = "";
            SqlCommand cmd = null; // Inicializar cmd fuera del bloque if
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(Connection.connection);

            try
            {
                conn.Open();
                if (s == "idPerson")
                {
                    query = "SELECT * FROM Users WHERE idPerson = @id;"; // Consulta SQL con un parámetro
                    cmd = new SqlCommand(query, conn); // Usando el bloque para garantizar la liberación de recursos
                    int id;
                    if (int.TryParse(Data, out id))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                    }
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            dt.Load(reader);
                        }
                    }

                }
                else if (s == "idUser")
                {
                    int id = 0;
                    query = "SELECT * FROM Users WHERE idUser = @Data;"; // Consulta SQL con un parámetro
                    cmd = new SqlCommand(query, conn); // Usando el bloque para garantizar la liberación de recursos
                    if (int.TryParse(Data, out id))
                    {
                        cmd.Parameters.AddWithValue("@Data", id);
                    }
                 
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            dt.Load(reader);
                        }
                    }


                }
                else if (s == "username")
                {
                    query = "SELECT * FROM Users WHERE username = @Data;"; // Consulta SQL con un parámetro
                    cmd = new SqlCommand(query, conn); // Usando el bloque para garantizar la liberación de recursos

                    cmd.Parameters.AddWithValue("@Data", Data);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            dt.Load(reader);
                        }
                    }


                }
                else if (s == "password")
                {
                    query = "SELECT * FROM Users WHERE password = @Data;"; // Consulta SQL con un parámetro
                    cmd = new SqlCommand(query, conn); // Usando el bloque para garantizar la liberación de recursos

                    cmd.Parameters.AddWithValue("@Data", Data);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            dt.Load(reader);
                        }
                    }


                }
                else if (s == "isActive")
                {
                    int id = 0;
                    query = "SELECT * FROM Users WHERE isActive = @Data;"; // Consulta SQL con un parámetro
                    cmd = new SqlCommand(query, conn); // Usando el bloque para garantizar la liberación de recursos
                    if (int.TryParse(Data, out id))
                    {
                        cmd.Parameters.AddWithValue("@Data", id);
                    }
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
             //   throw; // Rethrow the exception to propagate it up the call stack
            }
            finally
            {
                conn.Close(); // Asegurar que la conexión se cierre en todas las condiciones
            }

            return dt;
        }

        public static DataTable GereCombox(string s)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection(Connection.connection);

            try
            {
                conn.Open();
                if (s == "ALL")
                {
                    string query = "SELECT * FROM Users;"; // Consulta SQL con un parámetro
                    SqlCommand cmd = new SqlCommand(query, conn); // Usando el bloque para garantizar la liberación de recursos

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            dt.Load(reader);
                        }
                    }

                }
                else if (s == "YES")
                {
                    int id = 0;
                    string query = "SELECT * FROM Users WHERE isActive=1;"; // Consulta SQL con un parámetro
                    SqlCommand cmd = new SqlCommand(query, conn); // Usando el bloque para garantizar la liberación de recursos
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            dt.Load(reader);
                        }
                    }


                }
                else if (s == "NO")
                {
                    string query = "SELECT * FROM Users WHERE isActive=0;"; // Consulta SQL con un parámetro
                    SqlCommand cmd = new SqlCommand(query, conn); // Usando el bloque para garantizar la liberación de recursos

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
                //   throw; // Rethrow the exception to propagate it up the call stack
            }
            finally
            {
                conn.Close(); // Asegurar que la conexión se cierre en todas las condiciones
            }

            return dt;


        }

        



    }
}
