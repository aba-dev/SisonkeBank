/* Name of the file: LoginPage.xaml.cs
*Name of the author: Abisola O Adeyanju
*Date created: 19/06/2024
*Operating system: Cross-platform
*Version: 1.0
*Description of the code: Code-behind for the login page of the Sisonke Bank app. Handles user authentication and validation.
*/


using SisonkeBank;
using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace SisonkeBank
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        async void OnLoginClicked(object sender, EventArgs e)
        {
            string email = emailEntry.Text; // Ensure 'emailEntry' is correctly referenced in XAML
            string password = passwordEntry.Text;

            // Input validation
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Error", "Please enter email and password", "OK");
                return;
            }

            // Email format validation
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                await DisplayAlert("Error", "Invalid email format", "OK");
                return;
            }

            // Password length validation
            if (password.Length < 5)
            {
                await DisplayAlert("Error", "Password must be at least 5 characters long", "OK");
                return;
            }

            // Authenticate user
            bool isAuthenticated = await DatabaseService.AuthenticateUserAsync(email, password);
            if (isAuthenticated)
            {
                // Navigate to MainPage with user information
                User user = await DatabaseService.GetUserByEmailAsync(email);
                await Navigation.PushAsync(new MainPage(user));
            }
            else
            {
                // Display error message for invalid credentials
                await DisplayAlert("Error", "Invalid email or password", "OK");
            }
        }

        async void OnRegisterClicked(object sender, EventArgs e)
        {
            // Navigate to the registration page
            await Navigation.PushAsync(new RegistrationPage());
        }
    }
}

