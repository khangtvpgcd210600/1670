using BookStoreMVC.Data;
using BookStoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

		public async Task<IActionResult> Search(string query)
		{
			var books = await _context.Book
				.Where(b => b.Name.Contains(query))
				.ToListAsync();
			if (query == null)
			{
				books = await _context.Book
				.ToListAsync();
				return View("Index", books);
			}
			return View("Index", books);
		}
		public async Task<IActionResult> Filter(int genre)
		{

			var book = await _context.Book
				.Where(a => a.GenreId == genre)
				.ToListAsync();
			if (genre == 0)
			{
				book = await _context.Book
				.ToListAsync();
				return View("Index", book);
			}
			return View("Index", book);
		}
	}
}