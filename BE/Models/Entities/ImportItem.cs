using System;
using System.Collections.Generic;

namespace BE.Models.Entities;

public partial class ImportItem
{
    public int Id { get; set; }

    public int InvoiceId { get; set; }

    public int ItemId { get; set; }

    public double UnitPrice { get; set; }

    public long Quantity { get; set; }

    public double Total { get; set; }

    public virtual ImportInvoice Invoice { get; set; } = null!;

    public virtual Item Item { get; set; } = null!;
}
