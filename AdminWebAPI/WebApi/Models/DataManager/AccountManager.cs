using WebApi.Data;
using WebApi.Models.Repository;

namespace WebApi.Models.DataManager;

public class AccountManager : IDataRepository<Account, int>
{
    private readonly MCBAContext _context;

    public AccountManager(MCBAContext context)
    {
        _context = context;
    }

    public Account Get(int id)
    {
        return _context.Accounts.Find(id);
    }

    public IEnumerable<Account> GetAll()
    {
        return _context.Accounts.ToList();
    }

    public int Add(Account movie)
    {
        _context.Accounts.Add(movie);
        _context.SaveChanges();

        return movie.AccountNumber;
    }

    public int Delete(int id)
    {
        _context.Accounts.Remove(_context.Accounts.Find(id));
        _context.SaveChanges();

        return id;
    }

    public int Update(int id, Account account)
    {
        _context.Update(account);
        _context.SaveChanges();

        return id;
    }
}
