using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Report
{
    public int Id { get; set; }

    public int ReporterId { get; set; }

    public int TargetId { get; set; }

    public string TargetType { get; set; } = null!;

    public string Reason { get; set; } = null!;

    public DateTime ReportDate { get; set; }

    public int? Status { get; set; }

    public virtual User Reporter { get; set; } = null!;
}
