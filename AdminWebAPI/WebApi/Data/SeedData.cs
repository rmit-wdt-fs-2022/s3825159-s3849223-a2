using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new MCBAContext(
            serviceProvider.GetRequiredService<DbContextOptions<MCBAContext>>());

        // Look for any movies.
        if(context.Customers.Any())
            return; // DB has been seeded.

        
        

        context.SaveChanges();
    }
}
