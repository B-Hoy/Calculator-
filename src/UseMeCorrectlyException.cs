
namespace Calculator_.src
{
    [Serializable]
    internal class UseMeCorrectlyException : Exception
    {
        public UseMeCorrectlyException()
        {

        }

        public UseMeCorrectlyException(string? message) : base(message)
        {
        }

        public UseMeCorrectlyException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}