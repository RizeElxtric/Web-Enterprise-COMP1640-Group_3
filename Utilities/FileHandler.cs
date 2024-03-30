using System.Net.Http.Headers;

namespace MarketingEvent.Api.Utilities
{
    public class FileHandler
    {
        public async Task<string> SaveFileAsync(IFormFile file, string path)
        {
            try
            {
                var fileGuid = Guid.NewGuid();
                Directory.CreateDirectory(path);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    fileName = $"{fileGuid}-{fileName}";
                    var fullPath = Path.Combine(path, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    return fullPath;
                }
                throw new Exception("Empty file");
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
