

namespace CoreApplication.Common
{
    public class ForbiddenException : CustomApiException
    {
        public ForbiddenException(string message = "Authorization denied!") : base(message)
        {

        }

    }
}
