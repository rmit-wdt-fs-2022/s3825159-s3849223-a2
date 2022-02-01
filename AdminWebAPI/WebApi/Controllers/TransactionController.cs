using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Models.Repository;
using WebApi.Models.DataManager;

namespace WebApi.Controllers;

// See here for more information:
// https://docs.microsoft.com/en-au/aspnet/core/web-api/?view=aspnetcore-6.0

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly TransactionManager _repo;

    public TransactionController(TransactionManager repo)
    {
        _repo = repo;
    }

    // GET: api/movies
    [HttpGet]
    public IEnumerable<Transaction> Get()
    {
        return _repo.GetAll();
    }


    [HttpGet("{id}")]
    public Transaction Get(int id)
    {
        return _repo.Get(id);
    }

    // POST api/movies
    [HttpPost]
    public void Post([FromBody] Transaction Transaction)
    {
        _repo.Add(Transaction);
    }

    // PUT api/movies
    [HttpPut]
    public void Put([FromBody] Transaction Transaction)
    {
        _repo.Update(Transaction.TransactionID, Transaction);
    }

    // DELETE api/movies/1
    [HttpDelete("{id}")]
    public long Delete(int id)
    {
        return _repo.Delete(id);
    }
}
