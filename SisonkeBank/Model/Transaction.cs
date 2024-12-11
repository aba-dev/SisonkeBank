using SQLite;
using System;
using SisonkeBank.Model;

namespace SisonkeBank.Model
{
    public class Transaction
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Type { get; set; } // e.g., "Deposit", "Withdrawal"
    }
}

