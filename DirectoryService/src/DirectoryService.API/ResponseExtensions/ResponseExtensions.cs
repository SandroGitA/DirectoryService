using Microsoft.AspNetCore.Mvc;
using Shared;

namespace DirectoryService.API.ResponseExtensions
{
    //public static class ResponseExtensions
    //{
    //    public static ActionResult Response(this Errors errors)
    //    {
    //        if (!errors.Any())
    //        {
    //            return new ObjectResult(null)
    //            {
    //                StatusCode = StatusCodes.Status500InternalServerError
    //            };
    //        }

    //        var statusTypes = errors
    //            .Select(x => x.TypeError)
    //            .Distinct()
    //            .ToList();

    //        int statusCode = statusTypes.Count > 1
    //            ? StatusCodes.Status500InternalServerError
    //            : GetStatusCodeFromErrorType(statusTypes.First());

    //        return new ObjectResult(errors)
    //        {
    //            StatusCode = statusCode,
    //        };
    //    }

    //    private static int GetStatusCodeFromErrorType(TypeError typeError)
    //    {
    //        return typeError switch
    //        {
    //            TypeError.NOT_FOUND => StatusCodes.Status400BadRequest,
    //            TypeError.VALIDATION => StatusCodes.Status405MethodNotAllowed,
    //            TypeError.FAILURE => StatusCodes.Status500InternalServerError,
    //            _ => StatusCodes.Status500InternalServerError,
    //        };
    //    }
    //}
}
