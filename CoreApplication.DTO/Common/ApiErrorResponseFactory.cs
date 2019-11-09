using CoreApplication.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApplication.DTO
{
    public class ApiErrorResponseFactory
    {
        public static ApiErrorResponse Create(Exception ex)
        {
            if (ex.GetType() == typeof(ItemNotFoundException))
                return new ApiErrorResponse(404, ex.Message);

            else if (ex.GetType() == typeof(ForbiddenException))
                return new ApiErrorResponse(403, ex.Message);

            else if (ex.GetType() == typeof(CustomApiException))
                return new ApiErrorResponse(400, ex.Message);

            else
                return new ApiErrorResponse(500, string.Empty);
        }
    }
}
