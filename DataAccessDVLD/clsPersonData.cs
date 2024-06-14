using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Cryptography.X509Certificates;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Net;

namespace DataAccessDVLD
{
    public  class clsPersonData
    {

        public static bool GETPersonByID(int id, ref string firstname, ref string lastname, ref string secondname,
           ref string NationalNo, ref DateTime DateofBirth, ref string Gender, ref string address, ref string Phone,
           ref string Email, ref int NationalityCountry, ref string ImagePath)
        {
            bool isFound = false;
            SqlConnection conn = new SqlConnection(Connection.connection);
            string query = "select * from People where idPerson=@id";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@id", id);
            try
            {
                conn.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    isFound = true;
                    firstname = (string)Reader["FirstName"];
                    lastname = (string)Reader["LastName"];
                    Email = (string)Reader["Email"];
                    Phone = (string)Reader["Phone"];
                    address = (string)Reader["Address"];
                    DateofBirth = (DateTime)Reader["DateOfBirth"];
                    NationalityCountry = (int)Reader["nationalityCountry"];
                    ImagePath = (string)Reader["ImagePath"];
                    
                    NationalNo = (string)Reader["NationalNo"];
                    secondname = (string)Reader["secondname"];
                    Gender = (string)Reader["Gender"];
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


        public static bool GETPersonByNationality(int id, ref string firstname, ref string lastname, ref string secondname,
          ref string NationalNo, ref DateTime DateofBirth, ref string Gender, ref string address, ref string Phone,
           ref string Email, ref int NationalityCountry, ref string ImagePath)
        {
            bool isFound = false;
            SqlConnection conn = new SqlConnection(Connection.connection);
            string query = "select * from People where NationalNo=@NationalNo";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            try
            {
                conn.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    isFound = true;
                    id = (int)Reader["idPerson"];
                    firstname = (string)Reader["FirstName"];
                    lastname = (string)Reader["LastName"];
                    Email = (string)Reader["Email"];
                    Phone = (string)Reader["Phone"];
                    address = (string)Reader["Address"];
                    DateofBirth = (DateTime)Reader["DateOfBirth"];
                    NationalityCountry = (int)Reader["CountryID"];
                    ImagePath = (string)Reader["ImagePath"];
                    //  NationalNo=(string)Reader["NationalNo"];
                    secondname = (string)Reader["secondname"];

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


    public static int AddNewPerson(string firstname, string lastname, string secondname,
    string NationalNo, DateTime DateofBirth, string Gender, string address, string Phone,
    string Email, int NationalityCountry, string ImagePath)
        {
            // Using a using statement ensures proper disposal of resources
            using (SqlConnection conn = new SqlConnection(Connection.connection))
            {
                string query = @"INSERT INTO People VALUES (@firstname, @secondname, @lastname, @NationalNo, @DateofBirth, @Gender, @address, @Phone, @Email, @ImagePath, @NationalityCountry); SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@firstname", firstname);
                    command.Parameters.AddWithValue("@secondname", secondname);
                    command.Parameters.AddWithValue("@lastname", lastname);
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);
                    command.Parameters.AddWithValue("@DateofBirth", DateofBirth);
                    command.Parameters.AddWithValue("@Gender", Gender);
                    command.Parameters.AddWithValue("@address", address);
                    command.Parameters.AddWithValue("@Phone", Phone);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@NationalityCountry", NationalityCountry);
                    command.Parameters.AddWithValue("@ImagePath", ImagePath);

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


    public static bool UpdatePerson(int id, string firstname, string lastname, string secondname,
           string NationalNo, DateTime DateofBirth, string Gender, string address, string Phone,
           string Email, int NationalityCountry, string ImagePath)
        {
            int result = 0;

            SqlConnection conn = new SqlConnection(Connection.connection);

            string query = @"update People set
               firstname=@firstname,
               lastname=@lastname,
               secondname=@secondname,
               NationalNo=@NationalNo,
               DateofBirth=@DateofBirth,
               Gender=@Gender,
               address=@address,
               Phone=@Phone,
               Email=@Email,  
               NationalityCountry=@NationalityCountry,
               ImagePath=@ImagePath where idPerson=@id";
            SqlCommand command = new SqlCommand(query, conn);

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@firstname", firstname);
            command.Parameters.AddWithValue("@secondname", secondname);
            command.Parameters.AddWithValue("@lastname", lastname);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@DateofBirth", DateofBirth);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@address", address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@NationalityCountry", NationalityCountry);
            command.Parameters.AddWithValue("@ImagePath", ImagePath);
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

        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(Connection.connection);
            {
                string query = "select * from People;";
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
      
        public static bool deletePerson(int id)
        {
            int result = 0;
            SqlConnection conn = new SqlConnection(Connection.connection);
            string query = "delete from people where idPerson=@id";
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

        public static bool isExistPerson(int id)
        {
            object isFound;
            SqlConnection conn = new SqlConnection(Connection.connection);
            string query = "select found=1 from people where idPerson=@id";
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

        public static bool isExistPerson(string NationalNo)
        {
            object isFound;
            SqlConnection conn = new SqlConnection(Connection.connection);
            string query = "select found=1 from people where NationalNo=@NationalNo";
            SqlCommand sqlCommand = new SqlCommand(query, conn);
            sqlCommand.Parameters.AddWithValue("@NationalNo", NationalNo);
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

        public static DataTable SearchData(string s,string Data)
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
                    query = "SELECT * FROM People WHERE idPerson = @id;"; // Consulta SQL con un parámetro
                    cmd = new SqlCommand(query, conn); // Usando el bloque para garantizar la liberación de recursos
                    int id;
                    if (int.TryParse(Data, out id))
                    {
                        // La conversion a réussi, vous pouvez maintenant utiliser id comme valeur du paramètre
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
                else if(s=="FirstName")
                {
                    query = "SELECT * FROM People WHERE FirstName = @Data;"; // Consulta SQL con un parámetro
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
                else if (s == "LastName")
                {
                    query = "SELECT * FROM People WHERE LastName = @Data;"; // Consulta SQL con un parámetro
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
                else if (s == "SecondName")
                {
                    query = "SELECT * FROM People WHERE SecondName = @Data;"; // Consulta SQL con un parámetro
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
                else if (s=="NationalNo")
                {
                    query = "SELECT * FROM People WHERE NationalNo = @Data;"; // Consulta SQL con un parámetro
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
                else if (s == "Gender")
                {
                    query = "SELECT * FROM People WHERE Gender = @Data;"; // Consulta SQL con un parámetro
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
                else if (s == "Address")
                {
                    query = "SELECT * FROM People WHERE Address = @Data;"; // Consulta SQL con un parámetro
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
                else if (s == "Phone")
                {
                    query = "SELECT * FROM People WHERE Phone = @Data;"; // Consulta SQL con un parámetro
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
                else if (s == "Email")
                {
                    query = "SELECT * FROM People WHERE Email = @Data;"; // Consulta SQL con un parámetro
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

            }
            catch (Exception ex)
            {
                // Handle the exception appropriately (e.g., throw, log)
                Console.WriteLine("Error retrieving data: " + ex.Message);
                MessageBox.Show("Please enter a number!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close(); // Asegurar que la conexión se cierre en todas las condiciones
            }

            return dt;
        }

        public static bool GETPersonByNational(ref int id, ref string firstName, ref string lastName, ref string secondName,
               string  NationalNo, ref DateTime dateOfBirth, ref string gender, ref string address, ref string phone,
                ref string email, ref int nationalityCountry, ref string imagePath)
        {
            bool isFound = false;
            SqlConnection conn = new SqlConnection(Connection.connection);
            string query = "select * from People where NationalNo=@NationalNo";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            try
            {
                conn.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    isFound = true;
                    id = (int)Reader["idPerson"];
                    firstName = (string)Reader["FirstName"];
                    lastName = (string)Reader["LastName"];
                    email = (string)Reader["Email"];
                    phone = (string)Reader["Phone"];
                    address = (string)Reader["Address"];
                    dateOfBirth = (DateTime)Reader["DateOfBirth"];
                    nationalityCountry = (int)Reader["nationalityCountry"];
                    imagePath = (string)Reader["ImagePath"];

                  //  NationalNo = (string)Reader["NationalNo"];
                    secondName = (string)Reader["secondname"];
                    gender = (string)Reader["Gender"];
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

        public static bool updatePersonOfuser(int idPerson, string first, string second, string third)
        {
            // Construct the full name from the provided parts
            string fullName = $"{first} {second} {third}";

            // Define the connection string (assumed to be stored in Connection.connection)
            string connectionString = Connection.connection;

            // Define the SQL query
            string query = "UPDATE Users SET FullName = @fullName WHERE idPerson = @idPerson";

            // Create a SQL connection using the connection string
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    conn.Open();

                    // Create a SQL command
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters to the SQL command
                        cmd.Parameters.AddWithValue("@fullName", fullName);
                        cmd.Parameters.AddWithValue("@idPerson", idPerson);

                        // Execute the command and check the number of affected rows
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Return true if the update was successful, otherwise false
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions (e.g., log the error)
                    Console.WriteLine("An error occurred: " + ex.Message);

                    // Return false to indicate failure
                    return false;
                }
                finally
                {
                    // Ensure the connection is closed even if an error occurs
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
        }


    }
}
