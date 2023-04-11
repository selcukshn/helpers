using System.Text;
using Mvc.Helpers.Validation;

namespace Mvc.Helpers.FileOperations
{
    public class FileOperation
    {
        private IFormFile File { get; set; }
        private FileType Type { get; set; }
        private string Extension { get; set; }
        private string FileName => this.File.FileName;
        private string? UploadedFileName { get; set; }
        private ValidationBuilder Validation = new ValidationBuilder();

        public FileOperation(IFormFile file, FileType type)
        {
            Validation.For(file).FileIsValid().ExtensionIsValid(type);
            SetProps(file, type, Path.GetExtension(this.FileName));
        }
        public FileOperation(IFormFile file, FileType type, long maxSize)
        {
            Validation.For(file).FileIsValid().ExtensionIsValid(type).MaxSizeIsValid(maxSize);
            SetProps(file, type, Path.GetExtension(this.FileName));
        }
        private void SetProps(IFormFile file, FileType type, string extension)
        {
            this.File = file;
            this.Type = type;
            this.Extension = extension;
        }
        public FileOperation Upload(string? fileName = null, string path = "images")
        {
            var sb = new StringBuilder("_" + Guid.NewGuid().ToString());
            if (string.IsNullOrEmpty(fileName))
                sb.Insert(0, this.FileName);
            else
                sb.Insert(0, fileName);
            this.UploadedFileName = sb.ToString();
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", path, this.UploadedFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                this.File.CopyTo(stream);
            }
            return this;
        }
        public override string ToString() => this.UploadedFileName;

    }
}