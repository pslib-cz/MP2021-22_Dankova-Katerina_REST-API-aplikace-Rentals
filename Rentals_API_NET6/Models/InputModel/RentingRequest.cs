namespace Rentals_API_NET6.Models.InputModel
{
    public class RentingRequest
    {
        public ICollection<int> Items { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Note { get; set; }
    }
}
