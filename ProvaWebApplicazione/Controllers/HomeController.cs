using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProvaWebApplicazione.Models;
using System.Diagnostics;

namespace ProvaWebApplicazione.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Informazioni()     //pagina /Home/Informazioni
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()     //pagina /Home/Login       (richiesta pagina di login)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)     //pagina /Home/Login (richiesta post quando l'utente clicca sul bottone login. In user ci sarà la username e la password)
        {
            using (NorthwindDbContext dbContext = new NorthwindDbContext())
            {
                User? loggedUser = dbContext.Users
                    .Where(u => 
                        u.Username==user.Username && 
                        u.Password==user.Password)      //cerco nel db un utente con username e password specificati
                    .FirstOrDefault();
                if (loggedUser == null)
                    return NotFound("Nome utente o password errata");
                else  //utente trovato=>lo memorizzo nella sessione
                {
                    //memorizzo il nome dell'utente all'interno della sessione
                    HttpContext.Session.SetString("username",loggedUser.Username);
                    return RedirectToAction("Index"); //vado sulla pagina principale
                }

            }
        }

        [HttpGet]
        public IActionResult Logout()     //pagina /Home/Login       (richiesta pagina di login)
        {
            HttpContext.Session.Remove("username");     //rimuovo l'utente loggato
            HttpContext.Session.Clear();

            return RedirectToAction("Login");   //visualizzo la pagina di login
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}