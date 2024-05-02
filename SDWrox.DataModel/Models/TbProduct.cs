using System;
using System.Collections.Generic;

namespace SDWrox.DataModel.Models;

public partial class TbProduct
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<TbOrderDetail> TbOrderDetails { get; set; } = new List<TbOrderDetail>();
}
