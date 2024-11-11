**Quizzy**

**Overview**
This project is a Windows Presentation Foundation (WPF) desktop application, built to provide an engaging and interactive quiz experience. The application includes functionalities for users to register, log in, customize their profile, and participate in quizzes.

**Features**
- **User Registration and Login**: Users can register with an email and password, and log in using either their email or login name.
- **User Authentication**: Secure login mechanism utilizing SQL Server to authenticate users.
- **Customizable User Profiles**: Users can upload an avatar or choose from default avatars available.
- **Dynamic Panels**: The application uses animations for showing and hiding different panels like login, registration, and settings.
- **Validation**: Input validation for email and password during registration, ensuring a strong password policy.
- **Custom Avatars**: Users can set a default avatar or upload a personal image to represent their profile.
- **User-Specific Settings**: Each user has their own settings, stored in the database, including avatar image and login preferences.

**Technologies Used**
- **.NET Framework** with **WPF** for the desktop application.
- **C#** for implementing business logic.
- **SQL Server** for user data storage and retrieval.
- **Microsoft Win32** APIs for interacting with the file system and managing resources.
- **XAML** for defining the UI elements and layout.

**Application Structure**
- **App.xaml.cs**: This file contains global settings for the application, including registration, login, and user profile functionalities.
- **MainScreen.xaml.cs**: Contains the logic for the main user interface, including avatar settings, sidebar navigation, and overall layout.
- **StartScreen.xaml.cs**: Manages the login and registration panels with animations and validation checks for user input.

**How to Run the Application**
1. Clone the repository to your local machine.
2. Ensure you have **.NET Framework 4.7.2** or higher installed.
3. Update the `App.config` file with your SQL Server connection string.
4. Build and run the project using **Visual Studio 2019** or later.

**Dependencies**
- **Microsoft.Win32** for file dialog and system resource access.
- **System.Data.SqlClient** for database connectivity.
- **System.Windows** namespaces for UI elements.

**Setup Instructions**
1. Open the solution file in Visual Studio.
2. Replace the placeholder connection string in `App.config` with your own SQL Server connection details.
3. Run the project by pressing `F5` or selecting **Debug > Start Debugging** in Visual Studio.

**Key Functionalities Explained**
**User Registration & Login**
- Users can register with a unique login and email. Passwords are currently stored in plain text but encryption will be added in future updates.
- The `LoginUser` function in `App.xaml.cs` authenticates users against the stored credentials in the SQL Server database.

**Avatars and Profile Management**
- Users can upload custom avatars or select from a set of predefined images located in the `Resources/MainScreen/Avatars` directory.
- Avatar images are stored as byte arrays in the database, providing a consistent experience across user sessions.

**Animated UI Panels**
- The `StartScreen` uses animations to transition between login and registration panels, providing a smooth and modern user experience.
- XAML-defined storyboards (`FadeIn` and `FadeOut`) are used to manage these animations seamlessly.

**Contributing**
Contributions are welcome! If you would like to add new features or improve existing ones:
- Fork this repository.
- Create a new branch for your feature or bugfix.
- Submit a pull request with a clear explanation of your changes.

**License**
This project is licensed under the MIT License. Feel free to use it in your own projects.

**Contact**
For any inquiries, you can reach me at GitHub

**Screenshots**
- **Login Screen**: ![Login Screen](screenshots/login_screen.png)
- **Main Screen**: ![Main Screen](screenshots/main_screen.png)

Feel free to explore the application and contribute to make it even better!

