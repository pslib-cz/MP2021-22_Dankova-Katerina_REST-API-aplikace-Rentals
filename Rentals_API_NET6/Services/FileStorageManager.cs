using Rentals_API_NET6.Context;
using Rentals_API_NET6.Models.DatabaseModel;
using tusdotnet.Interfaces;
using tusdotnet.Models;
using tusdotnet.Models.Configuration;

namespace Rentals_API_NET6.Services
{
    public class FileStorageManager
    {
        private readonly ILogger<FileStorageManager> _logger;
        private readonly RentalsDbContext _context;
        public FileStorageManager(ILogger<FileStorageManager> logger, RentalsDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task StoreTus(ITusFile file, FileCompleteContext fileCompleteContext)
        {
            _logger.Log(LogLevel.Debug, "Storing file", file.Id);
            Dictionary<string, Metadata> metadata = await file.GetMetadataAsync(fileCompleteContext.CancellationToken);
            string? filename = metadata.FirstOrDefault(m => m.Key == "filename").Value.GetString(System.Text.Encoding.UTF8);
            string? filetype = metadata.FirstOrDefault(m => m.Key == "filetype").Value.GetString(System.Text.Encoding.UTF8);
            await CreateAsync(new UploadedFile { Id = file.Id, OriginalName = filename, ContentType = filetype });
        }

        public async Task<ICollection<UploadedFile>> ListAsync()
        {
            return _context.Files.ToList();
        }

        public async Task<UploadedFile> CreateAsync(UploadedFile fileRecord)
        {
            _context.Files.Add(fileRecord);
            await _context.SaveChangesAsync();
            return fileRecord;
        }
    }
}
