using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryFundAppASP.Data;
using LibraryFundAppASP.Models;

namespace LibraryFundAppASP.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.Books.Include(b => b.IdAuthorNavigation);
            return View(await libraryContext.ToListAsync());
        }

        public IActionResult Create()
        {
            var authors = _context.Authors.ToList();
            if (!authors.Any())
            {
                ViewData["ErrorMessage"] = "Нет доступных авторов. Пожалуйста, добавьте автора перед созданием книги.";
            }
            ViewBag.Authors = authors;
            return View(new Book());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                ViewData["ValidationErrors"] = string.Join("; ", errors);
                ViewBag.Authors = _context.Authors.ToList();
                return View(book);
            }

            try
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                ViewData["ErrorMessage"] = $"Ошибка при сохранении книги: {ex.InnerException?.Message ?? ex.Message}";
                ViewBag.Authors = _context.Authors.ToList();
                return View(book);
            }
        }

        public IActionResult Edit(int id)
        {
            var book = _context.Books.Include(b => b.IdAuthorNavigation).FirstOrDefault(b => b.IdBook == id);
            if (book == null) return NotFound();
            ViewBag.Authors = _context.Authors.ToList();
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (id != book.IdBook) return NotFound();
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                ViewData["ValidationErrors"] = string.Join("; ", errors);
                ViewBag.Authors = _context.Authors.ToList();
                return View(book);
            }

            try
            {
                _context.Update(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                ViewData["ErrorMessage"] = $"Ошибка при обновлении книги: {ex.InnerException?.Message ?? ex.Message}";
                ViewBag.Authors = _context.Authors.ToList();
                return View(book);
            }
        }

        public IActionResult Details(int id)
        {
            var book = _context.Books.Include(b => b.IdAuthorNavigation).FirstOrDefault(b => b.IdBook == id);
            if (book == null) return NotFound();
            return View(book);
        }

        public IActionResult Delete(int id)
        {
            var book = _context.Books.Include(b => b.IdAuthorNavigation).FirstOrDefault(b => b.IdBook == id);
            if (book == null) return NotFound();
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                try
                {
                    _context.Books.Remove(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    ViewData["ErrorMessage"] = $"Ошибка при удалении книги: {ex.InnerException?.Message ?? ex.Message}";
                    return View(book);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.IdBook == id);
        }
    }
}
