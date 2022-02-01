using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data;

public class MCBAContext : DbContext
{
    public MCBAContext (DbContextOptions<MCBAContext> options) : base(options)
    { }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
}
