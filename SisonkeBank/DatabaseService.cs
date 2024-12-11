/* Name of the file: DatabaseDervice.cs
*Name of the author: Abisola O Adeyanju
*Date created: 19/06/2024
*Operating system: Cross-platform
*Version: 1.0
*Description of the code: This class provides methods to interact with the SQLite database used in the Sisonke Bank app.
*/


using SQLite;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SisonkeBank
{
    public static class DatabaseService
    {
        static SQLiteAsyncConnection database;

        public static SQLiteAsyncConnection Database
        {
            get
            {
                if (database == null)
                {
                    database = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SisonkeBank.db3"));
                    InitializeDatabaseAsync().Wait(); // Make sure to avoid .Wait() in async methods to prevent deadlocks
                }
                return database;
            }
        }

        static async Task InitializeDatabaseAsync()
        {
            await Database.CreateTableAsync<User>().ConfigureAwait(false);
        }

        public static async Task<bool> AuthenticateUserAsync(string email, string password)
        {
            var user = await Database.Table<User>().Where(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();
            return user != null;
        }

        public static async Task<bool> RegisterUserAsync(User user)
        {
            var existingUser = await Database.Table<User>().Where(u => u.Email == user.Email).FirstOrDefaultAsync();
            if (existingUser != null)
                return false;

            user.CurrentBalance = 2500; // Set initial balances as per requirements
            user.SavingsBalance = 1000;

            int rowsInserted = await Database.InsertAsync(user);
            return rowsInserted > 0;
        }

        public static async Task<User> GetUserByEmailAsync(string email)
        {
            return await Database.Table<User>().Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public static async Task UpdateAccountBalancesAsync(decimal currentBalance, decimal savingsBalance)
        {
            var user = await Database.Table<User>().FirstOrDefaultAsync(); // Assuming you have a single user for now
            if (user != null)
            {
                user.CurrentBalance = currentBalance;
                user.SavingsBalance = savingsBalance;
                await Database.UpdateAsync(user);
            }
        }
    }
}