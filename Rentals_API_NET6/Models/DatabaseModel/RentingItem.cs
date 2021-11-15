namespace Rentals_API_NET6.Models.DatabaseModel
{
    public class RentingItem
    {
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int RentingId { get; set; }
        public Renting Renting { get; set; }
        public bool Returned { get; set; }
    }
}
