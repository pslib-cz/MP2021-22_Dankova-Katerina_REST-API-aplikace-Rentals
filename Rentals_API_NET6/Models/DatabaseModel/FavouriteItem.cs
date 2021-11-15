namespace Rentals_API_NET6.Models.DatabaseModel
{
    public class FavouriteItem
    {
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
