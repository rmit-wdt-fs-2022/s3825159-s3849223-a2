using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Models.Repository;
using WebApi.Models.DataManager;

namespace WebApi.Controllers;

// See here for more information:
// https://docs.microsoft.com/en-au/aspnet/core/web-api/?view=aspnetcore-6.0

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly AccountManager _repo;

    public AccountController(AccountManager repo)
    {
        _repo = repo;
    }

    // GET: api/movies
    [HttpGet]
    public IEnumerable<Account> Get()
    {
        return _repo.GetAll();
    }


    [HttpGet("{id}")]
    public Account Get(int id)
    {
        return _repo.Get(id);
    }

    // POST api/movies
    [HttpPost]
    public void Post([FromBody] Account Account)
    {
        _repo.Add(Account);
    }

    // PUT api/movies
    [HttpPut]
    public void Put([FromBody] Account Account)
    {
        _repo.Update(Account.AccountNumber, Account);
    }

    // DELETE api/movies/1
    [HttpDelete("{id}")]
    public long Delete(int id)
    {
        return _repo.Delete(id);
    }
}
