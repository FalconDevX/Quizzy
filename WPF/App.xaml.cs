using System.Configuration;
using System.Data;
using System.Windows;
using System.Data.SqlClient;
using Microsoft.Win32;
using System.IO;
using System.Resources;

namespace WPF
{
    public partial class App : Application
    {
    }

    //global variables for current login user
    public static class CurrentUser
    {
        public static string? Login { get; set; }
        public static string? Email { get; set; }
        public static int UserId { get; set; }
        public static byte[]? Avatar { get; set; }
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
                    return false;
                }

                string query = "SELECT UserId, Login, Email FROM Users WHERE (Login COLLATE Latin1_General_BIN = @Identifier OR Email COLLATE Latin1_General_BIN = @Identifier) AND PasswordHash COLLATE Latin1_General_BIN = @Password";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Identifier", identifier);
                    cmd.Parameters.AddWithValue("@Password", password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            CurrentUser.UserId = Convert.ToInt32(reader["UserId"]);
                            CurrentUser.Login = Convert.ToString(reader["Login"]);
                            CurrentUser.Email = Convert.ToString(reader["Email"]);

                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
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
                string query = "SELECT COUNT(1) FROM Users WHERE Email COLLATE Latin1_General_BIN = @Email";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        //Zapisywanie avatara w bazie danych - funkcja używana tylko przez domyślne avatary
        public void SaveAvatarToDatabase()
        {
            if (CurrentUser.Avatar == null || CurrentUser.Avatar.Length == 0)
            {
                throw new InvalidOperationException("Avatar is not set.");
            }

            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Users SET Avatar = @Avatar WHERE UserId = @UserId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Avatar", CurrentUser.Avatar);
                    command.Parameters.AddWithValue("@UserId", CurrentUser.UserId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Avatar has been successfully saved to the database.");
        }

        //zapisywanie avatara w bazie danych - funkcja używana przez dodawanie własnego avatara
        public void SaveUserAvatar(int userId, string filePath)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            byte[] avatarData;

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    avatarData = br.ReadBytes((int)fs.Length);
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Users SET Avatar = @Avatar WHERE UserId = @UserId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@Avatar", avatarData);

                    try
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Avatar updated successfully.");
                        }
                        else
                        {
                            MessageBox.Show("User not found or update failed.");
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        //funkcja pobierania avatara z bazy danych
        public void GetUserAvatar(int userId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Avatar FROM Users WHERE UserId = @UserId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read() && !reader.IsDBNull(0))
                        {
                            CurrentUser.Avatar = (byte[])reader["Avatar"];
                        }
                        else
                        {
                            CurrentUser.Avatar = null;
                        }
                    }
                }
            }
        }

        // Change user login function
        public void ChangeUserLogin(int userId, string newLogin)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(1) FROM Users WHERE Login COLLATE Latin1_General_BIN = @NewLogin";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@NewLogin", newLogin);
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("Login is already taken. Please choose another one.");
                        return;
                    }
                }
                string query = "UPDATE Users SET Login = @NewLogin WHERE UserId = @UserId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NewLogin", newLogin);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    try
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            CurrentUser.Login = newLogin; 
                            MessageBox.Show("Login updated successfully.");
                        }
                        else
                        {
                            MessageBox.Show("User not found or update failed.");
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

    }
}