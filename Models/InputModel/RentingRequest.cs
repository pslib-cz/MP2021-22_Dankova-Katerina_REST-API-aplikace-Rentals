using Rentals.Models.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rentals.Models.InputModel
{
    public class RentingRequest
    {
        public string Owner { get; set; }
        public string Approver { get; set; }
        public ICollection<int> Items { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Note { get; set; }
    }
}
