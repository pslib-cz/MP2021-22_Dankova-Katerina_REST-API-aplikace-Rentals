﻿namespace Rentals_API_NET6.Models.DatabaseModel
{
    public class CategoryItem
    {
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
