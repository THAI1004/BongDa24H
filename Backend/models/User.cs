using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Models;

public partial class User
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public int? Role { get; set; }

    public int? AccumulatedPoints { get; set; }
    public string? Image { get; set; }
    public bool IsDeleted { get; set; } = false;


    // [JsonIgnore]
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    // [JsonIgnore]
    public virtual ICollection<MatchRequest> MatchRequests { get; set; } = new List<MatchRequest>();

    // [JsonIgnore]
    public virtual ICollection<MatchResponse> MatchResponses { get; set; } = new List<MatchResponse>();

    // [JsonIgnore]
    public virtual ICollection<Message> MessageReceivers { get; set; } = new List<Message>();

    // [JsonIgnore]
    public virtual ICollection<Message> MessageSenders { get; set; } = new List<Message>();

    // [JsonIgnore]
    public virtual ICollection<PitchCluster> PitchClusters { get; set; } = new List<PitchCluster>();

    // [JsonIgnore]
    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();


    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
}
