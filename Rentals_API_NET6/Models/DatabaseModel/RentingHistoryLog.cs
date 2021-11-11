using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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
        public int UserId { get; set; }
        public DateTime ChangedTime { get; set; }
        public Action Action { get; set; }
    }

    public enum Action
    {
        Rented,
        PickedUpItems,
        Change,
        Delete,
        Cancel
    }
}
