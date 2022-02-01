using WebApi.Data;
using WebApi.Models.Repository;

namespace WebApi.Models.DataManager;

public class TransactionManager : IDataRepository<Transaction, int>
{
    private readonly MCBAContext _context;

    public TransactionManager(MCBAContext context)
    {
        _context = context;
    }

    public Transaction Get(int id)
    {
        return _context.Transactions.Find(id);
    }

    public IEnumerable<Transaction> GetAll()
    {
        return _context.Transactions.ToList();
    }

    public int Add(Transaction movie)
    {
        _context.Transactions.Add(movie);
        _context.SaveChanges();

        return movie.TransactionID;
    }

    public int Delete(int id)
    {
        _context.Transactions.Remove(_context.Transactions.Find(id));
        _context.SaveChanges();

        return id;
    }

    public int Update(int id, Transaction Transaction)
    {
        _context.Update(Transaction);
        _context.SaveChanges();

        return id;
    }
}
