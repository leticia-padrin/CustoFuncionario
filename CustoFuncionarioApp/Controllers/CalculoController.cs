using CustoFuncionarioApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustoFuncionarioApp.Controllers
{
    public class CalculoController : Controller
    {
        public IActionResult Relatorio(Custo custo)
        {
            return View(custo);
        }
    }
}
