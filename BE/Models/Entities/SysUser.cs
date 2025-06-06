using System;
using System.Collections.Generic;

namespace BE.Models.Entities;

public partial class SysUser
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime LastLogin { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Avatar { get; set; } = null!;

    public bool IsAdmin { get; set; }

    public virtual ICollection<ExportInvoice> ExportInvoices { get; set; } = new List<ExportInvoice>();

    public virtual ICollection<ImportInvoice> ImportInvoices { get; set; } = new List<ImportInvoice>();

    public virtual ICollection<StorageUser> StorageUsers { get; set; } = new List<StorageUser>();
}
