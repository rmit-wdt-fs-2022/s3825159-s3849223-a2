using WebApi.Data;
using WebApi.Models.Repository;

namespace WebApi.Models.DataManager;

public class CustomerManager : IDataRepository<Customer, int>
{
    private readonly MCBAContext _context;

    public CustomerManager(MCBAContext context)
    {
        _context = context;
    }

    public Customer Get(int id)
    {
        return _context.Customers.Find(id);
    }

    public IEnumerable<Customer> GetAll()
    {
        return _context.Customers.ToList();
    }

    public int Add(Customer movie)
    {
        _context.Customers.Add(movie);
        _context.SaveChanges();

        return movie.CustomerID;
    }

    public int Delete(int id)
    {
        _context.Customers.Remove(_context.Customers.Find(id));
        _context.SaveChanges();

        return id;
    }

    public int Update(int id, Customer movie)
    {
        _context.Update(movie);
        _context.SaveChanges();
            
        return id;
    }
}
