﻿namespace Rentals_API_NET6.Models.DatabaseModel
{
    public class AccessoryItem
    {
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int AccessoryId { get; set; }
        public Item Accessory { get; set; }
    }
}
