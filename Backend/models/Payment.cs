using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Payment
{
    public int Id { get; set; }

    public int BookingId { get; set; }

    public int Amount { get; set; }

    public string Method { get; set; } = null!;

    public string Type { get; set; } = null!;

    public DateTime PaymentTime { get; set; }

    public virtual Booking Booking { get; set; } = null!;
}
