namespace TaskManager.Domain.Exceptions;

/// <summary>
/// Represents a critical exception that indicates a severe or unexpected error in the application.
/// This exception is used to distinguish critical errors that may require immediate attention or special handling.
/// </summary>
/// <param name="message">The error message that explains the reason for the exception.</param>
public class CriticalException(string message) : Exception(message);