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
        public ICollection<ItemChange> ItemChanges { get; set; }

        public enum ItemAction
        {
            Created = 0,
            Changed = 1,
            ChangedAccessories = 2,
            Deleted = 3,
            Restored = 4,
            AddedToInventory = 5,
            DeletedFromInventory = 6,
        }
    }
}
