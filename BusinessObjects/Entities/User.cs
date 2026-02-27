using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string PasswordHash { get; set; } = null!;

    public int RoleId { get; set; }

    public DateTime? JoinDate { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();

    public virtual Role Role { get; set; } = null!;
}
