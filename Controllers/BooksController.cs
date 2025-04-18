using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bookstore.Data;
using Bookstore.Models;
using System.Linq;
using System.Threading.Tasks;

public class BooksController : Controller
{
    private readonly ApplicationDbContext _context;

    public BooksController(ApplicationDbContext context)
    {
        _context = context;
    }

    [AllowAnonymous] // Anyone can view books
    public async Task<IActionResult> Index(string search, string genre, decimal? minPrice, decimal? maxPrice)
    {
        var booksQuery = _context.Books.Include(b => b.Author).AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
           booksQuery = booksQuery.Where(b => b.Title != null && b.Title.Contains(search));


        if (!string.IsNullOrWhiteSpace(genre))
            booksQuery = booksQuery.Where(b => b.Genre == genre);

        if (minPrice.HasValue)
            booksQuery = booksQuery.Where(b => b.Price >= minPrice.Value);

        if (maxPrice.HasValue)
            booksQuery = booksQuery.Where(b => b.Price <= maxPrice.Value);

        var genres = await _context.Books
            .Where(b => !string.IsNullOrEmpty(b.Genre))
            .Select(b => b.Genre)
            .Distinct()
            .ToListAsync();

        ViewBag.Genres = new SelectList(genres);

        var books = await booksQuery.ToListAsync();
        return View(books);
    }

    [AllowAnonymous] // Anyone can view book details
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var book = await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(m => m.BookId == id);
        if (book == null) return NotFound();

        return View(book);
    }

    [Authorize(Roles = "Admin")] // Only Admins can create books
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Book book, string AuthorName)
    {
        if (ModelState.IsValid)
        {
            var existingAuthor = await _context.Authors.FirstOrDefaultAsync(a => a.Name == AuthorName);
            if (existingAuthor == null)
            {
                existingAuthor = new Author { Name = AuthorName };
                _context.Authors.Add(existingAuthor);
                await _context.SaveChangesAsync();
            }

            book.AuthorId = existingAuthor.AuthorId;
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(book);
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var book = await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.BookId == id);
        if (book == null) return NotFound();

        return View(book);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Book book, string AuthorName)
    {
        if (id != book.BookId) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                var existingAuthor = await _context.Authors.FirstOrDefaultAsync(a => a.Name == AuthorName);
                if (existingAuthor == null)
                {
                    existingAuthor = new Author { Name = AuthorName };
                    _context.Authors.Add(existingAuthor);
                    await _context.SaveChangesAsync();
                }

                book.AuthorId = existingAuthor.AuthorId;
                _context.Update(book);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Books.Any(e => e.BookId == book.BookId))
                    return NotFound();
                else
                    throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(book);
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var book = await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(m => m.BookId == id);
        if (book == null) return NotFound();

        return View(book);
    }

    [HttpPost, ActionName("Delete")]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book != null)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
