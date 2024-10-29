using System;
using System.Collections.Generic;

namespace BankSystem {
public class Customer
    {
        public string FullName { get; set; }
        public string HomeAddress { get; set; }
        public List<Account> Accounts { get; set; }

        public Customer(string fullName, string homeAddress)
        {
            FullName = fullName;
            HomeAddress = homeAddress;
            Accounts = new List<Account>();
        }
    }
}