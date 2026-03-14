using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class Reader
{
    public int ReaderId { get; set; }

    public int UserId { get; set; }

    public string CardNumber { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }

    public string? Address { get; set; }

    public DateTime? MembershipDate { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<ReaderComment> ReaderComments { get; set; } = new List<ReaderComment>();

    public virtual User User { get; set; } = null!;
}
