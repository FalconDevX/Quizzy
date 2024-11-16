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

namespace WPF
{
    public partial class MainScreen : Window
    {
        public MainScreen()
        {
            InitializeComponent();
            UserNameTextBlock.Text = $"Hi, {CurrentUser.Login}";
            SideBarNickTextBlock.Text = CurrentUser.Login;
            UserEmailSettingsTextBlock.Text = CurrentUser.Email;
            UserLoginSettingsTextBlock.Text = CurrentUser.Login;
            SetAvatar();
        }

        ///SIDEBAR///

        private void SideBarButton_Click(object sender, RoutedEventArgs e)
        {
            // Odznacz wszystkie przyciski w SideBarPanel
            foreach (var child in SideBarPanel.Children)
            {
                if (child is ToggleButton button && button != sender)
                {
                    button.IsChecked = false;
                }
            }
        }


        //Home button clicked
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            SideBarButton_Click(sender, new RoutedEventArgs());
            ShowOnlySelectedBorder("HomeBorder");
        }

        //Settings button clicked
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
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
        public void SetDefaultAvatar(int avatarNumber)
        {
            string avatarPath = $"/Resources/MainScreen/Avatars/Avatar{avatarNumber}.png";
            Uri resourceUri = new Uri(avatarPath, UriKind.Relative);
            StreamResourceInfo resourceInfo = Application.GetResourceStream(resourceUri);

            if (resourceInfo != null)
            {
                using (var stream = resourceInfo.Stream)
                {
                    using (BinaryReader br = new BinaryReader(stream))
                    {
                        CurrentUser.Avatar = br.ReadBytes((int)stream.Length);
                    }
                }
            }

            UserService userService = new UserService();
            userService.SaveAvatarToDatabase();

            SetAvatar();
        }

        //Setting avatar to current user
        private void SetAvatar()
        {
            UserService userService = new UserService();
            userService.GetUserAvatar(CurrentUser.UserId);

            if (CurrentUser.Avatar != null && CurrentUser.Avatar.Length > 0)
            {
                using (var stream = new MemoryStream(CurrentUser.Avatar))
                {
                    var imageSource = new BitmapImage();
                    imageSource.BeginInit();
                    imageSource.StreamSource = stream;
                    imageSource.CacheOption = BitmapCacheOption.OnLoad;
                    imageSource.EndInit();
                    UserAvatarImage.Source = imageSource;
                    SidebarUserPhoto.Source = imageSource;
                    AvatarUserPhoto.Source = imageSource;

                    UserAvatarImage.Width = 150;
                    UserAvatarImage.Height = 150;

                    AvatarUserPhoto.Height = 150;
                    AvatarUserPhoto.Width = 150;

                    SidebarUserPhoto.Width = 88;
                    SidebarUserPhoto.Height = 88;

                    UserAvatarImage.Stretch = Stretch.UniformToFill;
                    SidebarUserPhoto.Stretch = Stretch.UniformToFill;
                    AvatarUserPhoto.Stretch = Stretch.UniformToFill;

                    UserAvatarImage.Clip = new EllipseGeometry(new Point(UserAvatarImage.Width / 2, UserAvatarImage.Height / 2), UserAvatarImage.Width / 2, UserAvatarImage.Height / 2);
                    SidebarUserPhoto.Clip = new EllipseGeometry(new Point(SidebarUserPhoto.Width / 2, SidebarUserPhoto.Height / 2), SidebarUserPhoto.Width / 2, SidebarUserPhoto.Height / 2);
                    AvatarUserPhoto.Clip = new EllipseGeometry(new Point(AvatarUserPhoto.Width / 2, AvatarUserPhoto.Height / 2), AvatarUserPhoto.Width / 2, AvatarUserPhoto.Height / 2);
                }
            }
            else
            {
                UserAvatarImage.Source = new BitmapImage(new Uri("/Resources/MainScreen/SideBar/DefaultLogoIcon.png", UriKind.Relative));
                UserAvatarImage.Width = 100;
                UserAvatarImage.Height = 100;
                UserAvatarImage.Stretch = Stretch.UniformToFill;
                UserAvatarImage.Clip = new EllipseGeometry(new Point(UserAvatarImage.Width / 2, UserAvatarImage.Height / 2), UserAvatarImage.Width / 2, UserAvatarImage.Height / 2);
            }
        }

        //Change avatar button
        private void ChangeAvatarButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                UserService userService = new UserService();
                userService.SaveUserAvatar(CurrentUser.UserId, filePath);

                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        CurrentUser.Avatar = br.ReadBytes((int)fs.Length);
                    }
                }

                SetAvatar();
            }
            else
            {
                MessageBox.Show("Nie wybrano pliku.");
            }
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
        private void CloseWindowButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //minimize application
        private void MinimizeWindowButton_Click(Object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        //Change login button
        private void ChangeLoginButton_Click(object sender, RoutedEventArgs e)
        {
            DialogBox dialogbox = new DialogBox();
            dialogbox.ShowDialog();

            SideBarNickTextBlock.Text = CurrentUser.Login;
            UserLoginSettingsTextBlock.Text = CurrentUser.Login;
            UserNameTextBlock.Text = $"Hi, {CurrentUser.Login}";
        }
    }
}