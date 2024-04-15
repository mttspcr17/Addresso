using System.Net;

namespace Addresso.Shared.Domain.Common;

public record ServiceActionResult (
    bool Success,
    HttpStatusCode StatusCode,
    string? Message
);