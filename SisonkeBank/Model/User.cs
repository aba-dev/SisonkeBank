﻿using SQLite;

namespace SisonkeBank
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public string Gender { get; set; }
        public decimal CurrentBalance { get; set; }
        public decimal SavingsBalance { get; set; }
    }
}
