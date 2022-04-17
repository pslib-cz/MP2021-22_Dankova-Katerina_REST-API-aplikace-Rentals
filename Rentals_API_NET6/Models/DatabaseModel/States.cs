namespace Rentals_API_NET6.Models.DatabaseModel
{
    public enum ItemState
    {
        Available = 0,
        Rented = 1,
        Unavailable = 2,
    }
    public enum RentingState
    {
        WillStart = 0,
        InProgress = 1,
        Ended = 2,
        Cancelled = 3,
    }
}
