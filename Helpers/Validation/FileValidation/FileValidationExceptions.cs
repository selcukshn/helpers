namespace Mvc.Helpers.Validation
{
    public class InvalidFileTypeException : Exception
    {
        public InvalidFileTypeException(string message) : base(message) { }
    }
    public class InvalidFileException : Exception
    {
        public InvalidFileException(string message) : base(message) { }
    }
    public class InvalidFileSizeException : Exception
    {
        public InvalidFileSizeException(string message) : base(message) { }
    }
}