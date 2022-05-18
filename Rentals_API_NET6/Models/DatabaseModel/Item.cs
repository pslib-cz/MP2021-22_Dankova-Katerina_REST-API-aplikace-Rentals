using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rentals_API_NET6.Models.DatabaseModel
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public UploadedFile ImgFile { get; set; }
        [ForeignKey("ImgFile")]
        public string? Img { get; set; }
        public bool IsDeleted { get; set; }
        public ItemState State { get; set; }
        public ICollection<RentingItem> Rentings { get; set; }
        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<FavouriteItem> Favourites { get; set; }
        public ICollection<AccessoryItem> Accessories { get; set; }
        public ICollection<CartItem> Carts { get; set; }
        public ICollection<ItemHistoryLog> Logs { get; set; }

        public ICollection<ItemPreChangeConnection> PreChangeAccessory { get; set; }
        public ICollection<ItemChangeConnection> ChangeAccessory { get; set; }
    }
}
