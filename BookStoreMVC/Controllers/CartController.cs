using BookStoreMVC.Data;
using BookStoreMVC.Infrastructure;
using BookStoreMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreMVC.Controllers
{
    public class CartController : Controller
    {
        public Cart? Cart { get; set; }
        private readonly BookStoreMVCContext _context;

        public CartController(BookStoreMVCContext context)
        {
            _context = context;
        }


        public IActionResult AddToCart(int id)
        {
            Book? book = _context.Book?.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(book, 1);
                HttpContext.Session.SetJson("cart", Cart);
            }
            return View("Cart", Cart);
        }

        public IActionResult UpdateCart(int id)
        {
            Book? book = _context.Book?.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.DecreaseItem(book, 1);
                HttpContext.Session.SetJson("cart", Cart);
            }
            return View("Cart", Cart);
        }


        public IActionResult RemoveCart(int id)
        {
            Book? book = _context.Book?.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart");
                Cart.RemoveLine(book);
                HttpContext.Session.SetJson("cart", Cart);
            }
            return View("Cart", Cart);
        }

        public IActionResult Checkout()
        {
            // Process the checkout and create orders and order details
            // ...

            // Clear the cart after successful checkout
            HttpContext.Session.Remove("Cart");

            return View("CheckoutConfirmation"); // You can create a view for checkout confirmation
        }



    }
}
