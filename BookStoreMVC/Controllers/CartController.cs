using BookStoreMVC.Data;
using BookStoreMVC.Infrastructure;
using BookStoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        public IActionResult ViewCart()
        {
            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            return View("Cart", Cart);
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
            if (User.Identity.IsAuthenticated) // Check if the user is authenticated
            {
                // Get the current user's ID (you might have a different way to get this)
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                Cart = HttpContext.Session.GetJson<Cart>("cart");
                if (Cart != null && Cart.Lines.Any())
                {
                    // Create a new Order
                    var order = new Order
                    {
                        UserId = userId,
                        Price = (float)Cart.ComputeTotalValue(),
                        Date = DateTime.Now
                    };

                    _context.Order?.Add(order);
                    _context.SaveChanges();

                    // Create OrderDetail records for each item in the cart
                    foreach (var line in Cart.Lines)
                    {
                        var orderDetail = new OrderDetail
                        {
                            BookId = line.Book.Id,
                            OrderId = order.Id,
                            Quantity = line.Quantity
                        };

                        _context.OrderDetail?.Add(orderDetail);
                    }

                    _context.SaveChanges();

                    // Clear the cart after successful checkout
                    HttpContext.Session.Remove("Cart");

                    return View("Cart", Cart); // You can create a view for checkout confirmation
                }
                else
                {
                    return View("Cart", Cart);
                }
            }
            else
            {
                return RedirectToAction("Login", "Users"); // Redirect to login page if the user is not authenticated
            }
        }




    }
}
