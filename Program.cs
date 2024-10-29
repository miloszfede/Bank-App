using System;

namespace BankSystem
{
    public class Program
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank(5.0m);  // $5 commission on stock trades

            while (true)
            {
                Console.WriteLine("\nBank System Menu:");
                Console.WriteLine("1. Add Customer");
                Console.WriteLine("2. Add Account to Customer");
                Console.WriteLine("3. Deposit");
                Console.WriteLine("4. Withdraw");
                Console.WriteLine("5. Buy Stock");
                Console.WriteLine("6. Apply Interest to Savings Accounts");
                Console.WriteLine("7. Display Customer Information");
                Console.WriteLine("8. Exit");

                Console.Write("Enter your choice: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        BankOperations.AddCustomer(bank);
                        break;
                    case "2":
                        BankOperations.AddAccountToCustomer(bank);
                        break;
                    case "3":
                        BankOperations.Deposit(bank);
                        break;
                    case "4":
                        BankOperations.Withdraw(bank);
                        break;
                    case "5":
                        BankOperations.BuyStock(bank);
                        break;
                    case "6":
                        BankOperations.ApplyInterest(bank);
                        break;
                    case "7":
                        BankOperations.DisplayCustomerInfo(bank);
                        break;
                    case "8":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}