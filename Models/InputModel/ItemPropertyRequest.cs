using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rentals.Models.InputModel
{
    public class ItemPropertyRequest
    {
        public int Id { get; set; }
        public ICollection<int> Items { get; set; }
    }
}
