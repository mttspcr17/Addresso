namespace Addresso.UI.Domain.Common.Interfaces;

public interface IServiceQueryResultOfT<T>
{
    bool Success { get; set; }
    string? Message { get; set; }
    T? Result { get; set; }
}