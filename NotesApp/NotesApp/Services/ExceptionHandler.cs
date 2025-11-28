using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Diagnostics;
using NotesApp.Exceptions;

namespace NotesApp.Extension;

public class ExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is UserNotFoundExceptionId userNotFoundException)
        {
            httpContext.Response.ContentType = MediaTypeNames.Text.Plain;
            httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await httpContext.Response.WriteAsync(userNotFoundException.Message);
            return true;
        }
        
        if (exception is UserNotFoundByLoginException userNotFoundByLoginException)
        {
            httpContext.Response.ContentType = MediaTypeNames.Text.Plain;
            httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await httpContext.Response.WriteAsync(userNotFoundByLoginException.Message);
            return true;
        }

        if (exception is NoteNotFoundException noteNotFoundException)
        {
            httpContext.Response.ContentType = MediaTypeNames.Text.Plain;
            httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await httpContext.Response.WriteAsync(noteNotFoundException.Message);
            return true;
        }
        
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await httpContext.Response.WriteAsync(string.Empty);
        return false;
    }
}