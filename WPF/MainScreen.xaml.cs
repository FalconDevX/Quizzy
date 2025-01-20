using System;
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
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;

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

    public class EditableQuestion : INotifyPropertyChanged
    {
        private string _questionText;
        private string _correctAnswer;
        private ObservableCollection<Answer> _otherAnswersList = new ObservableCollection<Answer>();
        private ImageSource _image; // Dodaj pole do przechowywania obrazu

        public string QuestionText
        {
            get => _questionText;
            set
            {
                if (_questionText != value)
                {
                    _questionText = value;
                    OnPropertyChanged(nameof(QuestionText));
                }
            }
        }

        public string CorrectAnswer
        {
            get => _correctAnswer;
            set
            {
                if (_correctAnswer != value)
                {
                    _correctAnswer = value;
                    OnPropertyChanged(nameof(CorrectAnswer));
                }
            }
        }

        public ObservableCollection<Answer> OtherAnswersList
        {
            get => _otherAnswersList;
            set
            {
                if (_otherAnswersList != value)
                {
                    _otherAnswersList = value;
                    OnPropertyChanged(nameof(OtherAnswersList));
                }
            }
        }

        public ImageSource Image
        {
            get => _image;
            set
            {
                if (_image != value)
                {
                    _image = value;
                    OnPropertyChanged(nameof(Image));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }




    public class Answer : INotifyPropertyChanged
    {
        private string _text;

        public string Text
        {
            get => _text;
            set
            {
                if (_text != value)
                {
                    _text = value;
                    OnPropertyChanged(nameof(Text));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }



    public partial class MainScreen : Window
    {
        internal ObservableCollection<Item> Items = new ObservableCollection<Item>();
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
            MessageBox.Show("Feature not implemented yet");
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
            SideBarClick(sender, e); // Zarządzanie stanem przycisków
            ShowOnlySelectedBorder("HomeBorder"); // Pokaż tylko widok Home
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            SideBarClick(sender, e); // Zarządzanie stanem przycisków
            //ShowOnlySelectedBorder("EditBorder"); // Pokaż tylko widok Edit
            MessageBox.Show("Feature not implemented yet");
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SideBarClick(sender, e); // Zarządzanie stanem przycisków
            ShowOnlySelectedBorder("SettingsBorder"); // Pokaż tylko widok Settings
        }

        //show selected border(current panel from sidebar) function
        private void ShowOnlySelectedBorder(string borderName)
        {
            foreach (var child in MainGrid.Children)
            {
                if (child is Border border)
                {
                    border.Visibility = border.Name == borderName ? Visibility.Visible : Visibility.Collapsed;
                }
            }

            // Jeśli użytkownik opuścił edytor quizów, zresetuj jego dane
            if (borderName != "EditQuizBorder")
            {
                ResetEditQuizData();
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
        internal async void ChangeAvatarButton_Click(object sender, RoutedEventArgs e)
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
        internal async void CloseWindowButton_Click(object sender, RoutedEventArgs e)
        {
            string QuizesPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Quizzy", "Quizes");
            AzureBlobAPI azureblobapi = new AzureBlobAPI();
            if (await azureblobapi.CheckContainerExistsByUserID($"ident{CurrentUser.UserId}"))
            {
                await azureblobapi.UploadAllBlobs($"ident{CurrentUser.UserId}");
            }
            else
            {
                await azureblobapi.CreateContainerAsync($"ident{CurrentUser.UserId}");
                await azureblobapi.UploadAllBlobs($"ident{CurrentUser.UserId}");

                var answer = await azureblobapi.CreateContainerAsync($"files{CurrentUser.UserId}");

                //MessageBox.Show(answer);
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

        internal async void LoadAvatar()
        {
            int userId = CurrentUser.UserId;
            BitmapImage avatarImage;

            try
            {
                avatarImage = await _avatarService.GetAvatarAsync(userId);

                //MessageBox.Show(Convert.ToBase64String(avatarImage));

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
        internal void AddQuizToItems(string jsonPath)
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
                    Description = quiz.Description,
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
            _score = 0;
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
                byte[] imageBytes = Convert.FromBase64String(base64Image);
                using (var stream = new MemoryStream(imageBytes))
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();

                    var ellipseGeometry = new EllipseGeometry
                    {
                        Center = new Point(diameter / 2, diameter / 2),
                        RadiusX = diameter / 2,
                        RadiusY = diameter / 2
                    };

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
                byte[] imageBytes = File.ReadAllBytes(imagePath);

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
            if (sender is ToggleButton clickedButton)
            {
                // Odznacz wszystkie inne przyciski w panelu SideBar
                foreach (var child in SideBarPanel.Children)
                {
                    if (child is ToggleButton button && button != clickedButton)
                    {
                        button.IsChecked = false;
                    }
                }

                // Ustaw kliknięty przycisk jako aktywny
                clickedButton.IsChecked = true;
            }
        }


        private string _currentQuizName;

        //Check if quiz from listbox clicked
        private void QuizListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isLoadingQuizzes || QuizListBox.SelectedIndex == -1)
            {
                return;
            }

            if (QuizListBox.SelectedItem is Item selectedItem && selectedItem.Questions != null)
            {
                _currentQuizName = selectedItem.Name;

                LoadQuiz(selectedItem.Questions);

                

                QuizListBox.SelectedIndex = -1;
            }
        }
        

        private void BackToHomeButtonFromQuizView_Click(object sender, RoutedEventArgs e)
        {
            _score = 0;
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

        private async void DisplayQuestion()
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

            if (!string.IsNullOrEmpty(Convert.ToString(currentQuestion.Image)))
            {
                AzureBlobAPI azureBlobAPI = new AzureBlobAPI();
                var imageSource = await azureBlobAPI.GetBlobImageAsync($"files{CurrentUser.UserId}", $"{_currentQuizName}_{_currentQuestionIndex}");
                if (imageSource != null)
                {
                    QuizQuestionImage.Source = imageSource;
                    QuizQuestionImage.Visibility = Visibility.Visible;
                }
                else
                {
                    QuizQuestionImage.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                QuizQuestionImage.Visibility = Visibility.Hidden;
            }

            if (currentQuestion.Answers != null && currentQuestion.Answers.Count >= 1 && currentQuestion.Answers.Count <= 4)
            {
                var random = new Random();
                var shuffledAnswers = currentQuestion.Answers.OrderBy(x => random.Next()).ToList();

                var answerButtons = new List<Button> { QuizButton1, QuizButton2, QuizButton3, QuizButton4 };

                // Reset button visibility and click events
                foreach (var button in answerButtons)
                {
                    button.Visibility = Visibility.Hidden;
                    button.Click -= AnswerButton_Click;
                }

                // Assign answers to visible buttons
                for (int i = 0; i < shuffledAnswers.Count; i++)
                {
                    answerButtons[i].Content = shuffledAnswers[i];
                    answerButtons[i].Visibility = Visibility.Visible;
                    answerButtons[i].Click += AnswerButton_Click;
                }
            }
            else
            {
                MessageBox.Show("Nieprawidłowa liczba odpowiedzi w pytaniu.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private int _score = 0;
        private async void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedButton)
            {
                var currentQuestion = _currentQuizQuestions[_currentQuestionIndex];

                if(clickedButton.Content == currentQuestion.Answers[currentQuestion.CorrectAnswerIndex])
                {
                    _score++;
                }

                _currentQuestionIndex++;
                if (_currentQuestionIndex < _currentQuizQuestions.Count)
                {
                    await Task.Delay(150);
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

        ///EDIT Quizes

        private async void EditQuizButton_Click(object sender, RoutedEventArgs e)
        {
            // Pobierz quiz z DataContext przycisku
            var button = sender as Button;
            var quiz = button?.DataContext as Item;

            if (quiz == null)
            {
                MessageBox.Show("Nie udało się zidentyfikować wybranego quizu.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Pobierz nazwę quizu
            var quizName = quiz.Name;
            if (string.IsNullOrWhiteSpace(quizName))
            {
                MessageBox.Show("Wybrany quiz nie ma nazwy.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Przypisz nazwę do zmiennej _currentQuizName
            _currentQuizName = quizName;

            ShowOnlySelectedBorder("EditQuizBorder");

            // Mapowanie pytań
            var allQuestions = quiz.Questions
                ?.Select((q, index) => new EditableQuestion
                {
                    QuestionText = q.QuestionText,
                    CorrectAnswer = q.Answers[q.CorrectAnswerIndex],
                    OtherAnswersList = new ObservableCollection<Answer>(
                        q.Answers
                            .Where((ans, i) => i != q.CorrectAnswerIndex)
                            .Select(answerText => new Answer { Text = answerText })
                    ),
                    // Pobierz obraz pytania, jeśli istnieje
                    Image = string.IsNullOrEmpty(Convert.ToString(q.Image))
                        ? null : Task.Run(async () =>
                        {
                            AzureBlobAPI azureBlobAPI = new AzureBlobAPI();
                            return await azureBlobAPI.GetBlobImageAsync($"files{CurrentUser.UserId}", $"{quizName}_{index}");
                        }).Result
                })
                .ToList();

            if (allQuestions == null || allQuestions.Count == 0)
            {
                MessageBox.Show("Wybrany quiz nie zawiera pytań.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Przypisz pytania do ListView
            EditQuizListView.ItemsSource = new ObservableCollection<EditableQuestion>(allQuestions);
        }

        private void SaveQuizButton_Click(object sender, RoutedEventArgs e)
        {
            // Zapisz quiz lub wykonaj odpowiednią akcję

            // Po zapisaniu wróć do głównego ekranu
            HomeBorder.Visibility = Visibility.Visible;
            EditQuizBorder.Visibility = Visibility.Hidden;
        }

        private void AddQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            // Tworzenie nowego pytania
            EditableQuestion newQuestion = new EditableQuestion
            {
                QuestionText = string.Empty,
                CorrectAnswer = string.Empty,
                OtherAnswersList = new ObservableCollection<Answer> // Inicjalizacja z pustymi odpowiedziami
                {
                    new Answer { Text = string.Empty }, // Pierwsza odpowiedź
                    new Answer { Text = string.Empty }, // Druga odpowiedź
                    new Answer { Text = string.Empty }  // Trzecia odpowiedź
                }
            };

            // Pobieranie aktualnej listy pytań i dodanie nowego pytania
            if (EditQuizListView.ItemsSource is ObservableCollection<EditableQuestion> currentQuestions)
            {
                currentQuestions.Add(newQuestion);
            }
            else
            {
                EditQuizListView.ItemsSource = new ObservableCollection<EditableQuestion> { newQuestion };
            }
        }

        private void RemoveAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button &&
                button.CommandParameter is Answer answerToRemove &&
                EditQuizListView.SelectedItem is EditableQuestion selectedQuestion)
            {
                // Usuń odpowiedź
                selectedQuestion.OtherAnswersList.Remove(answerToRemove);
            }
        }



        private void AddAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            if (EditQuizListView.SelectedItem is EditableQuestion selectedQuestion)
            {
                // Sprawdź, czy liczba odpowiedzi nie przekracza 3
                if (selectedQuestion.OtherAnswersList.Count >= 3)
                {
                    MessageBox.Show("Nie można dodać więcej niż 3 odpowiedzi.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Dodaj nową odpowiedź
                selectedQuestion.OtherAnswersList.Add(new Answer { Text = string.Empty });
            }
            else
            {
                MessageBox.Show("Nie wybrano pytania do edycji.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }




        private void AddAnswerToLastPosition_Click(object sender, RoutedEventArgs e)
        {
            if (EditQuizListView.SelectedItem is EditableQuestion selectedQuestion)
            {
                // Sprawdź, czy liczba odpowiedzi nie przekracza 3
                if (selectedQuestion.OtherAnswersList.Count >= 3)
                {
                    MessageBox.Show("Nie można dodać więcej niż 3 odpowiedzi.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Dodaj nowe puste pole odpowiedzi
                selectedQuestion.OtherAnswersList.Add(new Answer { Text = string.Empty });
            }
            else
            {
                MessageBox.Show("Nie wybrano pytania do edycji.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RemoveQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is EditableQuestion questionToRemove &&
                EditQuizListView.ItemsSource is ObservableCollection<EditableQuestion> currentQuestions)
            {
                var result = MessageBox.Show(
                    "Czy na pewno chcesz usunąć to pytanie?",
                    "Potwierdzenie usunięcia",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    currentQuestions.Remove(questionToRemove);
                }
            }
            else
            {
                MessageBox.Show("Nie udało się usunąć pytania.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ChangeImageButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.CommandParameter is EditableQuestion question)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    string filePath = openFileDialog.FileName;

                    // Wczytaj obraz do pytania
                    question.Image = new BitmapImage(new Uri(filePath));

                    // Zmień wartość Image w JSON quizu
                    UpdateQuestionImageInQuizJson(question, filePath);
                }
            }
        }

        private void UpdateQuestionImageInQuizJson(EditableQuestion question, string imagePath)
        {
            try
            {
                string quizzesPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Quizzy", "Quizes");
                string quizFilePath = System.IO.Path.Combine(quizzesPath, $"{_currentQuizName}.json");

                if (!File.Exists(quizFilePath))
                {
                    MessageBox.Show("Plik quizu nie został znaleziony.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Odczytaj istniejący JSON
                string jsonContent = File.ReadAllText(quizFilePath);
                Quiz quiz = JsonSerializer.Deserialize<Quiz>(jsonContent);

                // Znajdź pytanie, które należy zaktualizować
                var targetQuestion = quiz?.Questions.FirstOrDefault(q => q.QuestionText == question.QuestionText);
                if (targetQuestion == null)
                {
                    MessageBox.Show("Nie znaleziono pytania w quizie.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Zmień wartość Image na identyfikator obrazu (np. "1")
                targetQuestion.Image = 1; // Możesz zmienić wartość na dowolną, np. indeks obrazu

                // Zapisz zaktualizowany JSON
                string updatedJson = JsonSerializer.Serialize(quiz, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(quizFilePath, updatedJson);

                MessageBox.Show("Obraz został pomyślnie zaktualizowany w quizie.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd podczas aktualizacji obrazu w quizie: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void SaveEditedQuizButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Pobierz zmodyfikowane pytania z ListView
                var updatedQuestions = EditQuizListView.ItemsSource as ObservableCollection<EditableQuestion>;

                // Ścieżka do pliku JSON
                string jsonPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Quizzy", "Quizes", $"{_currentQuizName}.json");

                // Załaduj istniejący quiz lub utwórz nowy, jeśli nie istnieje
                Quiz quiz = null;
                if (File.Exists(jsonPath))
                {
                    string existingJson = File.ReadAllText(jsonPath);
                    quiz = JsonSerializer.Deserialize<Quiz>(existingJson);
                }

                if (quiz == null)
                {
                    quiz = new Quiz { Name = _currentQuizName, Questions = new List<Question>() };
                }

                // AzureBlobAPI - instancja klasy do zarządzania przesyłaniem plików
                AzureBlobAPI azureBlobAPI = new AzureBlobAPI();

                // Stwórz listę pytań w odpowiednim formacie do zapisania
                var questionsToSave = new List<Question>();
                if (updatedQuestions != null)
                {
                    foreach (var q in updatedQuestions)
                    {
                        var existingQuestion = quiz.Questions?.FirstOrDefault(oldQ => oldQ.QuestionText == q.QuestionText);

                        int? imageId = existingQuestion?.Image; // Zachowaj istniejącą wartość obrazu

                        if (q.Image is BitmapImage bitmapImage)
                        {
                            // Zapisz obraz do tymczasowego pliku
                            string tempFilePath = System.IO.Path.GetTempFileName();
                            using (var fileStream = new FileStream(tempFilePath, FileMode.Create))
                            {
                                var encoder = new PngBitmapEncoder();
                                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                                encoder.Save(fileStream);
                            }

                            // Prześlij obraz do Azure Blob Storage
                            string containerName = $"files{CurrentUser.UserId}";
                            string fileName = $"{_currentQuizName}_{updatedQuestions.IndexOf(q)}";
                            await azureBlobAPI.UploadBlobToApi(tempFilePath, containerName, fileName);

                            // Usuń tymczasowy plik
                            File.Delete(tempFilePath);

                            imageId = 1; // Zmień na 1, jeśli obraz został dodany lub zmieniony
                        }

                        // Dodaj pytanie do listy
                        questionsToSave.Add(new Question
                        {
                            QuestionText = q.QuestionText,
                            CorrectAnswerIndex = 0, // Pierwsza odpowiedź jest poprawna
                            Answers = new List<string> { q.CorrectAnswer }.Concat(q.OtherAnswersList.Select(a => a.Text)).ToList(),
                            Image = q.Image == null ? null : existingQuestion?.Image ?? 1 // Ustaw na null, jeśli obraz został usunięty
                        });
                    }
                }

                // Przypisz zmodyfikowaną listę pytań do quizu
                quiz.Questions = questionsToSave;

                // Zapisz quiz do pliku JSON
                string jsonContent = JsonSerializer.Serialize(quiz, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(jsonPath, jsonContent);

                MessageBox.Show("Quiz zapisany pomyślnie!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadAllQuizzes();
                HomeBorder.Visibility = Visibility.Visible;
                EditQuizBorder.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd podczas zapisywania quizu: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void RemoveImageButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.CommandParameter is EditableQuestion question)
            {
                // Ustaw obraz na null w pytaniu
                question.Image = null;

                MessageBox.Show("Obraz został usunięty.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ResetEditQuizData()
        {
            // Ustawienia ListView na pustą kolekcję
            EditQuizListView.ItemsSource = null;

            // Reset innych danych edytora (opcjonalnie)
            _currentQuizName = string.Empty;
            _currentQuizQuestions.Clear();
        }


    }
}