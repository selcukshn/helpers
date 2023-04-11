using Mvc.Helpers.Validation;

namespace Mvc.Helpers.FileOperations
{
    public class FileOperation
    {
        private IFormFile File { get; set; }
        private FileType Type { get; set; }
        private string Extension { get; set; }
        private string FileName => this.File.FileName;
        private ValidationBuilder Validation = new ValidationBuilder();

        public FileOperation(IFormFile file, FileType type)
        {
            Validation.For(file).FileIsValid().ExtensionIsValid(type);
            this.File = file;
            this.Type = type;
            this.Extension = Path.GetExtension(this.FileName);
        }
        public FileOperation(IFormFile file, FileType type, long maxSize)
        {
            Validation.For(file).FileIsValid().ExtensionIsValid(type).MaxSizeIsValid(maxSize);
            this.File = file;
            this.Type = type;
            this.Extension = Path.GetExtension(this.FileName);
        }
        public string Upload(string? fileName = null, string path = "images")
        {
            if (string.IsNullOrEmpty(fileName))
                fileName = this.FileName + "_" + Guid.NewGuid().ToString();
            else
                fileName = fileName + "_" + Guid.NewGuid().ToString();

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", path, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                this.File.CopyTo(stream);
            }
            return fileName;
        }

    }
}