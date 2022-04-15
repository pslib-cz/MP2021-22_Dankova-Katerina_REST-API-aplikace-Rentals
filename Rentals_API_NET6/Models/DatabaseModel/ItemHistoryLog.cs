using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rentals_API_NET6.Models.DatabaseModel
{
    public class ItemHistoryLog
    {
        [Key]
        public int Id { get; set; }
        public Item Item { get; set; }
        [ForeignKey("Item")]
        public int ItemId { get; set; }
        public User User { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public DateTime ChangedTime { get; set; }
        public ItemAction Action { get; set; }
        public User UserInventory { get; set; }
        [ForeignKey("UserInventory")]
        public int? UserInventoryId { get; set; }
        public ICollection<ItemChange> ItemChanges { get; set; }

        public enum ItemAction
        {
            Created,
            Changed,
            ChangedAccessories,
            Deleted,
            Restored,
            AddedToInventory,
            DeletedFromInventory,
        }
    }
}
