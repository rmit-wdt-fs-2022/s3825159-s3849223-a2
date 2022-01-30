using Microsoft.AspNetCore.Mvc;
using McbaExample.Data;
using McbaExample.Models;
using McbaExample.Utilities;
using McbaExampleWithLogin.Filters;

namespace McbaExample.Controllers;

// Can add authorize attribute to controllers.
[AuthorizeCustomer]
public class CustomerController : Controller
{
    private readonly McbaContext _context;

    // ReSharper disable once PossibleInvalidOperationException
    private int CustomerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;

    public CustomerController(McbaContext context) => _context = context;

    // Can add authorize attribute to actions.
    //[AuthorizeCustomer]
    public async Task<IActionResult> Index()
    {
        // Lazy loading.
        // The Customer.Accounts property will be lazy loaded upon demand.
        var customer = await _context.Customers.FindAsync(CustomerID);

        // OR
        // Eager loading.
        //var customer = await _context.Customers.Include(x => x.Accounts).
        //    FirstOrDefaultAsync(x => x.CustomerID == _customerID);

        return View(customer);
    }

    public async Task<IActionResult> Deposit(int id) => View(await _context.Accounts.FindAsync(id));

    [HttpPost]
    public async Task<IActionResult> Deposit(int id, decimal amount, string comment)
    {
        var account = await _context.Accounts.FindAsync(id);

        if(amount <= 0)
            ModelState.AddModelError(nameof(amount), "Amount must be positive.");
        if(amount.HasMoreThanTwoDecimalPlaces())
            ModelState.AddModelError(nameof(amount), "Amount cannot have more than 2 decimal places.");
        if(!ModelState.IsValid)
        {
            ViewBag.Amount = amount;
            return View(account);
        }

        // Note this code could be moved out of the controller, e.g., into the Model.
        account.Balance += amount;
        account.Transactions.Add(
            new Transaction
            {
                TransactionType = TransactionType.Deposit,
                Amount = amount,
                TransactionTimeUtc = DateTime.UtcNow,
                Comment  = comment
            });

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Withdraw(int id) => View(await _context.Accounts.FindAsync(id));

    [HttpPost]
    public async Task<IActionResult> Withdraw(int id, decimal amount, string comment)
    {
        var account = await _context.Accounts.FindAsync(id);

        if (amount <= 0)
            ModelState.AddModelError(nameof(amount), "Amount must be positive.");
        if (amount.HasMoreThanTwoDecimalPlaces())
            ModelState.AddModelError(nameof(amount), "Amount cannot have more than 2 decimal places.");
        if (amount > account.Balance)
            ModelState.AddModelError(nameof(amount), "You cannot withdraw " + amount.ToString() + " from this acccount!");
        if (!ModelState.IsValid)
        {
            ViewBag.Amount = amount;
            return View(account);
        }


        // Note this code could be moved out of the controller, e.g., into the Model.
        account.Balance -= amount;
        account.Transactions.Add(
            new Transaction
            {
                TransactionType = TransactionType.Withdraw,
                Amount = amount,
                TransactionTimeUtc = DateTime.UtcNow,
                Comment = comment
            });

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Transfer(int id) => View(await _context.Accounts.FindAsync(id));

    [HttpPost]
    public async Task<IActionResult> Transfer(int id, decimal amount, string comment, int destinationaccountnumber)
    {
        var account = await _context.Accounts.FindAsync(id);
        var acccount2 = await _context.Accounts.FindAsync(destinationaccountnumber);

        if (amount <= 0)
            ModelState.AddModelError(nameof(amount), "Amount must be positive.");
        if (amount.HasMoreThanTwoDecimalPlaces())
            ModelState.AddModelError(nameof(amount), "Amount cannot have more than 2 decimal places.");
        if (amount > account.Balance)
            ModelState.AddModelError(nameof(amount), "You cannot withdraw " + amount.ToString() + " from this acccount!");
        if (!ModelState.IsValid)
        {
            ViewBag.Amount = amount;
            return View(account);
        }


        //Balance updating
        account.Balance -= amount;
        acccount2.Balance += amount;

        //Adding the transactions
        account.Transactions.Add(
            new Transaction
            {
                TransactionType = TransactionType.Transfer,
                Amount = amount,
                TransactionTimeUtc = DateTime.UtcNow,
                Comment = comment,
                DestinationAccountNumber = destinationaccountnumber
            });



        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> UpdateProfile(int id)
    {
        return View(await _context.Customers.FindAsync(id));
    }

    public async Task<IActionResult> Statements(int id) => View(await _context.Accounts.FindAsync(id));
}
