using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rentals.Models.OutputModel
{
    public class DatesResponse
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Owner { get; set; }
    }
}
