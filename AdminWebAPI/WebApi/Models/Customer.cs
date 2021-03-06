using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Validators;

namespace WebApi.Models;

public class Customer
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int CustomerID { get; set; }

    [Required, StringLength(50)]
    public string Name { get; set; }

    [StringLength(50)]
    public string Address { get; set; }

    [StringLength(40)]
    public string City { get; set; }

    [StringLength(40)]
    public string Suburb { get; set; }

    [StringLength(4)]
    public string State { get; set; }

    [StringLength(40)]
    public string TFN { get; set; }


    [StringLength(4)]
    public string PostCode { get; set; }

    public virtual List<Account> Accounts { get; set; }
}
