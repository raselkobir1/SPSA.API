using SPSA.API.Helper.Resources;

namespace SPSA.API.Helper.FileHelper
{
    public static class FileHelper
    {
        private static IWebHostEnvironment _hostEnvironment;
        //private static string bucketPrefix = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("awsS3")["BucketNamePrefix"];
        public static void Initialize(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }
        public static async Task<(bool, string)> UploadFile(IFormFile file, string destinationFolder)
        {
            try
            {
                if (file == null || file.Length <= 0)
                    return (false, CommonMessage.InvalidFile);

                string projectLocation = _hostEnvironment.WebRootPath;
                string fullPath = Path.Combine(projectLocation, destinationFolder.TrimStart('\\'));
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                fullPath = Path.Combine(fullPath, fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

                using var fileStream = new FileStream(fullPath, FileMode.Create);
                await file.CopyToAsync(fileStream);

                return (true, fullPath);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
