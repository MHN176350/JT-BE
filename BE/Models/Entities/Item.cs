using System;
using System.Collections.Generic;

namespace BE.Models.Entities;

public partial class Item
{
    public int Id { get; set; }

    public int StorageId { get; set; }

    public long Quantity { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int ProductId { get; set; }

    public long TotalAmount { get; set; }

    public virtual ICollection<ExportItem> ExportItems { get; set; } = new List<ExportItem>();

    public virtual ICollection<ImportItem> ImportItems { get; set; } = new List<ImportItem>();

    public virtual Product Product { get; set; } = null!;

    public virtual Storage Storage { get; set; } = null!;
}
