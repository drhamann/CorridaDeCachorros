using Microsoft.AspNetCore.Mvc;

namespace CorridaDeCachorros.Mvc.Controllers
{
    public class CorridaDeCachorrosController : Controller
    {
        public CorridaDeCachorro CorridaDeCachorro { get; set; }
        public static string SumarioCorrida { get; set; } = string.Empty;

        private static Task corridaEmExecucao { get; set; }

        public CorridaDeCachorrosController(
            CorridaDeCachorro corridaDeCachorro)
        {
            CorridaDeCachorro = corridaDeCachorro;
        }

        public IActionResult Index()
        {
            ViewData["SumarioCorrida"] = SumarioCorrida;
            var corridaIniciou = corridaEmExecucao != null ? true : false;
            ViewData["CorridaEmExecucao"] = corridaIniciou;
            return View(CorridaDeCachorro);
        }

        [HttpPost]
        public IActionResult Aposta(
            [Bind("nomeCorredor,nomeApostador,valorAposta")]
            string nomeCorredor, string nomeApostador, double valorAposta)
        {
            // Use the idDoCorredor parameter in your logic
            CorridaDeCachorro.Apostar(nomeApostador, nomeCorredor, valorAposta);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CorrerAsync()
        {
            corridaEmExecucao = Task.Run(async () =>
            {
                if (SumarioCorrida.Length != 0)
                {
                    CorridaDeCachorro.ResetarCorrida();
                }
                SumarioCorrida = await CorridaDeCachorro.Correr();
                Task.Run(() =>
                {
                    Thread.Sleep(200);
                    corridaEmExecucao = null;
                });
            });

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult NovaCorrida()
        {
            if (SumarioCorrida.Length != 0)
            {
                CorridaDeCachorro.ResetarCorrida();
                SumarioCorrida = string.Empty;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AdicionarCachorro([Bind("nome_cachorro")] string nome_cachorro)
        {
            CorridaDeCachorro.AdicionarCorredor(nome_cachorro);

            return RedirectToAction("Index");
        }
    }
}
