using BookStoreMVC.Data;
using BookStoreMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreMVC.Controllers
{
    public class CartController : Controller
    {
        private readonly BookStoreMVCContext _context;

        public CartController(BookStoreMVCContext context)
        {
            _context = context;
        }

        private Cart GetCart()
        {
            var cart = HttpContext.Session.Get<Cart>("Cart") ?? new Cart();
            HttpContext.Session.Set("Cart", cart);
            return cart;
        }

        [HttpGet]
        public IActionResult Cart()
        {
            var cart = GetCart();

            var totalPrice = cart.CartItems.Sum(item => item.Quantity * item.Book.Price);

            var cartViewModel = new Cart
            {
                CartItems = cart.CartItems,
                Price = totalPrice
            };

            return View(cartViewModel);
        }

        [HttpGet]
        public IActionResult AddToCart(int id)
        {
            var book = _context.Book.SingleOrDefault(s => s.Id == id);
            if (book != null)
            {
                var cart = GetCart();
                cart.AddCartItem(new CartItem { Book = book, Quantity = 1 }); // Use the AddCartItem method
            }
            return RedirectToAction("Cart");
        }


        [HttpPost]
        public IActionResult UpdateCart(int id, int quantity)
        {
            var cart = GetCart();
            var cartItem = cart.CartItems.FirstOrDefault(item => item.BookId == id);
            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                HttpContext.Session.Set("Cart", cart);
            }

            return RedirectToAction("Cart");
        }


        public IActionResult RemoveCart(int id)
        {
            var cart = HttpContext.Session.Get<Cart>("Cart");
            if (cart != null)
            {
                var cartItem = cart.CartItems.FirstOrDefault(item => item.BookId == id);
                if (cartItem != null)
                {
                    cart.CartItems.Remove(cartItem);
                    HttpContext.Session.Set("Cart", cart);
                }
            }

            return RedirectToAction("Cart");
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
