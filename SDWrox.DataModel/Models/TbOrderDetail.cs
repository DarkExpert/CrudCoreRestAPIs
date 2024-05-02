using System;
using System.Collections.Generic;

namespace SDWrox.DataModel.Models;

public partial class TbOrderDetail
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public double Count { get; set; }

    public decimal Price { get; set; }

    public decimal? Discount { get; set; }

    public virtual TbOrder Order { get; set; } = null!;

    public virtual TbProduct Product { get; set; } = null!;
}
