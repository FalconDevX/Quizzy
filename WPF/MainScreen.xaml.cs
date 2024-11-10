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

namespace WPF
{
    public partial class MainScreen : Window
    {

        public MainScreen()
        {
            InitializeComponent();
            UserNameTextBlock.Text = $"Hi, {CurrentUser.Login}";
            SetAvatar();
        }

        //Setting avatar to current user
        private void SetAvatar()
        {
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

                   
                    UserAvatarImage.Width = 150; 
                    UserAvatarImage.Height = 150; 
                    SidebarUserPhoto.Width = 50;
                    SidebarUserPhoto.Height = 50; 

                  
                    UserAvatarImage.Stretch = Stretch.UniformToFill;
                    SidebarUserPhoto.Stretch = Stretch.UniformToFill;

     
                    UserAvatarImage.Clip = new EllipseGeometry(new Point(UserAvatarImage.Width / 2, UserAvatarImage.Height / 2), UserAvatarImage.Width / 2, UserAvatarImage.Height / 2);
                    SidebarUserPhoto.Clip = new EllipseGeometry(new Point(SidebarUserPhoto.Width / 2, SidebarUserPhoto.Height / 2), SidebarUserPhoto.Width / 2, SidebarUserPhoto.Height / 2);
                }
            }
            else
            {
                UserAvatarImage.Source = new BitmapImage(new Uri("/Resources/MainScreen/SideBar/DefaultLogoIcon.png", UriKind.Relative));
                UserAvatarImage.Width = 100; // Adjust as needed
                UserAvatarImage.Height = 100; // Adjust as needed
                UserAvatarImage.Stretch = Stretch.UniformToFill;
                UserAvatarImage.Clip = new EllipseGeometry(new Point(UserAvatarImage.Width / 2, UserAvatarImage.Height / 2), UserAvatarImage.Width / 2, UserAvatarImage.Height / 2);
            }
        }


        //Home button clicked
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            ShowOnlySelectedBorder("HomeBorder");
        }

        //Settings button clicked
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            ShowOnlySelectedBorder("SettingsBorder"); 
        }

        //show selected border function
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

        private void ChangeAvatarButton_Click(object sender, RoutedEventArgs e)
        {
            // Otwieranie okna dialogowego do wyboru pliku
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                UserService userService = new UserService();
                userService.SaveUserAvatar(CurrentUser.UserId, filePath);

                // Aktualizacja CurrentUser.Avatar po zapisaniu pliku
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        CurrentUser.Avatar = br.ReadBytes((int)fs.Length);
                    }
                }

                // Ustawienie nowego avatara
                SetAvatar();
            }
            else
            {
                MessageBox.Show("Nie wybrano pliku.");
            }
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

        


}
}
