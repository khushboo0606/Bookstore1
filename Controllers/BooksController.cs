using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bookstore.Data;
using Bookstore.Models;
using System.Threading.Tasks;
using System.Linq;

public class BooksController : Controller
{
    private readonly ApplicationDbContext _context;

    public BooksController(ApplicationDbContext context)
    {
        _context = context;
    }

    [AllowAnonymous] // Anyone can view books
    public async Task<IActionResult> Index()
    {
        var books = await _context.Books.Include(b => b.Author).ToListAsync();
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

    [Authorize(Roles = "Admin")] // Only Admins can edit books
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

    [Authorize(Roles = "Admin")] // Only Admins can delete books
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
