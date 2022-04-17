using System.ComponentModel.DataAnnotations;

namespace Rentals_API_NET6.Models.DatabaseModel
{
    public class UploadedFile
    {
        [Key]
        public string Id { get; set; }
        public string OriginalName { get; set; }
        public ICollection<Item> Items { get; set; }
        public string ContentType { get; set; }
    }
}
