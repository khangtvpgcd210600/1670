namespace BookStoreMVC.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public float Price { get; set; }
        public DateTime Date { get; set; }
    }
}
