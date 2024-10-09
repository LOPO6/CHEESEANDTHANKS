using CheeseAndThankYou.Data;
using CheeseAndThankYou.Models;
using Microsoft.AspNetCore.Mvc;

namespace CheeseAndThankYou.Controllers
{
    public class ShopController : Controller
    {
        //db cpnnection for all methods in controller
        private readonly ApplicationDbContext _context;

        //constructor w/db connection dependancy
        public ShopController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //fetch list of categories and pass to view for display
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        public IActionResult ByCategory(int id)
        {

            //make sure we have a valid category id
            
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            var products = _context.Products.Where(p=>p.CategoryId==id).ToList();

            //fetch list of products in selected category & pass to view
            return View(products);

        }
        

    }
}
