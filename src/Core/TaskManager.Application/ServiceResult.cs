using System.Net;
using System.Text.Json.Serialization;

namespace TaskManager.Application;

public class ServiceResult<T>
{
    /// <summary>
    ///     Gets or sets the data returned by the service operation.
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    ///     Gets or sets the list of error messages in case of a failure.
    /// </summary>
    public List<string>? ErrorMessage { get; set; }

    /// <summary>
    ///     Indicates whether the service operation was successful.
    /// </summary>
    [JsonIgnore]
    public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0;

    /// <summary>
    ///     Indicates whether the service operation failed.
    /// </summary>
    [JsonIgnore]
    public bool IsFailure => !IsSuccess;

    /// <summary>
    ///     Gets or sets the HTTP status code associated with the service result.
    /// </summary>
    [JsonIgnore]
    public HttpStatusCode StatusCode { get; set; }

    /// <summary>
    ///     Gets or sets the URL of the created resource in case of a "Created" response.
    /// </summary>
    [JsonIgnore]
    public string? UrlAsCreated { get; set; }

    /// <summary>
    ///     Creates a successful service result with the specified data and status code.
    /// </summary>
    /// <param name="data">The data to include in the result.</param>
    /// <param name="statusCode">The HTTP status code (default is <see cref="HttpStatusCode.OK" />).</param>
    /// <returns>A <see cref="ServiceResult{T}" /> representing a successful operation.</returns>
    public static ServiceResult<T> Success(T data, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new ServiceResult<T>
        {
            Data = data,
            StatusCode = statusCode
        };
    }

    /// <summary>
    ///     Creates a successful service result for a "Created" response with the specified data and URL.
    /// </summary>
    /// <param name="data">The data to include in the result.</param>
    /// <param name="urlAsCreated">The URL of the created resource.</param>
    /// <returns>A <see cref="ServiceResult{T}" /> representing a successful creation operation.</returns>
    public static ServiceResult<T> SuccessAsCreated(T data, string urlAsCreated)
    {
        return new ServiceResult<T>
        {
            Data = data,
            StatusCode = HttpStatusCode.Created,
            UrlAsCreated = urlAsCreated
        };
    }

    /// <summary>
    ///     Creates a failed service result with the specified error messages and status code.
    /// </summary>
    /// <param name="errorMessage">The list of error messages.</param>
    /// <param name="statusCode">The HTTP status code (default is <see cref="HttpStatusCode.BadRequest" />).</param>
    /// <returns>A <see cref="ServiceResult{T}" /> representing a failed operation.</returns>
    public static ServiceResult<T> Failure(List<string> errorMessage,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ServiceResult<T>
        {
            ErrorMessage = errorMessage,
            StatusCode = statusCode
        };
    }

    /// <summary>
    ///     Creates a failed service result with a single error message and status code.
    /// </summary>
    /// <param name="errorMessage">The error message.</param>
    /// <param name="statusCode">The HTTP status code (default is <see cref="HttpStatusCode.BadRequest" />).</param>
    /// <returns>A <see cref="ServiceResult{T}" /> representing a failed operation.</returns>
    public static ServiceResult<T> Failure(string errorMessage, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ServiceResult<T>
        {
            ErrorMessage = [errorMessage],
            StatusCode = statusCode
        };
    }
}

/// <summary>
///     Represents a non-generic service result that encapsulates the outcome of a service operation.
///     This class is used for operations that do not return any data.
/// </summary>
public class ServiceResult
{
    /// <summary>
    ///     Gets or sets the list of error messages in case of a failure.
    /// </summary>
    public List<string>? ErrorMessage { get; set; }

    /// <summary>
    ///     Indicates whether the service operation was successful.
    /// </summary>
    [JsonIgnore]
    public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0;

    /// <summary>
    ///     Indicates whether the service operation failed.
    /// </summary>
    [JsonIgnore]
    public bool IsFailure => !IsSuccess;

    /// <summary>
    ///     Gets or sets the HTTP status code associated with the service result.
    /// </summary>
    [JsonIgnore]
    public HttpStatusCode StatusCode { get; set; }

    /// <summary>
    ///     Creates a successful service result with the specified status code.
    /// </summary>
    /// <param name="statusCode">The HTTP status code (default is <see cref="HttpStatusCode.OK" />).</param>
    /// <returns>A <see cref="ServiceResult" /> representing a successful operation.</returns>
    public static ServiceResult Success(HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new ServiceResult
        {
            StatusCode = statusCode
        };
    }

    /// <summary>
    ///     Creates a failed service result with the specified error messages and status code.
    /// </summary>
    /// <param name="errorMessage">The list of error messages.</param>
    /// <param name="statusCode">The HTTP status code (default is <see cref="HttpStatusCode.BadRequest" />).</param>
    /// <returns>A <see cref="ServiceResult" /> representing a failed operation.</returns>
    public static ServiceResult Failure(List<string> errorMessage,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ServiceResult
        {
            ErrorMessage = errorMessage,
            StatusCode = statusCode
        };
    }

    /// <summary>
    ///     Creates a failed service result with a single error message and status code.
    /// </summary>
    /// <param name="errorMessage">The error message.</param>
    /// <param name="statusCode">The HTTP status code (default is <see cref="HttpStatusCode.BadRequest" />).</param>
    /// <returns>A <see cref="ServiceResult" /> representing a failed operation.</returns>
    public static ServiceResult Failure(string errorMessage, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ServiceResult
        {
            ErrorMessage = [errorMessage],
            StatusCode = statusCode
        };
    }
}