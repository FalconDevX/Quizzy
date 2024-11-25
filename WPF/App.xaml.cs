using System.Configuration;
using System.Data;
using System.Windows;
using System.Data.SqlClient;
using Microsoft.Win32;
using System.IO;
using System.Resources;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Windows.Media.Imaging;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Windows.Media;

namespace WPF
{
    public partial class App : Application
    {
    }

    //Quiz structure
    public class Quiz
    {
        public string? Name { get; set; }
        public string? Category { get; set; }
        public DateTime LastModified { get; set; }
        public int QuestionCount { get; set; }

        public string? Image { get; set; }
        public List<Question>? Questions { get; set; }
    }

    //Question structure
    public class Question
    {
        public string? QuestionText { get; set; }
        public List<string>? Answers { get; set; }
        public int CorrectAnswerIndex { get; set; }
        public string? Image { get; set; }
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
        private readonly HttpClient _httpClient;

        public UserService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://quizapi-dccjbsaedqcthte3.polandcentral-01.azurewebsites.net/api/"); 
        }

        public async Task<bool> IsLoginTakenApi(string login)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"IsTaken/IsLoginTaken?login={login}");
                response.EnsureSuccessStatusCode();

                string result = await response.Content.ReadAsStringAsync();
                return bool.Parse(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while checking login: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> IsEmailTakenApi(string email)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"IsTaken/IsEmailTaken?email={email}");
                response.EnsureSuccessStatusCode();

                string result = await response.Content.ReadAsStringAsync();
                return bool.Parse(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while checking email: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> RegisterUserApi(string login, string email, string password)
        {
            try
            {
                string hashedPassword = HashPassword(password);

                string url = $"RegisterUser/Register?login={Uri.EscapeDataString(login)}&email={Uri.EscapeDataString(email)}&password={Uri.EscapeDataString(hashedPassword)}";

                HttpResponseMessage response = await _httpClient.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"User registered successfully.");
                    return true;
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Failed to register user. Server response: {errorMessage}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during registration: {ex.Message}");
                return false;
            }
        }

        //Hashing password function
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }




        //login user function
        public async Task<bool> LoginUser(string identifier, string password)
        {
            try
            {
                string endpoint = identifier.Contains("@") ? "RightPasswd/UsingEmail" : "RightPasswd/UsingLogin";
                bool isEmail = identifier.Contains("@");

                var response = await _httpClient.GetAsync($"{endpoint}?{(isEmail ? "email" : "login")}={Uri.EscapeDataString(identifier)}&passwd={Uri.EscapeDataString(password)}");

                if (response.IsSuccessStatusCode)
                {
                    bool isValid = bool.Parse(await response.Content.ReadAsStringAsync());
                    if (isValid)
                    {
                        return await SetCurrentUserInfo(identifier, isEmail);
                    }
                }

                string hashedPassword = HashPassword(password);
                response = await _httpClient.GetAsync($"{endpoint}?{(isEmail ? "email" : "login")}={Uri.EscapeDataString(identifier)}&passwd={Uri.EscapeDataString(hashedPassword)}");

                if (response.IsSuccessStatusCode)
                {
                    bool isValid = bool.Parse(await response.Content.ReadAsStringAsync());
                    if (isValid)
                    {
                        return await SetCurrentUserInfo(identifier, isEmail);
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return false;
            }
        }

        private async Task<bool> SetCurrentUserInfo(string identifier, bool isEmail)
        {
            if (isEmail)
            {
                CurrentUser.Email = identifier;
                UserService userService = new UserService();
                CurrentUser.UserId = await userService.GetUserIdByEmailApi(CurrentUser.Email);
                CurrentUser.Login = await userService.GetLoginByIdApi(CurrentUser.UserId);
            }
            else
            {
                CurrentUser.Login = identifier;
            }
            return true;
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


        //Getting email using Id
        public async Task<string> GetEmailByIdApi(int userId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"GetUserInfo/GetEmailById?id={userId}");
                response.EnsureSuccessStatusCode();

                string email = await response.Content.ReadAsStringAsync();
                return email;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while retrieving email: " + ex.Message);
                return string.Empty;
            }
        }


        //Getting login using Id
        public async Task<string> GetLoginByIdApi(int userId)
        {
            try  
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"GetUserInfo/GetLoginById?id={userId}");
                response.EnsureSuccessStatusCode();

                string login = await response.Content.ReadAsStringAsync();
                return login;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while retrieving login: " + ex.Message);
                return string.Empty;
            }
        }


        public async Task<int> GetUserIdByLoginApi(string login)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"GetUserId/GetUserIdByLogin?login={login}"
);
                response.EnsureSuccessStatusCode();

                string userId = await response.Content.ReadAsStringAsync();
                return int.Parse(userId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while retrieving user ID: " + ex.Message);
                return -1;
            }
        }

        public async Task<int> GetUserIdByEmailApi(string email)
        {
            try                                                       
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"GetUserId/GetUserIdByEmail?email={Uri.EscapeDataString(email)}");
                response.EnsureSuccessStatusCode();

                string userId = await response.Content.ReadAsStringAsync();
                return int.Parse(userId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while retrieving user ID by email: " + ex.Message);
                return -1;
            }
        }

        ///AVATAR///

        public async Task<BitmapImage?> GetAvatarAsync(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"GetUserInfo/GetAvatarById?id={userId}");

                if (response.IsSuccessStatusCode)
                {
                    var avatarBytes = await response.Content.ReadAsByteArrayAsync();

                    if (avatarBytes != null && avatarBytes.Length > 0)
                    {
                        var bitmap = new BitmapImage();
                        using (var stream = new MemoryStream(avatarBytes))
                        {
                            bitmap.BeginInit();
                            bitmap.StreamSource = stream;
                            bitmap.CacheOption = BitmapCacheOption.OnLoad;
                            bitmap.EndInit();
                        }
                        return bitmap;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> ChangeUserAvatarAsync(string filePath, int userId)
        {
            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var content = new MultipartFormDataContent())
                    {
                        var streamContent = new StreamContent(fileStream);
                        streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

                        content.Add(streamContent, "avatar", Path.GetFileName(filePath));

                        var response = await _httpClient.PostAsync($"ChangeUserInfo/ChangeAvatar?id={userId}", content);

                        if (response.IsSuccessStatusCode)
                        {
                            string result = await response.Content.ReadAsStringAsync();
                            return true;
                        }
                        else
                        {
                            string errorMessage = await response.Content.ReadAsStringAsync();
                            MessageBox.Show("Failed to update avatar. Server response: " + errorMessage);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
        }

    }

    ///JSON file///

    public class QuizFile
    {
        //Convert image to byte table
        private string ConvertImageToBase64(ImageSource imageSource)
        {
            if (imageSource is BitmapImage bitmapImage)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                    encoder.Save(ms);
                    byte[] imageBytes = ms.ToArray();
                    return Convert.ToBase64String(imageBytes);
                }
            }
            return string.Empty;
        }




        //Convert byte table to image 

        public ImageSource ConvertBase64ToImage(string base64String)
        {
            if (!string.IsNullOrEmpty(base64String))
            {
                try
                {
                    byte[] imageBytes = Convert.FromBase64String(base64String);
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        BitmapImage image = new BitmapImage();
                        image.BeginInit();
                        image.CacheOption = BitmapCacheOption.OnLoad; // Zapewnia ładowanie obrazu w pamięci
                        image.StreamSource = ms;
                        image.EndInit();
                        return image;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Błąd podczas konwersji obrazu: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return null;
        }


        //Save quiz to JSON
        public void SaveQuizToJson(Quiz quiz, string filePath)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(quiz, options);

            File.WriteAllText(filePath, json);

        }

        //Read quiz from json
        public Quiz LoadQuizFromJson(string filePath)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Plik nie istnieje.");
                return null;
            }

            try
            {
                string json = File.ReadAllText(filePath);
                Quiz quiz = JsonSerializer.Deserialize<Quiz>(json); 
                return quiz;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd podczas odczytu: {ex.Message}");
                return null;
            }
        }

        //Debug create custom quiz
        public void CreateAndSaveQuizExample(string QuizName, string JsonName, string Category, ImageSource image)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string QuizesPath = System.IO.Path.Combine(basePath, "Resources", "Quizes", JsonName);

            var quiz = new Quiz
            {
                Name = QuizName,
                Category = Category,
                LastModified = DateTime.Now,
                QuestionCount = 2,
                Image = ConvertImageToBase64(image),
                Questions = new List<Question>
            {
                new Question
                {
                    QuestionText = "What is the capital of France?",
                    Answers = new List<string> { "Paris", "London", "Berlin", "Rome" },
                    CorrectAnswerIndex = 0,
                    Image = ConvertImageToBase64(image),
                },
                new Question
                {
                    QuestionText = "What is 2+2?",
                    Answers = new List<string> { "3", "4", "5", "6" },
                    CorrectAnswerIndex = 1,
                    Image = ConvertImageToBase64(image),
                }
            }
            };
            SaveQuizToJson(quiz, $"C:/Quizzy/QuizzyAplication/{JsonName}.json");
        }
    }
}


