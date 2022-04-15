using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rentals_API_NET6.Models.DatabaseModel
{
    public class ItemChange
    {
        [Key]
        public int Id { get; set; }
        public ItemHistoryLog ItemHistoryLog { get; set; }
        [ForeignKey("ItemHistoryLog")]
        public int ItemHistoryLogId { get; set; }
        public Property ChangedProperty { get; set; }
        public string PreviousValue { get; set; }
        public string ChangedValue { get; set; }
        public ICollection<ItemPreChangeConnection> PreviousAccessories { get; set; }
        public ICollection<ItemChangeConnection> ChangedAccessories { get; set; }
        public enum Property
        {
            Name,
            Category,
            Description,
            Note,
            Accessories
        }
    }
}
