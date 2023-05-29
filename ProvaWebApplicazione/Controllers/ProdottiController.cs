using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProvaWebApplicazione.Models;

namespace ProvaWebApplicazione.Controllers
{
    public class ProdottiController : Controller
    {
        public IActionResult Index(int? idCategoria)  //gestirà richieste /Prodotti o /Prodotti/Index o /Prodotti?idCategoria=*
        {
            using (NorthwindDbContext dbContext = new NorthwindDbContext())
            {
                List<Product> prodotti;
                if (idCategoria == null)    //non è stata specificata la categoria => visualizzo tutti i prodotti             
                    prodotti = dbContext.Products
                            .Include(p => p.Category)
                            .ToList();     //recupero l'elenco dei prodotti  
                else                                  //è stata specificata la categoria => visualizzo tutti i prodotti di quella categoria
                    prodotti = dbContext.Products
                            .Where(p => p.CategoryId == idCategoria)
                            .Include(p => p.Category)
                            .ToList();     //recupero l'elenco dei prodotti                   

                return View(prodotti);      //lo passo alla vista
            }
        }

        [HttpGet]
        public IActionResult Create()       //gestisce richieste /Prodotti/Create (visualizza una pagina di inserimento)
        {
            using (NorthwindDbContext dbContext = new NorthwindDbContext())
            {
                List<Category> categories =
                    dbContext.Categories
                    .OrderBy(c => c.CategoryName)
                    .ToList();
                ViewData["elencoCategorie"] = categories;   //passo l'elenco delle categorie alla vista "nuovo ordine"
                return View();      //la vista si chiamerà Create
            }
        }


        [HttpPost]
        public IActionResult Create(Product product)       //gestisce richieste /Prodotti/Create (visualizza una pagina di inserimento)
        {
            //product mi arriva valorizzato dalla vista di inserimento
            using (NorthwindDbContext dbContext = new NorthwindDbContext())
            {
                Product p = new Product();
                //copio i dati nel nuovo prodotto
                p.ProductId = product.ProductId;
                p.ProductName = product.ProductName;
                p.UnitPrice = product.UnitPrice;
                p.CategoryId = product.CategoryId;
                p.Discontinued = 0;
                dbContext.Products.Add(p);      //aggiungo il prodotto
                dbContext.SaveChanges();        //salvo i cambiamenti sul db
                return (RedirectToAction("Index"));     //ritorno sull'elenco dei prodotti
            }

        }


        [HttpGet]
        public IActionResult Update(int id)       //gestisce richieste /Prodotti/Update (visualizza una pagina di modifica, con i valori del prodotto impostati)
        {
            using (NorthwindDbContext dbContext = new NorthwindDbContext())
            {
                List<Category> categories =
                    dbContext.Categories
                    .OrderBy(c => c.CategoryName)
                    .ToList();

                Product? product =
                    dbContext.Products
                        .Where(p => p.ProductId == id)
                        .FirstOrDefault();  //restituisce il prodotto corrispondente all'id passato come parametro o un valore null se l'id non esiste
                if (product == null)
                    return NotFound("Prodotto non trovato");
                else
                {
                    ViewData["elencoCategorie"] = categories;   //passo l'elenco delle categorie alla vista "nuovo ordine"
                    return View(product);      //la vista si chiamerà Update e riceve in ingresso il prodotto da modificare
                }
                
            }
        }


        [HttpPost]
        public IActionResult Update(Product product)       //gestisce richieste /Prodotti/Create (visualizza una pagina di inserimento)
        {
            //product mi arriva valorizzato dalla vista di inserimento
            using (NorthwindDbContext dbContext = new NorthwindDbContext())
            {
                Product? productToEdit =
                    dbContext.Products
                        .Where(p => p.ProductId == product.ProductId)
                        .FirstOrDefault();  //restituisce il prodotto corrispondente all'id passato come parametro o un valore null se l'id non esiste
                if (productToEdit == null)
                    return NotFound("Prodotto non trovato");
                else
                {
                    //copio i dati nel prodotto da editare

                    productToEdit.ProductName = product.ProductName;
                    productToEdit.UnitPrice = product.UnitPrice;
                    productToEdit.CategoryId = product.CategoryId;                   
                    
                    dbContext.SaveChanges();        //salvo i cambiamenti sul db
                    return (RedirectToAction("Index"));     //ritorno sull'elenco dei prodotti
                }
                
            }

        }


        public IActionResult Delete(int id)       //gestisce richieste /Prodotti/Delete (elimina un prodotto)
        {
            using (NorthwindDbContext dbContext = new NorthwindDbContext())
            {
                Product? product=dbContext.Products
                        .FirstOrDefault(p  => p.ProductId == id);
                if (product == null)
                    return NotFound("Prodotto non trovato");
                else
                {
                    dbContext.Products.Remove(product);
                    dbContext.SaveChanges();
                    return (RedirectToAction("Index"));
                }
               
            }
        }
    }
}
