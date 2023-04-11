namespace Mvc.Helpers.Validation
{
    public class ValidationBuilder
    {
        public FileValidation For(IFormFile file) => new FileValidation(file);
    }
}