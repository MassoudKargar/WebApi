namespace Api.Common.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string message) : base(message, ApiResultStatusCode.NotFound)
        {
        }
    }
}
