using McbaExampleWithLogin.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace McbaExample.Models
{
    //Viewmodel for presenting the bill create page
    public class BillScheduleModel
    {
        public Customer Customer { get; set; }
        public BillPay Billpay { get; set; } = new BillPay();

        [Required]
        public int SelectedAccount { get; set; }

        public int SelectedPayee { get; set; }

        [Required]
        [Display(Name = "Payee")]
        public Dictionary<int, string> Payees = new Dictionary<int, string>();

        //Dictionary for the payee dropdown
        public void SetPayeeDictionary(List<Payee> pList)
        {
            foreach (var payee in pList)
            {
                Payees.Add(payee.PayeeID, payee.Name);
            }
        }
    }
}