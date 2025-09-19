using Microsoft.AspNetCore.Diagnostics;
using System.ComponentModel;

namespace GlobalErrorHandlingForAPI
{

    //Represents an interface for handling exceptions in ASP.NET Core applications.
    //`IExceptionHandler` implementations are used by the exception handler middleware.
    public class ApplicationExceptionHandler : IExceptionHandler                 
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, Exception exception, CancellationToken cancellationToken)   
        {
           
             if (exception is AppIdNotFoundErrorHandling)
            {
                var response = new ErrorResponse()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ExceptionMessage = exception.Message,
                    Title = "No data found with given Id"
                };
                await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

                return true;
            }
            else if (exception is InvalidOperationException) 
            {
                var response = new ErrorResponse()
                {
                    StatusCode = StatusCodes.Status501NotImplemented,
                    ExceptionMessage = exception.Message,
                    Title = "Operation can't be performed"
                };
                await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

                return true;
            }
            return false;
        }

    }
}










/*The IExceptionHandler interface is a powerful component in ASP.NET Core that allows developers to
 * handle exceptions consistently across their applications.
 * IExceptionHandler is an interface provided by ASP.NET Core. It serves as a central location for managing known exceptions.
 * When exceptions occur in your code, you can implement custom logic to handle them using this interface.
 * Instead of scattering error-handling code throughout your application, you can consolidate it within an IExceptionHandler implementation.
 */
