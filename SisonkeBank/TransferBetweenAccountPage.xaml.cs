/* Name of the file: TransferBetweenAccountPage.xaml.cs
*Name of the author: Abisola O Adeyanju
*Date created: 19/06/2024
*Operating system: Cross-platform
*Version: 1.0
*Description of the code: Code-behind for the page allowing users to transfer funds between their accounts in the Sisonke Bank app.
*/


using System;
using Xamarin.Forms;

namespace SisonkeBank
{
    public partial class TransferBetweenAccountsPage : ContentPage
    {
        private User currentUser;

        public TransferBetweenAccountsPage(User user)
        {
            InitializeComponent();
            currentUser = user;
            UpdateAccountDetails();
        }

        private void UpdateAccountDetails()
        {
            // Display current and savings account balances for the user
            currentAccountBalanceLabel.Text = $"Current Account Balance: {currentUser.CurrentBalance:C}";
            savingsAccountBalanceLabel.Text = $"Savings Account Balance: {currentUser.SavingsBalance:C}";
        }

        async void OnTransferClicked(object sender, EventArgs e)
        {
            string amountText = amountEntry.Text;
            string transferDirection = transferDirectionPicker.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(amountText) || !decimal.TryParse(amountText, out decimal amount))
            {
                await DisplayAlert("Error", "Please enter a valid amount", "OK");
                return;
            }

            if (transferDirection == null)
            {
                await DisplayAlert("Error", "Please select a transfer direction", "OK");
                return;
            }

            bool transferSuccess = false;

            // Perform transfer based on selected direction
            if (transferDirection == "Current to Savings")
            {
                if (amount > currentUser.CurrentBalance)
                {
                    await DisplayAlert("Error", "Insufficient funds in current account", "OK");
                    return;
                }

                // Update balances
                decimal newCurrentBalance = currentUser.CurrentBalance - amount;
                decimal newSavingsBalance = currentUser.SavingsBalance + amount;

                // Update balances in database
                await DatabaseService.UpdateAccountBalancesAsync(newCurrentBalance, newSavingsBalance);

                transferSuccess = true;
            }
            else if (transferDirection == "Savings to Current")
            {
                if (amount > currentUser.SavingsBalance)
                {
                    await DisplayAlert("Error", "Insufficient funds in savings account", "OK");
                    return;
                }

                // Update balances
                decimal newCurrentBalance = currentUser.CurrentBalance + amount;
                decimal newSavingsBalance = currentUser.SavingsBalance - amount;

                // Update balances in database
                await DatabaseService.UpdateAccountBalancesAsync(newCurrentBalance, newSavingsBalance);

                transferSuccess = true;
            }

            if (transferSuccess)
            {
                await DisplayAlert("Success", "Transfer successful", "OK");
                // Refresh UI with updated balances
                UpdateAccountDetails();
            }
            else
            {
                await DisplayAlert("Error", "Transfer failed", "OK");
            }
        }

        async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
