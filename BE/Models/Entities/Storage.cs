using System;
using System.Collections.Generic;

namespace BE.Models.Entities;

public partial class Storage
{
    public int Id { get; set; }

    public string Location { get; set; } = null!;

    public DateTime UpdatedDate { get; set; }

    public DateTime CreatedDate { get; set; }

    public string Code { get; set; } = null!;

    public virtual ICollection<ExportInvoice> ExportInvoices { get; set; } = new List<ExportInvoice>();

    public virtual ICollection<ImportInvoice> ImportInvoices { get; set; } = new List<ImportInvoice>();

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual ICollection<StorageUser> StorageUsers { get; set; } = new List<StorageUser>();
}
