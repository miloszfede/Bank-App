using System;
using System.Collections.Generic;

namespace BankSystem {
public class InvestmentAccount : Account
    {
        public List<Stock> Stocks { get; set; }

        public InvestmentAccount(decimal initialBalance)
        {
            Balance = initialBalance;
            Stocks = new List<Stock>();
        }

        public bool BuyStock(Stock stock, int quantity, decimal commission)
        {
            decimal totalCost = stock.Price * quantity + commission;
            if (Balance >= totalCost)
            {
                Balance -= totalCost;
                stock.Quantity += quantity;
                if (!Stocks.Contains(stock))
                {
                    Stocks.Add(stock);
                }
                return true;
            }
            return false;
        }
    }
}