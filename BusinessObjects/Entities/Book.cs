using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string? Isbn { get; set; }
    
    public string? Publisher { get; set; }

    public int? CategoryId { get; set; }

    public int? PublishedYear { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime? DateAdded { get; set; }
    public int BorrowCount { get; set; }

    public virtual ICollection<BookCopy> BookCopies { get; set; } = new List<BookCopy>();

    public virtual Category? Category { get; set; }

    public virtual ICollection<ReaderComment> ReaderComments { get; set; } = new List<ReaderComment>();

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
