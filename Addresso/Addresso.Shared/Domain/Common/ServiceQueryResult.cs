﻿using System.Net;
using Addresso.Shared.Domain.Common.Interfaces;

namespace Addresso.Shared.Domain.Common;

public class ServiceQueryResult<T>(bool success, HttpStatusCode statusCode, string? message, T result)
    : IServiceQueryResultOfT<T>
    where T : class
{
    public bool Success { get; set; } = success;
    public HttpStatusCode StatusCode { get; set; } = statusCode;
    public string? Message { get; set; } = message;
    public T? Result { get; set; } = result;
}