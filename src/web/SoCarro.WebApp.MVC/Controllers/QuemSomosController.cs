using Microsoft.AspNetCore.Mvc;

namespace SoCarro.WebApp.MVC.Controllers;

public class QuemSomosController : Controller
{
    [Route("quemsomos")]
    public IActionResult Index()
    {
        return View();
    }
}
