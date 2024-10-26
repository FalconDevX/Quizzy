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
        public void RegisterUser(string email, string password)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Users (Email, PasswordHash) VALUES (@Email, @Password)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    //Przechowywanie email i hasła
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("User registered successfully.");
                    }
                    catch (SqlException ex)
                    {
                        //Obsługa błędów
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }
        public bool LoginUser(string email, string password)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(1) FROM Users WHERE Email = @Email AND PasswordHash = @Password";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    //Hasło w postaci jawnej - potem algorytm szyfrujący 
                    cmd.Parameters.AddWithValue("@Password", password);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count == 1;
                }
            }
        }

    }

}
