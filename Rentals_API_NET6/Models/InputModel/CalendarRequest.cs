namespace Rentals_API_NET6.Models.InputModel
{
    public class CalendarRequest
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public List<int> Items { get; set; }
    }
}
