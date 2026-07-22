namespace StudentManagementAPI.Middleware
{
    // Thrown when a requested resource (e.g. a student by id) does not exist.
    // Caught by ExceptionMiddleware and mapped to HTTP 404.
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }

    // Thrown for invalid business input that isn't already caught by model validation.
    // Caught by ExceptionMiddleware and mapped to HTTP 400.
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }

    // Thrown for auth failures (bad credentials, duplicate username, etc).
    // Caught by ExceptionMiddleware and mapped to HTTP 401.
    public class UnauthorizedAppException : Exception
    {
        public UnauthorizedAppException(string message) : base(message) { }
    }
}
