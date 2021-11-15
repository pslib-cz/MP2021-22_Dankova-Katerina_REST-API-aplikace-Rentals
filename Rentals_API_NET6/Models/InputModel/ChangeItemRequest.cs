namespace Rentals_API_NET6.Models.InputModel
{
    public class ChangeItemRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
    }
}
