using System.Configuration;
using System.Data.SqlClient;

namespace NewLoginGui
{
    internal static class Program
    {
        struct Answer
        {
            string q;
            bool isTrue;
        }
        class Quiz
        {
            string quest;
            bool hasPicture;
            Answer[] ans = new Answer[4];
            public Quiz(string quest)
            {
                this.quest = quest;
            }
        }
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new StartScreen());
        }
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
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);  // Przechowywanie has³a w postaci jawnej

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("User registered successfully.");
                    }
                    catch (SqlException ex)
                    {
                        // Obs³uga b³êdów, np. duplikat email
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
                    cmd.Parameters.AddWithValue("@Password", password);  // Porównywanie has³a w postaci jawnej

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count == 1;  // Zwraca true, jeœli dane s¹ poprawne
                }
            }
        }

    }
}