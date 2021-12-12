namespace Rentals_API_NET6.Models.DatabaseModel
{
    public enum ItemState
    {
        Available,
        Rented,
        Unavailable,
    }
    public enum RentingState
    {
        WillStart,
        InProgress,
        Ended,
        Cancelled,
    }
}
