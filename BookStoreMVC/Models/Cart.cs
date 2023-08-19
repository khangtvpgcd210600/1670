namespace BookStoreMVC.Models
{
    public class CartItem
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public Book Book { get; set; }
    }

    public class Cart
    {
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();

        public double Price { get; set; }

        public void AddCartItem(CartItem cartItem)
        {
            // Check if the same book is already in the cart
            var existingCartItem = CartItems.FirstOrDefault(item => item.BookId == cartItem.BookId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += cartItem.Quantity;
            }
            else
            {
                CartItems.Add(cartItem);
            }
        }
    }
}