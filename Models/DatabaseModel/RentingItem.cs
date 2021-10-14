﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rentals.Models.DatabaseModel
{
    public class RentingItem
    {
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int RentingId { get; set; }
        public Renting Renting { get; set; }
        public bool Returned { get; set; }
    }
}