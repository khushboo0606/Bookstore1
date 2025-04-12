using Microsoft.AspNetCore.Mvc;
using Bookstore.Data;
using Bookstore.Models;
using System.Diagnostics;
using System.Linq;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var groupedBooks = _context.Books
            .GroupBy(b => b.Genre)
            .Select(g => new
            {
                Genre = g.Key,
                Books = g.ToList()
            })
            .ToList<dynamic>(); // Convert to dynamic list to match your View

        return View(groupedBooks);
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
