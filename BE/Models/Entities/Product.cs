using System;
using System.Collections.Generic;

namespace BE.Models.Entities;

public partial class Product
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Image { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public string Descriptions { get; set; } = null!;

    public int CatId { get; set; }

    public virtual Category Cat { get; set; } = null!;

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
