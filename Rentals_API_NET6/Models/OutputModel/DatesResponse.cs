using Rentals_API_NET6.Models.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rentals_API_NET6.Models.OutputModel
{
    public class DatesResponse
    {
        public int Id { get;set;}
        public RentingState State { get;set;}
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; }
    }
}
