using System;
using System.Collections.Generic;

namespace BankSystem {
public class SavingsAccount : Account
    {
        public decimal InterestRate { get; set; }

        public SavingsAccount(decimal initialBalance, decimal interestRate)
        {
            Balance = initialBalance;
            InterestRate = interestRate;
        }

        public void ApplyInterest()
        {
            Balance += Balance * InterestRate;
        }
    }
}