using Rentals_API_NET6.Models.DatabaseModel;

namespace Rentals_API_NET6.Models.OutputModel
{
    public class ItemHistory
    {
        public ItemHistoryLog ItemHistoryLog { get; set; }
        public List<Item> PreviousAccessories { get; set; }
        public List<Item> ChangedAccessories { get; set; }
    }
}