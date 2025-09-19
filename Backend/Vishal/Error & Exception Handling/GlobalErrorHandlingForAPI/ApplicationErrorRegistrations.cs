using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GlobalErrorHandlingForAPI
{
    public class ApplicationErrorRegistrations
    {    
    }

    public class AppIdNotFoundErrorHandling : SystemException
    {
       
        public AppIdNotFoundErrorHandling(string message)
        {
            
        }
    }

    public class InvalidOperationException : SystemException
    {
        public InvalidOperationException(string message)
        {
            
        }
    }


}
