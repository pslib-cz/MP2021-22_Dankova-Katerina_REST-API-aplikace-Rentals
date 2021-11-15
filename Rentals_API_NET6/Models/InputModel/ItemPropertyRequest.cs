namespace Rentals_API_NET6.Models.InputModel
{
    public class ItemPropertyRequest
    {
        public int Id { get; set; }
        public ICollection<int> Categories { get; set; }
    }
}
