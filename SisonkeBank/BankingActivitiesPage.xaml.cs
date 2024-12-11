/* Name of the file: BankingActivitiesPage.xaml.cs
*Name of the author: Abisola O Adeyanju
*Date created: 19/06/2024
*Operating system: Cross-platform
*Version: 1.0
*Description of the code: Code-behind for the banking activities page, displaying transaction history and details.
*/


using Xamarin.Forms;
using System;
using SisonkeBank.Model; 
using System.Collections.ObjectModel;

namespace SisonkeBank
{
    public partial class BankingActivitiesPage : ContentPage
    {
        public BankingActivitiesPage()
        {
            InitializeComponent();

            // Example: Initializing transactions list
            ObservableCollection<Transaction> transactions = new ObservableCollection<Transaction>
            {
                new Transaction { Date = DateTime.Today, Description = "Deposit", Amount = 100 },
                new Transaction { Date = DateTime.Today.AddDays(-1), Description = "Withdrawal", Amount = -50 }
            };

            transactionsListView.ItemsSource = transactions;
        }
    }
}
