using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class Fine
{
    public int FineId { get; set; }

    public int LoanId { get; set; }

    public decimal Amount { get; set; }

    public DateTime? FineDate { get; set; }

    public bool? IsPaid { get; set; }

    public virtual Loan Loan { get; set; } = null!;
}
