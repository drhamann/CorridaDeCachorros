using Microsoft.AspNetCore.Mvc;

namespace CorridaDeCachorros.Mvc.Controllers
{
    public class CorridaDeCachorrosController : Controller
    {
        public CorridaDeCachorro CorridaDeCachorro { get; set; }

        public CorridaDeCachorrosController(
            CorridaDeCachorro corridaDeCachorro)
        {
            CorridaDeCachorro = corridaDeCachorro;
        }

        public IActionResult Index()
        {
            return View(CorridaDeCachorro);
        }
    }
}
