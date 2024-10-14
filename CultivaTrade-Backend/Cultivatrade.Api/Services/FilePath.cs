namespace Cultivatrade.Api.Services
{
    public class FilePath
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FilePath(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string BusinessPermitFolderPath(string fileName)
        {
            return $"{_webHostEnvironment.WebRootPath}/Images/BusinessPermits/${fileName}";
        }  

        public string ProductFolderPath(string fileName)
        {
            return $"{_webHostEnvironment.WebRootPath}/Images/Products/${fileName}";
        }  
        
        public string ProfileFolderPath(string fileName)
        {
            return $"{_webHostEnvironment.WebRootPath}/Images/Profiles/${fileName}";
        }

        public string SanitaryPermitFolderPath(string fileName)
        {
            return $"{_webHostEnvironment.WebRootPath}/Images/SanitaryPermits/${fileName}";
        }
    }
}
