using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Message
{
    public int Id { get; set; }

    public int BookingId { get; set; }

    public int SenderId { get; set; }

    public int ReceiverId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime SentTime { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual User Receiver { get; set; } = null!;

    public virtual User Sender { get; set; } = null!;
}
