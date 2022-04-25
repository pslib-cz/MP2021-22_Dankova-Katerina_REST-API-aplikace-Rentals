using Rentals_API_NET6.Models.DatabaseModel;

namespace Rentals_API_NET6.Models.OutputModel
{
    public class RentingDetail
    {
        public Renting Renting { get; set; }
        public List<Item> Items { get; set; }
    }
}
