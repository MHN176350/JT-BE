using System;
using System.Collections.Generic;

namespace BE.Models.Entities;

public partial class ExportItem
{
    public int Id { get; set; }

    public int ItemId { get; set; }

    public long Quantity { get; set; }

    public double UnitPrice { get; set; }

    public int InvoiceId { get; set; }

    public virtual ExportInvoice Invoice { get; set; } = null!;

    public virtual Item Item { get; set; } = null!;
}
