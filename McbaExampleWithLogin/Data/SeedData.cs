using McbaExample.Models;
using Newtonsoft.Json;

namespace McbaExample.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<McbaContext>();

        // Look for customers.
        if(context.Customers.Any())
            return; // DB has already been seeded.


                context.Customers.AddRange(
            new Customer
            {
                CustomerID = 2100,
                Name = "Matthew Bolger",
                Address = "123 Fake Street",
                City = "Melbourne",
                PostCode = "3000"
            },
            new Customer
            {
                CustomerID = 2200,
                Name = "Rodney Cocker",
                Address = "456 Real Road",
                City = "Melbourne",
                PostCode = "3005"
            },
            new Customer
            {
                CustomerID = 2300,
                Name = "Shekhar Kalra"
            });

        context.Logins.AddRange(
            new Login
            {
                LoginID = "12345678",
                CustomerID = 2100,
                PasswordHash = "YBNbEL4Lk8yMEWxiKkGBeoILHTU7WZ9n8jJSy8TNx0DAzNEFVsIVNRktiQV+I8d2"
            },
            new Login
            {
                LoginID = "38074569",
                CustomerID = 2200,
                PasswordHash = "EehwB3qMkWImf/fQPlhcka6pBMZBLlPWyiDW6NLkAh4ZFu2KNDQKONxElNsg7V04"
            },
            new Login
            {
                LoginID = "17963428",
                CustomerID = 2300,
                PasswordHash = "LuiVJWbY4A3y1SilhMU5P00K54cGEvClx5Y+xWHq7VpyIUe5fe7m+WeI0iwid7GE"
            });

        context.Accounts.AddRange(
            new Account
            {
                AccountNumber = 4100,
                AccountType = AccountType.Saving,
                CustomerID = 2100,
                Balance = 100
            },
            new Account
            {
                AccountNumber = 4101,
                AccountType = AccountType.Checking,
                CustomerID = 2100,
                Balance = 900
            },
            new Account
            {
                AccountNumber = 4200,
                AccountType = AccountType.Saving,
                CustomerID = 2200,
                Balance = 500.95m
            },
            new Account
            {
                AccountNumber = 4300,
                AccountType = AccountType.Checking,
                CustomerID = 2300,
                Balance = 1250.50m
            });
            
        const string format = "dd/MM/yyyy hh:mm:ss tt";

        context.Transactions.AddRange(
            new Transaction
            {
                TransactionType = TransactionType.Deposit,
                AccountNumber = 4100,
                Amount = 100,
                Comment = "Opening balance",
                TransactionTimeUtc = DateTime.ParseExact("03/01/2022 08:00:00 PM", format, null)
            },
            new Transaction
            {
                TransactionType = TransactionType.Deposit,
                AccountNumber = 4101,
                Amount = 600,
                Comment = "First deposit",
                TransactionTimeUtc = DateTime.ParseExact("03/01/2022 08:30:00 PM", format, null)
            },
            new Transaction
            {
                TransactionType = TransactionType.Deposit,
                AccountNumber = 4101,
                Amount = 300,
                Comment = "Second deposit",
                TransactionTimeUtc = DateTime.ParseExact("03/01/2022 08:45:00 PM", format, null)
            },
            new Transaction
            {
                TransactionType = TransactionType.Deposit,
                AccountNumber = 4200,
                Amount = 500,
                Comment = "Deposited $500",
                TransactionTimeUtc = DateTime.ParseExact("03/01/2022 09:00:00 PM", format, null)
            },
            new Transaction
            {
                TransactionType = TransactionType.Deposit,
                AccountNumber = 4200,
                Amount = 0.95m,
                Comment = "Deposited $0.95",
                TransactionTimeUtc = DateTime.ParseExact("03/01/2022 09:15:00 PM", format, null)
            },
            new Transaction
            {
                TransactionType = TransactionType.Deposit,
                AccountNumber = 4300,
                Amount = 1250.50m,
                Comment = null,
                TransactionTimeUtc = DateTime.ParseExact("03/01/2022 10:00:00 PM", format, null)
            });

        context.SaveChanges();
    }
}
