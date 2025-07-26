using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class MatchResponse
{
    public int Id { get; set; }

    public int RequestId { get; set; }

    public int ResponderId { get; set; }

    public string? Content { get; set; }

    public string Status { get; set; } = null!;

    public virtual MatchRequest Request { get; set; } = null!;

    public virtual User Responder { get; set; } = null!;
}
