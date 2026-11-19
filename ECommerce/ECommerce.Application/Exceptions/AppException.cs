using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Exceptions
{
    public abstract class AppException(string message, int statusCode,  Exception? innerException=null) : Exception(message,innerException) 
    {
        public int StatusCode { get; } = statusCode;
    }

    public class NotFoundException(string message) : AppException(message, 404);
    public class ConflictException(string message) : AppException(message, 409);
    public class ForbiddenException(string message) : AppException(message, 403);



}
