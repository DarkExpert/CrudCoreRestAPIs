using System;
using System.Collections.Generic;

namespace SDWrox.DataModel.Models;

public partial class TbOrder
{
    public int Id { get; set; }

    public int ShopId { get; set; }

    public string Number { get; set; } = null!;

    public DateTime Date { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public string Owner { get; set; } = null!;

    public virtual TbShop Shop { get; set; } = null!;

    public virtual ICollection<TbOrderDetail> TbOrderDetails { get; set; } = new List<TbOrderDetail>();
}
