using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Models;

public partial class Review
{
    public int Id { get; set; }

    public int BookingId { get; set; }

    public int? PitchRating { get; set; }

    public int? OpponentRating { get; set; }

    public string? OpponentSkill { get; set; }

    public string? OpponentAttitude { get; set; }

    public string? Comment { get; set; }

    [JsonIgnore]
    public virtual Booking Booking { get; set; } = null!;
}
