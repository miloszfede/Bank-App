using System;
using System.Collections.Generic;

namespace BankSystem {
public class Bank
{
    public List<Customer> Customers { get; private set; }
    public decimal StockCommission { get; set; }

    public Bank(decimal stockCommission)
    {
        Customers = new List<Customer>();
        StockCommission = stockCommission;
    }

    public void AddCustomer(Customer customer)
    {
        Customers.Add(customer);
    }
}
}