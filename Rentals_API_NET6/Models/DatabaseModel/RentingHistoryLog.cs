using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rentals_API_NET6.Models.DatabaseModel
{
    public class RentingHistoryLog
    {
        [Key]
        public int Id { get; set; }
        public Renting Renting { get; set; }
        [ForeignKey("Renting")]
        public int RentingId { get; set; }
        public User User { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; }
        public DateTime ChangedTime { get; set; }
        public Action Action { get; set; }
        public List<Item> ReturnedItems { get; set; }
    }

    public enum Action
    {
        Created = 0,
        Activated = 1,
        Changed = 2,
        Canceled = 3,
        Returned = 4,
        Updated = 5,
    }
}
