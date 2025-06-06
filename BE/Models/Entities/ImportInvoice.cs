using System;
using System.Collections.Generic;

namespace BE.Models.Entities;

public partial class ImportInvoice
{
    public int Id { get; set; }

    public int SupplierId { get; set; }

    public double Total { get; set; }

    public int StorageId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual SysUser? CreatedByNavigation { get; set; }

    public virtual ICollection<ImportItem> ImportItems { get; set; } = new List<ImportItem>();

    public virtual Storage Storage { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;
}
