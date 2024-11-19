using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shared.Exceptions;

namespace Shared.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case DatabaseException:
                case InvalidHttpResponseException:
                case InvalidResponseException:
                    context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                    break;

                case NotFoundException:
                    context.Result = new NotFoundResult();
                    break;
            }
        }
    }
}