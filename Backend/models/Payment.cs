using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Models;

public partial class Payment
{
    public int Id { get; set; }

    public int BookingId { get; set; }

    public int Amount { get; set; }

    public DateTime PaymentTime { get; set; }

    public int? Type { get; set; }

    public int? Method { get; set; }

    [JsonIgnore]
    public virtual Booking Booking { get; set; } = null!;
}
