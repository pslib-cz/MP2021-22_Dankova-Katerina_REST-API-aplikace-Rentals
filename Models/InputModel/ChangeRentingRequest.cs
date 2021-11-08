using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rentals.Models.InputModel
{
    public class ChangeRentingRequest
    {
        public int Id { get; set; }
        public ICollection<int> ReturnedItems { get; set; }
        public string UserId { get; set; }
    }
}
