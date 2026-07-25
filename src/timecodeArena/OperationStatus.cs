using System;

namespace timecodeArena;

/// <summary>
/// Represents the result of an operation, encapsulating the success state, error messages, and any underlying exceptions.
/// </summary>
public class OperationStatus
{
    public Exception? Exception { get; }
    public bool IsSuccess { get; }
    public string? Cause { get; }
    public string? Resolution { get; }
    
    public bool HasException => Exception != null;

    // Changed from private to protected so OperationStatus<T> can inherit it
    protected OperationStatus(bool isSuccess, string? cause, string? resolution, Exception? exception)
    {
        IsSuccess = isSuccess;
        Cause = cause;
        Resolution = resolution;
        Exception = exception;
    }

    public static OperationStatus ValidResult()
        => new OperationStatus(true, null, null, null);

    public static OperationStatus InvalidResult(string cause, string? resolution = null)
        => new OperationStatus(false, cause, resolution, null);

    public static OperationStatus InvalidResult(Exception exception, string? resolution = null)
        => new OperationStatus(false, null, resolution, exception);

    public static OperationStatus InvalidResult(Exception exception, string cause, string? resolution = null)
        => new OperationStatus(false, cause, resolution, exception);

    public static implicit operator bool(OperationStatus opStat)
        => opStat.IsSuccess;
}