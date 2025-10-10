using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Models;

public partial class MatchResponse
{
    public int Id { get; set; }

    public int RequestId { get; set; }

    public int ResponderId { get; set; }

    public string? Content { get; set; }

    public int? Status { get; set; }

    [JsonIgnore]
    public virtual MatchRequest Request { get; set; } = null!;

    public virtual User Responder { get; set; } = null!;
}
