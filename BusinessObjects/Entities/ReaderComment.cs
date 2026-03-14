using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class ReaderComment
{
    public int CommentId { get; set; }

    public int ReaderId { get; set; }

    public int BookId { get; set; }

    public string? Comment { get; set; }

    public DateTime? CommentDate { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Reader Reader { get; set; } = null!;
}
