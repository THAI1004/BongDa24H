using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Models;

public partial class Message
{
    public int Id { get; set; }

    public int BookingId { get; set; }

    public int SenderId { get; set; }

    public int ReceiverId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime SentTime { get; set; }

    [JsonIgnore]
    public virtual Booking Booking { get; set; } = null!;

    [JsonIgnore]
    public virtual User Receiver { get; set; } = null!;

    [JsonIgnore]
    public virtual User Sender { get; set; } = null!;
}
