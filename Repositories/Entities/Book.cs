using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.DAL.Entities;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string? Isbn { get; set; }

    public string? Publisher { get; set; }

    public int? PublishedYear { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime? DateAdded { get; set; }

    public virtual ICollection<BookStatus> BookStatuses { get; set; } = new List<BookStatus>();

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
