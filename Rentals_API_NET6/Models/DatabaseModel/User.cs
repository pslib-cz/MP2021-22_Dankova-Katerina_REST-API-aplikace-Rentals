using System.ComponentModel.DataAnnotations;

namespace Rentals_API_NET6.Models.DatabaseModel
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string OauthId { get; set; }
        public int Trustfulness { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return $"{FirstName} {LastName}"; } }
        public ICollection<InventoryItem> Inventory { get; set; }
        public ICollection<FavouriteItem> Favourite { get; set; }
        public ICollection<CartItem> Cart { get; set; }
        public ICollection<Renting> Rentings { get; set; }
        public ICollection<RentingHistoryLog> Logs { get; set; }
    }
}
