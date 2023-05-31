using Microsoft.AspNetCore.Mvc;
using ProvaWebApplicazione.Models;

namespace ProvaWebApplicazione.Controllers
{
    public class OrdiniController : Controller
    {
        public IActionResult Index()    //elenco ordini
        {
            if (HttpContext.Session.GetString("username") == null)  //se non c'è un utente loggato richiamo la pagina di login
                return RedirectToAction("Login", "Home");
            else
            {       //utente loggato => faccio vedere l'elenco dei ordini
                using (NorthwindDbContext dbContext = new NorthwindDbContext())
                {
                    List<Vistaordini> ordini =
                        dbContext.Vistaordinis.OrderBy(o => o.OrderDate).ToList();

                    return View(ordini);
                }

            }
        }
    }
}
