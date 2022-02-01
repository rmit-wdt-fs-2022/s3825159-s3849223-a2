using McbaExample.Data;
using McbaExample.Models;
using McbaExampleWithLogin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace McbaExample.Controllers
{
    public class BillPayController : Controller
    {
        private readonly McbaContext _context;

        private int CustomerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;

        public BillPayController(McbaContext context) => _context = context;


        public async Task<IActionResult> CreateBill(int billID = 0)
        {
            HttpContext.Session.SetInt32("Mod", 0);
            ViewData["Mod"] = 0;
            var customer = await _context.Customers.Where(x => x.CustomerID == CustomerID).Include(x => x.Accounts).FirstOrDefaultAsync();
            var billviewmodel = new BillScheduleModel { Customer = customer };
            if (billID != 0)
            {
                var bill = await _context.BillPay.Where(x => x.BillPayID == billID).FirstOrDefaultAsync();
                billviewmodel.Billpay = bill;
                HttpContext.Session.SetInt32("Mod", billID);
                ViewData["Mod"] = billID;
            }
            var list = await _context.Payee.ToListAsync();
            billviewmodel.SetPayeeDictionary(list);
            return View(billviewmodel);

        }

        [HttpPost]


        //Method for prompting user to select account to view bills from
        public async Task<IActionResult> SelectAccount()
        {
            var accounts = await _context.Accounts.Where(x => x.CustomerID == CustomerID).ToListAsync();

            return View(accounts);
        }

        //Method for preparing BillSchedule page
        //Utilizes BillScheduleViewModel object
        public async Task<IActionResult> BillSchedule(int accountNumber)
        {
            var account = await _context.Accounts.Where(x => x.AccountNumber == accountNumber).Include(x => x.Bills).FirstOrDefaultAsync();
            var list = await _context.Payee.ToListAsync();
            BillSchedule bills = new BillSchedule(account, list);
            return View(bills);
        }

        //Method for returning partial view of balance total
        public async Task<IActionResult> SeeMyBalance(int id)
        {

            var account = await _context.Accounts.Where(x => x.AccountNumber == id).FirstOrDefaultAsync();
            return PartialView(account);

        }

        //Method for handling deletion of existing bills



    }
}
