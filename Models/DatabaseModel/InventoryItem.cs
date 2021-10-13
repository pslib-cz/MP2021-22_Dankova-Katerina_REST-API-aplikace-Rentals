using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rentals.Models.DatabaseModel
{
    public class InventoryItem
    {
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
