using BookStoreMVC.Data;
using BookStoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookStoreMVC.Controllers
{
    public class HomeController : Controller
    {
		private readonly BookStoreMVCContext _context;

		public HomeController(BookStoreMVCContext context)
		{
			_context = context;
		}

		public IActionResult Index()
        {
            return View(_context.Book.ToList());
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