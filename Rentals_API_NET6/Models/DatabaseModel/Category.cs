using System.ComponentModel.DataAnnotations;

namespace Rentals_API_NET6.Models.DatabaseModel
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
