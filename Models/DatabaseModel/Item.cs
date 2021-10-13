using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Rentals.Models.DatabaseModel
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public string Img { get; set; }
        public bool IsDeleted { get; set; }
        public ItemState State { get; set; }
        public ICollection<RentingItem> Rentings { get; set; }
        public ICollection<CategoryItem> Categories { get; set; }
        public ICollection<InventoryItem> Inventories { get; set; }
        public ICollection<FavouriteItem> Favourites { get; set; }
        public ICollection<Item> AccessoryFor { get; set; }
        public ICollection<CartItem> Carts { get; set; }
    }
}
