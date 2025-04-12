using Microsoft.AspNetCore.Mvc;
using Bookstore.Models;

namespace Bookstore.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Submit(Contact model)
        {
            if (ModelState.IsValid)
            {
                // You can add email sending logic here
                TempData["Success"] = "Your message has been sent successfully!";
                return RedirectToAction("Index");
            }
            return View("Index", model);
        }
    }
}
