using System;
using System.Collections.Generic;

namespace BankSystem {
public class Stock
    {
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Stock(string symbol, decimal price, int quantity = 0)
        {
            Symbol = symbol;
            Price = price;
            Quantity = quantity;
        }
    }
}