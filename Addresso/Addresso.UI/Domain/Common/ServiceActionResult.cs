using System.Net;

namespace Addresso.UI.Domain.Common;

public record ServiceActionResult (
    bool Success,
    HttpStatusCode StatusCode,
    string? Message
);