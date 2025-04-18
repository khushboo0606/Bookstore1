using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bookstore.Data;
using System.Linq;
using System.Threading.Tasks;

[Authorize(Roles = "Admin")]
public class ReportsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ReportsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Total books sold
        var totalBooksSold = await _context.OrderItems.SumAsync(oi => oi.Quantity);

        // Orders by date (grouped)
        var ordersByDate = await _context.Orders
            .GroupBy(o => o.OrderDate.Date)
            .Select(g => new { Date = g.Key, Count = g.Count() })
            .ToListAsync();

        // Remaining stock for each book
        var stockList = await _context.Books
            .Select(b => new { b.Title, b.Quantity })
            .ToListAsync();

        ViewBag.TotalBooksSold = totalBooksSold;
        ViewBag.OrdersByDate = ordersByDate;
        ViewBag.StockList = stockList;

        return View();
    }
}
