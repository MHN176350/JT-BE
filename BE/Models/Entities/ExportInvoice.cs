using System;
using System.Collections.Generic;

namespace BE.Models.Entities;

public partial class ExportInvoice
{
    public int Id { get; set; }

    public double Discount { get; set; }

    public double Total { get; set; }

    public int CustId { get; set; }

    public int StorageId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual SysUser? CreatedByNavigation { get; set; }

    public virtual Customer Cust { get; set; } = null!;

    public virtual ICollection<ExportItem> ExportItems { get; set; } = new List<ExportItem>();

    public virtual Storage Storage { get; set; } = null!;
}
