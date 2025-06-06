using System;
using System.Collections.Generic;

namespace BE.Models.Entities;

public partial class Customer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public long Points { get; set; }

    public string Address { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<ExportInvoice> ExportInvoices { get; set; } = new List<ExportInvoice>();
}
