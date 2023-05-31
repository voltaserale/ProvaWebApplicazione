using Microsoft.AspNetCore.Mvc;
using ProvaWebApplicazione.Models;

namespace ProvaWebApplicazione.Controllers
{
    public class ClientiController : Controller     //richieste /Clienti/*
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("username") == null)  //se non c'è un utente loggato richiamo la pagina di login
                return RedirectToAction("Login", "Home");
            else
            {       //utente loggato => faccio vedere l'elenco dei clienti
                using (NorthwindDbContext dbContext = new NorthwindDbContext())
                {
                    List<Customer> clienti = dbContext.Customers.ToList();     //recupero l'elenco dei clienti
                    return View(clienti);      //lo passo alla vista              
                }
            }
        }
    }
}
