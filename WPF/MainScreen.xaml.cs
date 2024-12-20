﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Windows.Resources;
using System.Windows.Controls.Primitives;
using System.Text.Json;
using System.Collections.ObjectModel;
using static System.Formats.Asn1.AsnWriter;

namespace WPF
{
    public class Item
    {
        public string? Name { get; set; }
        public string? Category { get; set; }
        public DateTime LastModified { get; set; }
        public int QuestionCount { get; set; }
        public ImageSource? Image { get; set; }
        public List<Question>? Questions { get; set; }
        public string? Description { get; set; } 
    }


    public partial class MainScreen : Window
    {
        private ObservableCollection<Item> Items = new ObservableCollection<Item>();
        private bool _isLoadingQuizzes = true;

        public UserService userService;
        private readonly UserService _avatarService;
        private Item SelectedItem { get; set; } = new Item();
        public MainScreen()
        {
            _isLoadingQuizzes = true;
            _avatarService = new UserService();
            InitializeComponent();
            QuizListBox.SelectedItem = null;
            QuizListBox.SelectedIndex = -1;
            Loaded += MainScreen_Loaded;
        }

        //Loading screen function
        private async void MainScreen_Loaded(object sender, RoutedEventArgs e)
        {
            _isLoadingQuizzes = true;

            LoadAllQuizzes();

            var groupedItems = (CollectionViewSource)FindResource("GroupedItems");
            groupedItems.Source = Items;

            UserService userservice = new UserService();
            CurrentUser.Email = await userservice.GetEmailByIdApi(CurrentUser.UserId);

            LoadAvatar();

            UserNameTextBlock.Text = $"Hi, {CurrentUser.Login}";
            SideBarNickTextBlock.Text = CurrentUser.Login;
            UserEmailSettingsTextBlock.Text = CurrentUser.Email;
            UserLoginSettingsTextBlock.Text = CurrentUser.Login;

            _isLoadingQuizzes = false;

            QuizListBox.SelectedIndex = -1; 
        }

        ///SIDEBAR///

        private void SideBarButton_Click(object sender, RoutedEventArgs e)
        {
            SideBarClick(sender, e);
            if (sender is ToggleButton clickedButton)
            {
                if (clickedButton.IsChecked == true)
                {
                    return;
                }
                foreach (var child in SideBarPanel.Children)
                {
                    if (child is ToggleButton button)
                    {
                        button.IsChecked = false;
                    }
                }
                clickedButton.IsChecked = true;
            }
        }

        //Home button clicked
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            SideBarClick(sender, e);
            SideBarButton_Click(sender, new RoutedEventArgs());
            ShowOnlySelectedBorder("HomeBorder");
        }

        //Settings button clicked
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SideBarClick(sender, e);
            SideBarButton_Click(sender, new RoutedEventArgs());
            ShowOnlySelectedBorder("SettingsBorder");
        }

        //show selected border(current panel from sidebar) function
        private void ShowOnlySelectedBorder(string borderName)
        {
            foreach (var child in MainGrid.Children)
            {
                if (child is Border border)
                {
                    if (border.Name == borderName)
                    {
                        border.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        border.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        ///SETTINGS///

        //Setting default avatar
        public async void SetDefaultAvatar(int avatarNumber)
        {
            LoadingSpinner.Visibility = Visibility.Visible;

            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string avatarPath = System.IO.Path.Combine(basePath, "Resources", "MainScreen", "Avatars", $"Avatar{avatarNumber}.png");

            if (System.IO.File.Exists(avatarPath))
            {
                using (var stream = System.IO.File.OpenRead(avatarPath))
                {
                    using (BinaryReader br = new BinaryReader(stream))
                    {
                        CurrentUser.Avatar = br.ReadBytes((int)stream.Length);
                    }
                }

                UserService userService = new UserService();
                await userService.ChangeUserAvatarAsync(avatarPath, CurrentUser.UserId);
            }
            else
            {
                MessageBox.Show($"Avatar file not found: {avatarPath}");
            }

            LoadAvatar();
            LoadingSpinner.Visibility = Visibility.Hidden;
        }


        //Change avatar button
        private async void ChangeAvatarButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFileDialog.ShowDialog() == true)
            {
                LoadingSpinner.Visibility = Visibility.Visible;
                string filePath = openFileDialog.FileName;
                UserService userService = new UserService();

                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        CurrentUser.Avatar = br.ReadBytes((int)fs.Length);
                    }
                }

                bool isSuccess = await userService.ChangeUserAvatarAsync(filePath, CurrentUser.UserId);
                if (!isSuccess)
                {
                    MessageBox.Show("Aktualizacja avatara nie powiodła się.");
                }
            }
            else
            {
                MessageBox.Show("Nie wybrano pliku.");
            }
            LoadAvatar();
            LoadingSpinner.Visibility = Visibility.Hidden;
        }


        //Avatars button section

        private void AvatarButton1_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultAvatar(1);
        }

        private void AvatarButton2_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultAvatar(2);
        }

        private void AvatarButton3_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultAvatar(3);
        }

        private void AvatarButton4_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultAvatar(4);
        }

        private void AvatarButton5_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultAvatar(5);
        }

        private void AvatarButton6_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultAvatar(6);
        }

        private void AvatarButton7_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultAvatar(7);
        }

        private void AvatarButton8_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultAvatar(8);
        }

        private void AvatarButton9_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultAvatar(9);
        }

        private void AvatarButton10_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultAvatar(10);
        }

        private void AvatarButton11_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultAvatar(11);
        }

        private void AvatarButton12_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultAvatar(12);
        }

        //exit application
        private async void CloseWindowButton_Click(object sender, RoutedEventArgs e)
        {
            string QuizesPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Quizzy", "Quizes");
            AzureBlobAPI azureblobapi = new AzureBlobAPI();
        if (await azureblobapi.CheckContainerExistsByUserID(CurrentUser.UserId))
        {
            await azureblobapi.UploadAllBlobs($"ident{CurrentUser.UserId}");
        }
        else
        {
            await azureblobapi.CreateContainerAsync($"ident{CurrentUser.UserId}");
            await azureblobapi.UploadAllBlobs($"ident{CurrentUser.UserId}");
        }
                
            Application.Current.Shutdown();
        }

        //minimize application
        private void MinimizeWindowButton_Click(Object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
            
        //Change password button
        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordWindow changePasswordWindow = new ChangePasswordWindow();
            changePasswordWindow.Show();
        }


        //Change login button
        private void ChangeLoginButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeLoginWindow changeLoginWindow = new ChangeLoginWindow();
            changeLoginWindow.Show();
            //DialogBox dialogbox = new DialogBox();
            //dialogbox.DeleteButton.Visibility = Visibility.Collapsed;
            //dialogbox.ChangeButton.Visibility = Visibility.Visible;
            //dialogbox.DeleteTextBlock.Visibility = Visibility.Collapsed;
            //dialogbox.ChangeLoginTextBlock.Visibility = Visibility.Visible;
            //dialogbox.DialogBoxTextBox.Tag = "Type new login";
            //dialogbox.DialogBoxTextBlockInfo.Text = "Login taken";
            //dialogbox.ChangeButton.Content = "Change";
            //dialogbox.ShowDialog();

            //SideBarNickTextBlock.Text = CurrentUser.Login;
            //UserLoginSettingsTextBlock.Text = CurrentUser.Login;
            //UserNameTextBlock.Text = $"Hi, {CurrentUser.Login}";
        }

        //Delete account button

        private void DeleteAccountButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteWindow deletewindow = new DeleteWindow();
            deletewindow.Show();
            //dialogbox.ChangeButton.Visibility = Visibility.Collapsed;
            //dialogbox.DeleteButton.Visibility = Visibility.Visible;
            //dialogbox.ChangeLoginTextBlock.Visibility = Visibility.Collapsed;
            //dialogbox.DeleteTextBlock.Visibility = Visibility.Visible;
            //dialogbox.DialogBoxTextBox.Tag = "Retype your login";
            //dialogbox.DialogBoxTextBlockInfo.Text = "Login do not match";
            //dialogbox.ShowDialog();
        }

        private async void LoadAvatar()
        {
            int userId = CurrentUser.UserId;
            BitmapImage avatarImage;

            try
            {
                avatarImage = await _avatarService.GetAvatarAsync(userId);

                if (avatarImage == null)
                {
                    avatarImage = new BitmapImage(new Uri("/Resources/MainScreen/SideBar/DefaultLogoIcon.png", UriKind.Relative));
                }

                UserAvatarImage.Source = avatarImage;
                SidebarUserPhoto.Source = avatarImage;
                AvatarUserPhoto.Source = avatarImage;

                UserAvatarImage.Width = 150;
                UserAvatarImage.Height = 150;
                AvatarUserPhoto.Height = 150;
                AvatarUserPhoto.Width = 150;
                SidebarUserPhoto.Width = 60;
                SidebarUserPhoto.Height = 60;

                UserAvatarImage.Stretch = Stretch.UniformToFill;
                SidebarUserPhoto.Stretch = Stretch.UniformToFill;
                AvatarUserPhoto.Stretch = Stretch.UniformToFill;

                UserAvatarImage.Clip = new EllipseGeometry(new Point(UserAvatarImage.Width / 2, UserAvatarImage.Height / 2), UserAvatarImage.Width / 2, UserAvatarImage.Height / 2);
                SidebarUserPhoto.Clip = new EllipseGeometry(new Point(SidebarUserPhoto.Width / 2, SidebarUserPhoto.Height / 2), SidebarUserPhoto.Width / 2, SidebarUserPhoto.Height / 2);
                AvatarUserPhoto.Clip = new EllipseGeometry(new Point(AvatarUserPhoto.Width / 2, AvatarUserPhoto.Height / 2), AvatarUserPhoto.Width / 2, AvatarUserPhoto.Height / 2);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the avatar: {ex.Message}");

                avatarImage = new BitmapImage(new Uri("/Resources/MainScreen/SideBar/DefaultLogoIcon.png", UriKind.Relative));
                UserAvatarImage.Source = avatarImage;
                SidebarUserPhoto.Source = avatarImage;
                AvatarUserPhoto.Source = avatarImage;

                UserAvatarImage.Width = 100;
                UserAvatarImage.Height = 100;
                UserAvatarImage.Stretch = Stretch.UniformToFill;
                UserAvatarImage.Clip = new EllipseGeometry(new Point(UserAvatarImage.Width / 2, UserAvatarImage.Height / 2), UserAvatarImage.Width / 2, UserAvatarImage.Height / 2);
            }
        }

        public string ConvertImageToBase64String(string imagePath)
        {
            if (!File.Exists(imagePath))
            {
                throw new FileNotFoundException($"Image not found at path: {imagePath}");
            }

            byte[] imageBytes = File.ReadAllBytes(imagePath); // Odczytaj obraz jako tablicę bajtów
            return Convert.ToBase64String(imageBytes); // Konwertuj na Base64
        }

        //Adding one quiz to Item list
        private void AddQuizToItems(string jsonPath)
        {
            string defaultLogoPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Quizes", "DefaultQuizLogo.png");

            // Tworzenie ImageSource na podstawie ścieżki
            string defaultLogoImage = ConvertImageToBase64String(defaultLogoPath);


            QuizFile quizFile = new QuizFile();
                Quiz quiz = quizFile.LoadQuizFromJson(jsonPath);

            if (quiz != null)
            {
                    // Załaduj obraz z quizu
                    
                if (quiz.Image =="") 
                {
                    quiz.Image = defaultLogoImage;
                }

                var imageSource = CreateCircularImage(quiz.Image, 50);

                Items.Add(new Item
                {
                    Name = quiz.Name,
                    Category = quiz.Category,
                    LastModified = quiz.LastModified,
                    QuestionCount = quiz.Questions.Count,
                    Image = imageSource,
                    Questions = quiz.Questions
                });
            }
            else
            {
                MessageBox.Show("Nie udało się wczytać quizu z JSON.");
            }

                
        }

        //Loading all quizes
        public void LoadAllQuizzes()
        {
            _isLoadingQuizzes = true;

            // Ładowanie quizów...
            Items.Clear();
            string quizzesPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Quizzy", "Quizes");

            if (!Directory.Exists(quizzesPath))
            {
                Directory.CreateDirectory(quizzesPath);
            }

            string[] quizFiles = Directory.GetFiles(quizzesPath, "*.json", SearchOption.TopDirectoryOnly);

            foreach (string file in quizFiles)
            {
                try
                {
                    AddQuizToItems(file);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Błąd podczas wczytywania quizu z pliku {file}: {ex.Message}");
                }
            }

            var groupedItems = (CollectionViewSource)FindResource("GroupedItems");
            groupedItems.Source = null;
            groupedItems.Source = Items;

            _isLoadingQuizzes = false;
        }


        private void AddQuizButton_Click(object sender, RoutedEventArgs e)
        {
            HomeBorder.Visibility = Visibility.Hidden;
            NewQuizBorder.Visibility = Visibility.Visible;
        }

        private void BackToHomeButton_Click(object sender, RoutedEventArgs e)
        {
            BackToHome();
        }

        private void NewQuizNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "")
            {
                textBox.Tag = "";
            }
        }

        private void DescriptionTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "")
            {
                textBox.Tag = "";
            }
        }

        private void NewQuizNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "")
            {
                textBox.Tag = "Type new quiz name";
            }
        }

        private void DescriptionTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "")
            {
                textBox.Tag = "Add short description";
            }
        }

        private void AddImageQuizButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            QuizFile quizfile = new QuizFile();

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        string base64String = Convert.ToBase64String(br.ReadBytes((int)fs.Length));
                        SelectedItem.Image = quizfile.ConvertBase64ToImage(base64String);
                        ImageQuiz.Source = SelectedItem.Image;
                    }
                }
            }
        }


        //function for creating quiz
        private void CreateQuizButton_Click(object sender, RoutedEventArgs e)
        {
            QuizFile quizFile = new QuizFile();
            Item item = new Item();

            // Pobranie kategorii
            var selectedItem = (ComboBoxItem)CategoryQuizComboBox.SelectedItem;
            if (selectedItem != null)
            {
                string selectedCategory = selectedItem.Content.ToString();
                item.Category = selectedCategory;
            }
            else
            {
                MessageBox.Show("Nie wybrano kategorii.");
                return;
            }

            if (!string.IsNullOrWhiteSpace(NewQuizNameTextBox.Text))
            {
                item.Name = NewQuizNameTextBox.Text;
            }
            else
            {
                MessageBox.Show("Nazwa quizu nie może być pusta.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if(!string.IsNullOrEmpty(DescriptionTextBox.Text))
            {
                item.Description = DescriptionTextBox.Text;
            }
            else
            {
                MessageBox.Show("Opis nie może być pusty");
                return;
            }

            if (quizFile.CreateAndSaveQuiz(item.Name, item.Description, item.Name, item.Category, SelectedItem.Image))
            {
                MessageBox.Show("Quiz został pomyślnie dodany!");
                BackToHome();
            }
        }


        private void BackToHome()
        {
            LoadAllQuizzes();

            CategoryQuizComboBox.SelectedItem = null;
            NewQuizNameTextBox.Text = "";
            DescriptionTextBox.Text = "";
            NewQuizNameTextBox.Tag = "Type new quiz name";
            DescriptionTextBox.Tag = "Add short description";

            HomeBorder.Visibility = Visibility.Visible;
            NewQuizBorder.Visibility = Visibility.Hidden;

            var groupedItems = (CollectionViewSource)FindResource("GroupedItems");
            if (groupedItems != null)
            {
                groupedItems.Source = null;
                groupedItems.Source = Items;
            }
        }

        public ImageSource CreateCircularImage(string base64Image, double diameter)
        {
            if (string.IsNullOrEmpty(base64Image))
                return null;

            try
            {
                // Konwersja Base64 na obraz
                byte[] imageBytes = Convert.FromBase64String(base64Image);
                using (var stream = new MemoryStream(imageBytes))
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();

                    // Ustawienie przycięcia w formie koła
                    var ellipseGeometry = new EllipseGeometry
                    {
                        Center = new Point(diameter / 2, diameter / 2),
                        RadiusX = diameter / 2,
                        RadiusY = diameter / 2
                    };

                    // Tworzenie ImageBrush z kołem
                    var brush = new ImageBrush(bitmap)
                    {
                        Stretch = Stretch.UniformToFill
                    };

                    var visual = new DrawingVisual();
                    using (var context = visual.RenderOpen())
                    {
                        context.PushClip(ellipseGeometry);
                        context.DrawRectangle(brush, null, new Rect(0, 0, diameter, diameter));
                    }

                    var renderTarget = new RenderTargetBitmap((int)diameter, (int)diameter, 96, 96, PixelFormats.Pbgra32);
                    renderTarget.Render(visual);

                    return renderTarget;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas konwersji obrazu: {ex.Message}");
                return null;
            }
        }

        public string ConvertImageToBase64(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath))
            {
                throw new ArgumentException("Ścieżka obrazu nie może być pusta.");
            }

            if (!File.Exists(imagePath))
            {
                throw new FileNotFoundException($"Plik nie został znaleziony: {imagePath}");
            }

            try
            {
                // Wczytanie obrazu jako tablicy bajtów
                byte[] imageBytes = File.ReadAllBytes(imagePath);

                // Konwersja na ciąg Base64
                string base64String = Convert.ToBase64String(imageBytes);

                return base64String;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Błąd podczas konwersji obrazu na Base64: {ex.Message}");
            }
        }

        private ToggleButton _activeButton; 

        private void SideBarClick(object sender, RoutedEventArgs e)
        {
            var clickedButton = sender as ToggleButton;

            if (_activeButton == clickedButton)
            {
                clickedButton.IsChecked = true; 
                return;
            }

            if (_activeButton != null)
            {
                _activeButton.IsChecked = false;
            }

            if (clickedButton != null)
            {
                _activeButton = clickedButton;
                clickedButton.IsChecked = true;
            }
        }

        //Check if quiz from listbox clicked
        private void QuizListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isLoadingQuizzes || QuizListBox.SelectedIndex == -1)
            {
                return;
            }

            if (QuizListBox.SelectedItem is Item selectedItem && selectedItem.Questions != null)
            {
                LoadQuiz(selectedItem.Questions);
                

                QuizListBox.SelectedIndex = -1;
            }
        }
        

        private void BackToHomeButtonFromQuizView_Click(object sender, RoutedEventArgs e)
        {
            QuizView.Visibility = Visibility.Hidden;
            HomeBorder.Visibility = Visibility.Visible;
            SideBarGrid.Visibility = Visibility.Visible;
        }

        private List<Question> _currentQuizQuestions = new List<Question>();
        private int _currentQuestionIndex = 0;

        private void LoadQuiz(List<Question> questions)
        {
            _currentQuizQuestions = questions;
            _currentQuestionIndex = 0;

            if (_currentQuizQuestions.Count > 0)
            {
                DisplayQuestion();
                SideBarGrid.Visibility = Visibility.Hidden;
                HomeBorder.Visibility = Visibility.Hidden;
                QuizView.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Wybrany quiz nie zawiera pytań.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DisplayQuestion()
        {
            if (_currentQuestionIndex < 0 || _currentQuestionIndex >= _currentQuizQuestions.Count)
            {
                MessageBox.Show("To było ostatnie pytanie w quizie.", "Koniec quizu", MessageBoxButton.OK, MessageBoxImage.Information);
                QuizView.Visibility = Visibility.Hidden;
                HomeBorder.Visibility = Visibility.Visible;
                SideBarGrid.Visibility = Visibility.Visible;

                return;
            }

            var currentQuestion = _currentQuizQuestions[_currentQuestionIndex];

            QuizQestion.Text = currentQuestion.QuestionText;

            if (currentQuestion.Answers != null && currentQuestion.Answers.Count >= 4)
            {
                QuizButton1.Content = currentQuestion.Answers[0];
                QuizButton2.Content = currentQuestion.Answers[1];
                QuizButton3.Content = currentQuestion.Answers[2];
                QuizButton4.Content = currentQuestion.Answers[3];

                QuizButton1.Click -= AnswerButton_Click;
                QuizButton2.Click -= AnswerButton_Click;
                QuizButton3.Click -= AnswerButton_Click;
                QuizButton4.Click -= AnswerButton_Click;

                QuizButton1.Click += AnswerButton_Click;
                QuizButton2.Click += AnswerButton_Click;
                QuizButton3.Click += AnswerButton_Click;
                QuizButton4.Click += AnswerButton_Click;
            }
            else
            {
                MessageBox.Show("Nieprawidłowa liczba odpowiedzi w pytaniu.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private int _score = 0;
        private void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedButton)
            {
                var currentQuestion = _currentQuizQuestions[_currentQuestionIndex];

                int clickedIndex = -1;

                if (clickedButton == QuizButton1) clickedIndex = 0;
                else if (clickedButton == QuizButton2) clickedIndex = 1;
                else if (clickedButton == QuizButton3) clickedIndex = 2;
                else if (clickedButton == QuizButton4) clickedIndex = 3;

                if (clickedIndex == currentQuestion.CorrectAnswerIndex)
                {
                    //MessageBox.Show("Poprawna odpowiedź!", "Gratulacje", MessageBoxButton.OK, MessageBoxImage.Information);
                    _score++; 
                }
                else
                {
                    //MessageBox.Show($"Niepoprawna odpowiedź. Poprawna odpowiedź to: {currentQuestion.Answers[currentQuestion.CorrectAnswerIndex]}", "Spróbuj ponownie", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                _currentQuestionIndex++;
                if (_currentQuestionIndex < _currentQuizQuestions.Count)
                {
                    DisplayQuestion();
                }
                else
                {
                    MessageBox.Show($"To było ostatnie pytanie w quizie. Twój wynik: {_score}/{_currentQuizQuestions.Count}.",
                        "Koniec quizu",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);

                    _score = 0;

                    QuizView.Visibility = Visibility.Hidden;
                    HomeBorder.Visibility = Visibility.Visible;
                    SideBarGrid.Visibility = Visibility.Visible;
                }
            }
        }



        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Czy na pewno chcesz pominąć to pytanie? Otrzymasz zero punktów za pytanie.",
                "Potwierdzenie",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _currentQuestionIndex++;

                if (_currentQuestionIndex < _currentQuizQuestions.Count)
                {
                    DisplayQuestion(); 
                }
                else
                {
                    MessageBox.Show($"To było ostatnie pytanie w quizie. Twój wynik: {_score}/{_currentQuizQuestions.Count}.",
                        "Koniec quizu",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);

                    QuizView.Visibility = Visibility.Hidden;
                    HomeBorder.Visibility = Visibility.Visible;
                    SideBarGrid.Visibility = Visibility.Visible;
                }
            }

        }

    }
}