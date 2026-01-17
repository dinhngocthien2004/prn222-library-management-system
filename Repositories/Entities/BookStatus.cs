using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.DAL.Entities;

public partial class BookStatus
{
    public int CopyId { get; set; }

    public int BookId { get; set; }

    public string Barcode { get; set; } = null!;

    public int? Status { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
