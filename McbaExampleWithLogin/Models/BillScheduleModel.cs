using McbaExampleWithLogin.Models;

namespace McbaExample.Models
{
    //ViewModel for presenting the BillSchedule page
    public class BillSchedule
    {
        public Account account;
        public List<Payee> payees;
        public List<NameBill> bills;


        //LINQ for getting a list of bills matching payees.
        //Made in order to obtain payee names
        public BillSchedule(Account a, List<Payee> p)
        {
            bills =
                (from bill in a.Bills
                 join payee in p
                 on bill.PayeeID equals payee.PayeeID
                 select new NameBill { PayeeName = payee.Name, Bill = bill })
                 .ToList(); //comment if problems

        }

        //Object for combining payees and matching bills
        public class NameBill
        {
            public string PayeeName { get; set; }
            public BillPay Bill { get; set; }
        }
    }
}
