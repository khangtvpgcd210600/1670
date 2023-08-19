using BookStoreMVC.Data;
using BookStoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BookStoreMVC.Controllers
{
	public class BooksController : Controller
	{
		private readonly BookStoreMVCContext _context;

		public BooksController(BookStoreMVCContext context)
		{
			_context = context;
		}

		// GET: Books
		public async Task<IActionResult> Index()
		{
			return _context.Book != null ?
						View(await _context.Book.ToListAsync()) :
						Problem("Entity set 'BookStoreMVCContext.Book'  is null.");
		}

		// GET: Books/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Book == null)
			{
				return NotFound();
			}

			var book = await _context.Book
				.FirstOrDefaultAsync(m => m.Id == id);
			if (book == null)
			{
				return NotFound();
			}

			return View(book);
		}

		// GET: Books/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Books/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(IFormFile file, [Bind("Id,Name,Price,Description,AuthorId,GenreId")] Book book)
		{
			if (file != null)
			{
				string filename = file.FileName;
				//  string  ext = Path.GetExtension(file.FileName);
				string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images"));
				using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
				{ await file.CopyToAsync(filestream); }

				book.Image = filename;
			}


			_context.Add(book);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));

		}

		// GET: Books/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Book == null)
			{
				return NotFound();
			}

			var book = await _context.Book.FindAsync(id);
			if (book == null)
			{
				return NotFound();
			}
			return View(book);
		}

		// POST: Books/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(IFormFile file, int id, [Bind("Id,Name,Price,Description,AuthorId,GenreId,Image")] Book book)
		{
			if (file != null)
			{
				string filename = file.FileName;
				//  string  ext = Path.GetExtension(file.FileName);
				string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images"));
				using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
				{ await file.CopyToAsync(filestream); }

				book.Image = filename;
			}
			_context.Update(book);
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}


		// GET: Books/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Book == null)
			{
				return NotFound();
			}

			var book = await _context.Book
				.FirstOrDefaultAsync(m => m.Id == id);
			if (book == null)
			{
				return NotFound();
			}

			return View(book);
		}

		// POST: Books/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Book == null)
			{
				return Problem("Entity set 'BookStoreMVCContext.Book'  is null.");
			}
			var book = await _context.Book.FindAsync(id);
			if (book != null)
			{
				_context.Book.Remove(book);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool BookExists(int id)
		{
			return (_context.Book?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
