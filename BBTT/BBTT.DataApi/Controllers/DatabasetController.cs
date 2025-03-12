using Microsoft.AspNetCore.Mvc;

namespace BBTT.DataApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DatabasetController : ControllerBase
{
    public Service service;


    [ HttpGet]
    public async Task Get ()
    {
        //DbContextPostgres
    }
}
