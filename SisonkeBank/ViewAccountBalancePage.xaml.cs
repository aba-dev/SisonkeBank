/* Name of the file: ViewAccountBalancePage.xaml.cs
*Name of the author: Abisola O Adeyanju
*Date created: 19/06/2024
*Operating system: Cross-platform
*Version: 1.0
*Description of the code: Code-behind for the page displaying account balances in the Sisonke Bank app. Retrieves and displays user account balances.
*/


using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SisonkeBank
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewAccountBalancePage : ContentPage
    {
        private User currentUser;

        public ViewAccountBalancePage(User user)
        {
            InitializeComponent();
            currentUser = user;
            UpdateAccountDetails();
        }

        private async void UpdateAccountDetails()
        {
            try
            {
                // Retrieve user details including balances from database
                User userFromDb = await DatabaseService.GetUserByEmailAsync(currentUser.Email);

                if (userFromDb != null)
                {
                    // Display account balances
                    currentAccountLabel.Text = $"Current Account Balance: {userFromDb.CurrentBalance:C}";
                    savingsAccountLabel.Text = $"Savings Account Balance: {userFromDb.SavingsBalance:C}";
                }
                else
                {
                    await DisplayAlert("Error", "User details not found", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to retrieve account balances: {ex.Message}", "OK");
            }
        }

        async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}