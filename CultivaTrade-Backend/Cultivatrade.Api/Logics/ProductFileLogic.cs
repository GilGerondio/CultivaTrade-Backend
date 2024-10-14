using Cultivatrade.Api.DatabaseConnection;
using Cultivatrade.Api.DTO;
using Cultivatrade.Api.Models;
using FileUpload.Api.Logics;
using System.IO;

namespace Cultivatrade.Api.Logics
{
    public class ProductFileLogic
    {
        private readonly CultivatradeContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly CheckFileType _checkFileType;
        public ProductFileLogic(CultivatradeContext context, IWebHostEnvironment webHostEnvironment, CheckFileType checkFileType)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _checkFileType = checkFileType;
        }

        // GET PRODUCT FILE BY PRODUCT ID
        public DisplayFile GetProductFileById(Guid productId)
        {
            var data = _context.ProductFiles.FirstOrDefault(x => x.ProductId == productId);

            var displayFile = new DisplayFile();

            var fileStream = new FileStream(data.ImagePath, FileMode.Open, FileAccess.Read);
            displayFile.FileStream = fileStream;
            var getFileExtension = Path.GetExtension(data.ImagePath);
            var getContentType = _checkFileType.GetContentType(getFileExtension);
            displayFile.ContentType = getContentType;
            displayFile.FileName = data.Image;

            return displayFile;
        }

        // ADD PRODUCT FILE
        public bool AddProductFile(ProductFileDTO_POST dto)
        {
            int success = 0;
            foreach(var file in dto.Files)
            {
                Guid fileId = Guid.NewGuid();
                string fileName = fileId + Path.GetExtension(file.FileName);
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images/Products");
                string filePath = Path.Combine(path, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                var data = new ProductFile
                {
                    ProductFileId = Guid.NewGuid(),
                    ProductId = dto.ProductId,
                    Image = fileName,
                    ImagePath = filePath,
                    DateTimeCreated = DateTime.UtcNow
                };

                _context.ProductFiles.Add(data);
            }
            success = _context.SaveChanges();
            if (success > 0)
            {
                return true;
            }
            return false;
        }

        
    }
}
