using Microsoft.AspNetCore.Mvc;
using ProvaWebApplicazione.Models;

namespace ProvaWebApplicazione.Controllers
{
    public class CategorieController : Controller
    {
        public IActionResult Index()
        {
            using (NorthwindDbContext dbContext = new NorthwindDbContext())
            {
                List<Category> categories = dbContext.Categories.ToList();     //recupero l'elenco delle categorie
                return View(categories);      //lo passo alla vista              
            }
        }
    }
}
