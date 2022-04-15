namespace Rentals_API_NET6.Models.DatabaseModel
{
    public class ItemChangeConnection
    {
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int ItemChangeId { get; set; }
        public ItemChange ItemChange { get; set; }
    }
}
