using System.Configuration;
using System.Data;
using System.Windows;
using System.Data.SqlClient;

namespace WPF
{
    public partial class App : Application
    {
    }

    public class UserService
    {
        //register user function
        public void RegisterUser(string login, string email, string password)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Users (Login, Email, PasswordHash) VALUES (@Login, @Email, @Password)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Login", login);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("User registered successfully.");
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        public string GetUserLogin(string identifier)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Zapytanie SQL do znalezienia loginu na podstawie podanego loginu lub e-maila
                string query = "SELECT Login FROM Users WHERE Login COLLATE Latin1_General_BIN = @Identifier OR Email COLLATE Latin1_General_BIN = @Identifier";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Identifier", identifier);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader["Login"].ToString(); // Zwraca login użytkownika
                        }
                        else
                        {
                            return null; // Jeśli nie znaleziono użytkownika
                        }
                    }
                }
            }
        }


        //login user function
        public bool LoginUser(string identifier, string password)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                }
                catch
                {
                    MessageBox.Show("Error: Unable to connect to the database.");

                }

                // Zapytanie sprawdzające login lub email, rozróżniające wielkość liter
                string query = "SELECT COUNT(1) FROM Users WHERE (Login COLLATE Latin1_General_BIN = @Identifier OR Email COLLATE Latin1_General_BIN = @Identifier) AND PasswordHash COLLATE Latin1_General_BIN = @Password";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Identifier", identifier);
                    cmd.Parameters.AddWithValue("@Password", password);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count == 1;
                }
            }
        }

        //checking if login exist
        public bool IsLoginTaken(string login)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // Sprawdzenie, czy login istnieje z rozróżnieniem wielkości liter
                string query = "SELECT COUNT(1) FROM Users WHERE Login COLLATE Latin1_General_BIN = @Login";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Login", login);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        //checking if email exist
        public bool IsEmailTaken(string email)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // Sprawdzenie, czy email istnieje z rozróżnieniem wielkości liter
                string query = "SELECT COUNT(1) FROM Users WHERE Email COLLATE Latin1_General_BIN = @Email";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        


    }
}