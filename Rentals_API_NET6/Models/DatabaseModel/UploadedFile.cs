using System.ComponentModel.DataAnnotations;

namespace Rentals_API_NET6.Models.DatabaseModel
{
    public class UploadedFile
    {
        [Key]
        public string Id { get; set; }
        public string OriginalName { get; set; } = "";
        public string ContentType { get; set; } = "";
        public DateTime Uploaded { get; set; }
        public long Size { get; set; }
    }
}
