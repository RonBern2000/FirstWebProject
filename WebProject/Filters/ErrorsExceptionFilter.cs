using Microsoft.AspNetCore.Mvc.Filters;

namespace WebProject.Filters
{
    public class ErrorsExceptionFilter: ExceptionFilterAttribute
    {
        private readonly ILogger<ErrorsExceptionFilter> _logger;
        public ErrorsExceptionFilter(ILogger<ErrorsExceptionFilter> logger)
        {
            _logger = logger;
        }
        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception.Message, "An unhandeled exception occured");
            context.ExceptionHandled = true;
        }
    }
}
