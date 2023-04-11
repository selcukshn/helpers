using Mvc.Helpers.FileOperations;

namespace Mvc.Helpers.Validation
{
    public class FileValidation
    {
        private IFormFile File;
        private Dictionary<FileType, string[]> validExtensions = new Dictionary<FileType, string[]>(){
            { FileType.Image , new string[]{ ".jpg", ".jpeg", ".png", ".gif", ".bmp" } },
            { FileType.PDF, new string[]{ ".pdf" } },
            { FileType.Word, new string[]{ ".doc", ".docx" } },
            { FileType.Excel, new string[]{ ".xls", ".xlsx" } }
        };
        public FileValidation(IFormFile file)
        {
            this.File = file;
        }
        public FileValidation FileIsValid()
        {
            if (this.File == null || this.File.Length == 0)
                throw new InvalidFileException("Dosya boş veya hatalı");
            return this;
        }
        public FileValidation ExtensionIsValid(FileType fileType)
        {
            if (!validExtensions[fileType].Contains(Path.GetExtension(this.File.FileName)))
                throw new InvalidFileTypeException("Dosya uzantısı geçersiz");
            return this;
        }
        public FileValidation MaxSizeIsValid(long size)
        {
            if (this.File.Length >= size)
                throw new InvalidFileSizeException("Dosya boyutu izin verilenin üzerinde");
            return this;
        }
    }
}