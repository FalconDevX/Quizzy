using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace WPF
{
    public partial class App : Application
    {
    }

    public class UserService
    {
        public void RegisterUser(string login, string email, string password)
        {
            string connectionString = DatabaseConnect.GetConnectionString();

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

        public bool LoginUser(string identifier, string password)
        {
            string connectionString = DatabaseConnect.GetConnectionString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(1) FROM Users WHERE (Login = @Identifier OR Email = @Identifier) AND PasswordHash = @Password";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Identifier", identifier);
                    cmd.Parameters.AddWithValue("@Password", password);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count == 1;
                }
            }
        }

        public bool IsLoginTaken(string login)
        {
            string connectionString = DatabaseConnect.GetConnectionString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(1) FROM Users WHERE Login = @Login";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Login", login);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0; // Zwraca true, jeśli login już istnieje
                }
            }
        }

        // Funkcje szyfrowania i odszyfrowywania nie są potrzebne, jeśli używasz Azure Key Vault.
    }

    public class DatabaseConnect
    {
        public static string GetConnectionString()
        {
            string keyVaultName = "KeyApp"; 
            var kvUri = $"https://{keyVaultName}.vault.azure.net/";

            try
            {
                
                var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());

                KeyVaultSecret secret = client.GetSecret("DatabaseConnectionString");

                return secret.Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas pobierania connection string z Key Vault: {ex.Message}");
                return null;
            }
        }
    }
}
