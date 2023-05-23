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

        [HttpGet]
        public IActionResult Create()       //gestisce richieste /Prodotti/Create (visualizza una pagina di inserimento)
        {
            return View();      //la vista si chiamerà Create
        }


        [HttpPost]
        public IActionResult Create(Product product)       //gestisce richieste /Prodotti/Create (visualizza una pagina di inserimento)
        {
            //product mi arriva valorizzato dalla vista di inserimento
            using (NorthwindDbContext dbContext = new NorthwindDbContext())
            {
                Product p=new Product();
                //copio i dati nel nuovo prodotto
                p.ProductId=product.ProductId;
                p.ProductName=product.ProductName;
                p.UnitPrice=product.UnitPrice;
                p.Discontinued = 0;
                dbContext.Products.Add(p);      //aggiungo il prodotto
                dbContext.SaveChanges();        //salvo i cambiamenti sul db
                return (RedirectToAction("Index"));     //ritorno sull'elenco dei prodotti
            }
             
        }
    }
}
