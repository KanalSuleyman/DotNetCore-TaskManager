using System.Net;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application;

namespace TaskManager.API.Controllers;

/// <summary>
/// Provides a base controller class for customizing action results based on <see cref="ServiceResult"/> or <see cref="ServiceResult"/>.
/// This class centralizes the logic for creating consistent HTTP responses.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class CustomBaseController : ControllerBase
{
    [NonAction]
    public IActionResult CreateActionResult<T>(ServiceResult<T> result)
    {
        return result.StatusCode switch
        {
            HttpStatusCode.NoContent => NoContent(), // Returns 204 No Content.
            HttpStatusCode.Created => Created(result.UrlAsCreated, result), // Returns 201 Created with the resource location.
            _ => new ObjectResult(result) { StatusCode = result.StatusCode.GetHashCode() } // Returns a custom status code with the result.
        };
    }

    [NonAction]
    public IActionResult CreateActionResult(ServiceResult result)
    {
        return result.StatusCode switch
        {
            HttpStatusCode.NoContent => new ObjectResult(null) { StatusCode = result.StatusCode.GetHashCode() }, // Returns 204 No Content.
            _ => new ObjectResult(result) { StatusCode = result.StatusCode.GetHashCode() } // Returns a custom status code with the result.
        };
    }
}