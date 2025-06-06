using System;
using System.Collections.Generic;

namespace BE.Models.Entities;

public partial class StorageUser
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int StorageId { get; set; }

    public int RoleId { get; set; }

    public virtual Storage Storage { get; set; } = null!;

    public virtual SysUser User { get; set; } = null!;
}
