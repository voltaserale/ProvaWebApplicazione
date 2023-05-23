using Microsoft.AspNetCore.Mvc;
using ProvaWebApplicazione.Models;

namespace ProvaWebApplicazione.Controllers
{
    public class OrdiniController : Controller
    {
        public IActionResult Index()    //elenco ordini
        {
            using (NorthwindDbContext dbContext=new NorthwindDbContext())
            {
                List<Vistaordini> ordini=
                    dbContext.Vistaordinis.OrderBy(o => o.OrderDate).ToList();

                return View(ordini);
            }     
                
        }
    }
}
