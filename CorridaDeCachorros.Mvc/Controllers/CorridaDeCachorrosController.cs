using Microsoft.AspNetCore.Mvc;

namespace CorridaDeCachorros.Mvc.Controllers
{
    public class CorridaDeCachorrosController : Controller
    {
        public CorridaDeCachorro CorridaDeCachorro { get; set; }
        public static string SumarioCorrida { get; set; } = string.Empty;
        public CorridaDeCachorrosController(
            CorridaDeCachorro corridaDeCachorro)
        {
            CorridaDeCachorro = corridaDeCachorro;
        }

        public IActionResult Index()
        {
            ViewData["SumarioCorrida"] = SumarioCorrida;
            return View(CorridaDeCachorro);
        }

        [HttpPost]
        public IActionResult Aposta(
            [Bind("nomeCorredor,nomeApostador,valorAposta")]
            string nomeCorredor, string nomeApostador, double valorAposta)
        {
            // Use the idDoCorredor parameter in your logic
            CorridaDeCachorro.Apostar(nomeApostador,nomeCorredor,valorAposta);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CorrerAsync()
        {
            SumarioCorrida = await CorridaDeCachorro.Correr();
            return RedirectToAction("Index");
        }
    }
}
