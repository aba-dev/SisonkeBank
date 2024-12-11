/* Name of the file: RegistrationPage.xaml.cs
*Name of the author: Abisola O Adeyanju
*Date created: 19/06/2024
*Operating system: Cross-platform
*Version: 1.0
*Description of the code: Code-behind for the registration page of the Sisonke Bank app. Handles user registration and validation.
*/

using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace SisonkeBank
{
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        async void OnRegisterClicked(object sender, EventArgs e)
        {
            string firstName = firstNameEntry.Text;
            string lastName = lastNameEntry.Text;
            string email = emailEntry.Text;
            string password = passwordEntry.Text;
            string mobile = mobileEntry.Text;
            string gender = genderPicker.SelectedItem?.ToString(); // Check for null

            // Input validation (optional, if already validated)
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) ||
                string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(mobile) || string.IsNullOrEmpty(gender))
            {
                await DisplayAlert("Error", "Please fill in all fields", "OK");
                return;
            }

            // Email format validation (optional, if already validated)
            if (!IsValidEmail(email))
            {
                await DisplayAlert("Error", "Invalid email format", "OK");
                return;
            }

            // Password length validation (optional, if already validated)
            if (password.Length < 5)
            {
                await DisplayAlert("Error", "Password must be at least 5 characters long", "OK");
                return;
            }

            // Mobile number validation (optional, if already validated)
            if (!Regex.IsMatch(mobile, @"^[0-9]{10}$")) // Example pattern for 10-digit number
            {
                await DisplayAlert("Error", "Invalid mobile number format", "OK");
                return;
            }

            // Assuming DatabaseService.RegisterUserAsync method to save user data
            bool registrationSuccess = await DatabaseService.RegisterUserAsync(new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password,
                MobileNumber = mobile,
                Gender = gender,
                CurrentBalance = 2500, // Initialize with R2500
                SavingsBalance = 1000  // Initialize with R1000
            });

            if (registrationSuccess)
            {
                await DisplayAlert("Success", "Registration successful", "OK");

                // Navigate to LoginPage or handle navigation as per your flow
                await Navigation.PushAsync(new LoginPage());
            }
            else
            {
                await DisplayAlert("Error", "Registration failed", "OK");
            }
        }

        bool IsValidEmail(string email)
        {
            string emailPattern = @"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$";
            return Regex.IsMatch(email, emailPattern);
        }

        async void OnLoginClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}