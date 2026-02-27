using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class Loan
{
    public int LoanId { get; set; }

    public int UserId { get; set; }

    public int CopyId { get; set; }

    public DateTime? LoanDate { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public int? Status { get; set; }

    public virtual BookCopy Copy { get; set; } = null!;

    public virtual Fine? Fine { get; set; }

    public virtual User User { get; set; } = null!;
}
