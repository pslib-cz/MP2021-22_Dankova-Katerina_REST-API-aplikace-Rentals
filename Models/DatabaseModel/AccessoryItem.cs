﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rentals.Models.DatabaseModel
{
    public class AccessoryItem
    {
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int AccessoryId { get; set; }
        public Item Accessory { get; set; }
    }
}
