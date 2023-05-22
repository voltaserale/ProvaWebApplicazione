using Microsoft.AspNetCore.Mvc;
using ProvaWebApplicazione.Models;

namespace ProvaWebApplicazione.Controllers
{
    public class ClientiController : Controller     //richieste /Clienti/*
    {
        public IActionResult Index()
        {
            using (NorthwindDbContext dbContext=new NorthwindDbContext()) {
                List<Customer> clienti = dbContext.Customers.ToList();     //recupero l'elenco dei clienti
                return View(clienti);      //lo passo alla vista              
            }            
        }
    }
}
