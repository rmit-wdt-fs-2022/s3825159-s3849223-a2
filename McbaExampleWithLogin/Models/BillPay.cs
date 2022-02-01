
using McbaExample.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace McbaExampleWithLogin.Models
{
    public class BillPay
    {
        public int BillPayID { get; set; }

       
        public int AccountNumber { get; set; }
       

    
       
        public int PayeeID { get; set; }
   

        [Required]
        [Column(TypeName = "money")]
        [DataType(DataType.Currency)]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter a valid amount")]
        public decimal Amount { get; set; }

        [Required]
       // [ValidDate(ErrorMessage = "Date must be greater than or equal to today")]
        [DataType(DataType.DateTime)]
        public DateTime ScheduleDate { get; set; } = DateTime.UtcNow;

        public enum Periods
        {
            [Display(Name = "Once Off")]
            OnceOff = 'S',
            Monthly = 'M',
            Quarterly = 'Q',
            Annually = 'Y'
        }

        [Required]
        public Periods Period { get; set; }

        [Required]
        public DateTime ModifyDate { get; set; }
    }
}
