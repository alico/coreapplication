using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CoreApplication.Common
{
    public class CustomApiException : Exception
    {
        public CustomApiException(string message) : base(message)
        {

        }

        public CustomApiException(Exception exception) : base(exception.Message, exception)
        {

        }

        public CustomApiException(string message, Exception exception) : base(message, exception)
        {

        }

        public CustomApiException(IEnumerable<string> message) : base(string.Join(",", message.Distinct()))
        {

        }

        public CustomApiException(IEnumerable<string> message, Exception exception) : base(string.Join(",", message.Distinct()), exception)
        {

        }

        public override string ToString()
        {
            if (InnerException == null)
            {
                return base.ToString();
            }

            return string.Format(CultureInfo.InvariantCulture, "{0} [See nested exception: {1}]", base.ToString(), InnerException);
        }
    }
}
