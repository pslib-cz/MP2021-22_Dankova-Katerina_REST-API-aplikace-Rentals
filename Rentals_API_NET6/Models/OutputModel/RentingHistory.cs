using Rentals_API_NET6.Models.DatabaseModel;

namespace Rentals_API_NET6.Models.OutputModel
{
    public class RentingHistory
    {
        public int Id { get; set; }
        public Renting Renting { get; set; }
        public int RentingId { get; set; }
        public User User { get; set; }
        public int? UserId { get; set; }
        public DateTime ChangedTime { get; set; }
        public DatabaseModel.Action Action { get; set; }
        public List<Item> ReturnedItems { get; set; }
    }
}
