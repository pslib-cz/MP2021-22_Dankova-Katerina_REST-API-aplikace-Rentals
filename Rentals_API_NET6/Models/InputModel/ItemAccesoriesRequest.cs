namespace Rentals_API_NET6.Models.InputModel
{
    public class ItemAccesoriesRequest
    {
        public int Id { get; set; }
        public ICollection<int> Items { get; set; }
    }
}
