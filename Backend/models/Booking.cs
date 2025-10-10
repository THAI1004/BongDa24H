using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Models;

public partial class Booking
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int PitchId { get; set; }

    public DateOnly BookingDate { get; set; }

    public string TimeSlot { get; set; } = null!;

    public int? DepositAmount { get; set; }

    public bool IsDeposited { get; set; }

    public int? Status { get; set; }

    [JsonIgnore]
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    [JsonIgnore]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    [JsonIgnore]
    public virtual Pitch Pitch { get; set; } = null!;

    [JsonIgnore]
    public virtual Review? Review { get; set; }

    [JsonIgnore]
    public virtual User User { get; set; } = null!;
}
