using System;
using System.Collections.Generic;

namespace BankSystem
{
    public static class BankOperations
    {
        public static void AddCustomer(Bank bank)
        {
            Console.Write("Enter customer full name: ");
            string? fullName = Console.ReadLine();
            Console.Write("Enter customer home address: ");
            string? homeAddress = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(homeAddress))
            {
                Console.WriteLine("Invalid input. Customer not added.");
                return;
            }

            Customer customer = new Customer(fullName, homeAddress);
            bank.AddCustomer(customer);
            Console.WriteLine("Customer added successfully.");
        }

        public static void AddAccountToCustomer(Bank bank)
        {
            Customer? customer = FindCustomer(bank);
            if (customer == null) return;

            Console.Write("Enter account type (1 for Savings, 2 for Investment): ");
            string? accountType = Console.ReadLine();
            Console.Write("Enter initial balance: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal initialBalance))
            {
                Console.WriteLine("Invalid balance input.");
                return;
            }

            if (accountType == "1")
            {
                Console.Write("Enter interest rate (as a decimal, e.g., 0.05 for 5%): ");
                if (decimal.TryParse(Console.ReadLine(), out decimal interestRate))
                {
                    customer.Accounts.Add(new SavingsAccount(initialBalance, interestRate));
                    Console.WriteLine("Savings account added successfully.");
                }
                else
                {
                    Console.WriteLine("Invalid interest rate input.");
                }
            }
            else if (accountType == "2")
            {
                customer.Accounts.Add(new InvestmentAccount(initialBalance));
                Console.WriteLine("Investment account added successfully.");
            }
            else
            {
                Console.WriteLine("Invalid account type.");
            }
        }

        public static void Deposit(Bank bank)
        {
            Customer? customer = FindCustomer(bank);
            if (customer == null) return;

            Account? account = SelectAccount(customer);
            if (account == null) return;

            Console.Write("Enter deposit amount: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                account.Deposit(amount);
                Console.WriteLine($"Deposit successful. New balance: {account.Balance:C}");
            }
            else
            {
                Console.WriteLine("Invalid amount input.");
            }
        }

        public static void Withdraw(Bank bank)
        {
            Customer? customer = FindCustomer(bank);
            if (customer == null) return;

            Account? account = SelectAccount(customer);
            if (account == null) return;

            Console.Write("Enter withdrawal amount: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                if (account.Withdraw(amount))
                {
                    Console.WriteLine($"Withdrawal successful. New balance: {account.Balance:C}");
                }
                else
                {
                    Console.WriteLine("Insufficient funds.");
                }
            }
            else
            {
                Console.WriteLine("Invalid amount input.");
            }
        }

        public static void BuyStock(Bank bank)
        {
            Customer? customer = FindCustomer(bank);
            if (customer == null) return;

            InvestmentAccount? account = SelectAccount(customer) as InvestmentAccount;
            if (account == null)
            {
                Console.WriteLine("Selected account is not an investment account.");
                return;
            }

            Console.Write("Enter stock symbol: ");
            string? symbol = Console.ReadLine();
            Console.Write("Enter stock price: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Invalid price input.");
                return;
            }
            Console.Write("Enter quantity to buy: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity))
            {
                Console.WriteLine("Invalid quantity input.");
                return;
            }

            if (string.IsNullOrWhiteSpace(symbol))
            {
                Console.WriteLine("Invalid stock symbol.");
                return;
            }

            Stock stock = new Stock(symbol, price);
            if (account.BuyStock(stock, quantity, bank.StockCommission))
            {
                Console.WriteLine($"Stock purchase successful. New balance: {account.Balance:C}");
            }
            else
            {
                Console.WriteLine("Insufficient funds to complete the stock purchase.");
            }
        }

        public static void ApplyInterest(Bank bank)
        {
            foreach (Customer customer in bank.Customers)
            {
                foreach (Account account in customer.Accounts)
                {
                    if (account is SavingsAccount savingsAccount)
                    {
                        savingsAccount.ApplyInterest();
                        Console.WriteLine($"Applied interest for {customer.FullName}'s savings account. New balance: {savingsAccount.Balance:C}");
                    }
                }
            }
        }

        public static void DisplayCustomerInfo(Bank bank)
        {
            Customer? customer = FindCustomer(bank);
            if (customer == null) return;

            Console.WriteLine($"Customer: {customer.FullName}");
            Console.WriteLine($"Address: {customer.HomeAddress}");
            Console.WriteLine("Accounts:");
            foreach (Account account in customer.Accounts)
            {
                Console.WriteLine($"- Type: {account.GetType().Name}, Balance: {account.Balance:C}");
                if (account is SavingsAccount savingsAccount)
                {
                    Console.WriteLine($"  Interest Rate: {savingsAccount.InterestRate:P}");
                }
                else if (account is InvestmentAccount investmentAccount)
                {
                    Console.WriteLine("  Stocks:");
                    foreach (Stock stock in investmentAccount.Stocks)
                    {
                        Console.WriteLine($"    {stock.Symbol}: {stock.Quantity} shares at {stock.Price:C} each");
                    }
                }
            }
        }

        private static Customer? FindCustomer(Bank bank)
        {
            Console.Write("Enter customer full name: ");
            string? fullName = Console.ReadLine();
            Customer? customer = bank.Customers.Find(c => c.FullName.Equals(fullName, StringComparison.OrdinalIgnoreCase));
            if (customer == null)
            {
                Console.WriteLine("Customer not found.");
            }
            return customer;
        }

        private static Account? SelectAccount(Customer customer)
        {
            if (customer.Accounts.Count == 0)
            {
                Console.WriteLine("Customer has no accounts.");
                return null;
            }

            Console.WriteLine("Select an account:");
            for (int i = 0; i < customer.Accounts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {customer.Accounts[i].GetType().Name}");
            }

            Console.Write("Enter account number: ");
            if (int.TryParse(Console.ReadLine(), out int accountIndex))
            {
                accountIndex--;
                if (accountIndex >= 0 && accountIndex < customer.Accounts.Count)
                {
                    return customer.Accounts[accountIndex];
                }
            }

            Console.WriteLine("Invalid account selection.");
            return null;
        }
    }
}