namespace Rentals_API_NET6.Models.InputModel
{
    public class ChangeRentingRequest
    {
        public int Id { get; set; }
        public ICollection<int> ReturnedItems { get; set; }
    }
}
