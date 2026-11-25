using Microsoft.AspNetCore.Mvc;

namespace ProjetoLivraria.Controllers
{
    public class LivroController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
