using Demo.LifeTime.Models;
using Demo.LifeTime.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Demo.LifeTime.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISingleton singleton;
        private readonly IScoped scoped;
        private readonly ITransient transient;
        private readonly POCService service;

        public HomeController(ISingleton singleton, IScoped scoped, ITransient transient, POCService service)
        {
            this.singleton = singleton;
            this.scoped = scoped;
            this.transient = transient;
            this.service = service;
        }

        public IActionResult Index()
        {
            ViewBag.Singleton = singleton.Id.ToString();
            ViewBag.Scoped = scoped.Id.ToString();
            ViewBag.Transient = transient.Id.ToString();

            ViewBag.ServiceSingleton = service.Singleton.Id.ToString();
            ViewBag.ServiceScoped = service.Scoped.Id.ToString();
            ViewBag.ServiceTransient = service.Transient.Id.ToString();


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
