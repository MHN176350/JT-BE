using System;
using System.Collections.Generic;

namespace BE.Models.Entities;

public partial class Supplier
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string Address { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public virtual ICollection<ImportInvoice> ImportInvoices { get; set; } = new List<ImportInvoice>();
}
