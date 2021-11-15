using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rentals_API_NET6.Models.DatabaseModel
{
    public class Renting
    {
        [Key]
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Note { get; set; }
        public User Owner { get; set; }
        [ForeignKey("Owner")]
        public int OwnerId { get; set; }
        public User Approver { get; set; }
        [ForeignKey("Approver")]
        public int? ApproverId { get; set; }
        public ICollection<RentingItem> Items { get; set; }
        public RentingState State { get; set; }
        public ICollection<RentingHistoryLog> Logs { get; set; }
    }
}
