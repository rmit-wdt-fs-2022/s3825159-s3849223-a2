using Microsoft.AspNetCore.Mvc;
using McbaExample.Data;
using McbaExample.Models;
using McbaExample.Utilities;
using McbaExampleWithLogin.Filters;

namespace McbaExampleWithLogin.Controllers
{
    public class MyStatementsController : Controller
    {
        private readonly McbaContext _context;
        private int CustomerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;

        public MyStatementsController(McbaContext context) => _context = context;



        


    }
}
