using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProvaWebApplicazione.Models;

namespace ProvaWebApplicazione.Controllers
{
    public class ProdottiController : Controller
    {
        public IActionResult Index(int? idCategoria)  //gestirà richieste /Prodotti o /Prodotti/Index o /Prodotti?idCategoria=*
        {            
            using(NorthwindDbContext dbContext=new NorthwindDbContext())
            {
                List<Product> prodotti;
                if (idCategoria == null)    //non è stata specificata la categoria => visualizzo tutti i prodotti             
                    prodotti = dbContext.Products
                            .Include(p => p.Category)
                            .ToList();     //recupero l'elenco dei prodotti  
                 else                                  //è stata specificata la categoria => visualizzo tutti i prodotti di quella categoria
                    prodotti = dbContext.Products
                            .Where(p => p.CategoryId== idCategoria)
                            .Include(p => p.Category)
                            .ToList();     //recupero l'elenco dei prodotti                   

                return View(prodotti);      //lo passo alla vista
            }           
        }
    }
}
