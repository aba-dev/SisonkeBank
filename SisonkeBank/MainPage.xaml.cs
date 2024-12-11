/* Name of the file: MainPage.xaml.cs
*Name of the author: Abisola O Adeyanju
*Date created: 19/06/2024
*Operating system: Cross-platform
*Version: 1.0
*Description of the code: Code-behind for the main page of the Sisonke Bank app, handling navigation and user interactions.
*/


using System;
using Xamarin.Forms;

namespace SisonkeBank
{
    public partial class MainPage : ContentPage
    {
        private User currentUser; // Assuming you have a User model

        public MainPage(User user)
        {
            InitializeComponent();
            currentUser = user;
            UpdateWelcomeMessage();
        }

        private void UpdateWelcomeMessage()
        {
            welcomeLabel.Text = $"Welcome, {currentUser.FirstName}";
        }

        async void OnViewAccountBalanceClicked(object sender, EventArgs e)
        {
            try
            {
                // Assuming you have a DatabaseService or similar to retrieve user data
                currentUser = await DatabaseService.GetUserByEmailAsync(currentUser.Email);
                if (currentUser != null)
                {
                    await Navigation.PushAsync(new ViewAccountBalancePage(currentUser));
                }
                else
                {
                    await DisplayAlert("Error", "Failed to retrieve user data", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to navigate to Account Balance: {ex.Message}", "OK");
            }
        }

        async void OnTransferBetweenAccountsClicked(object sender, EventArgs e)
        {
            try
            {
                // Assuming you have a DatabaseService or similar to retrieve user data
                currentUser = await DatabaseService.GetUserByEmailAsync(currentUser.Email);
                if (currentUser != null)
                {
                    await Navigation.PushAsync(new TransferBetweenAccountsPage(currentUser));
                }
                else
                {
                    await DisplayAlert("Error", "Failed to retrieve user data", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to navigate to Transfer Funds: {ex.Message}", "OK");
            }
        }

        async void OnLogoutClicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PopToRootAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to logout: {ex.Message}", "OK");
            }
        }

        private async void OnViewBankingActivitiesClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BankingActivitiesPage());
        }

    }
}
