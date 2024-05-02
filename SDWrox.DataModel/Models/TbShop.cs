using System;
using System.Collections.Generic;

namespace SDWrox.DataModel.Models;

public partial class TbShop
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public string? EmailAddress { get; set; }

    public virtual ICollection<TbOrder> TbOrders { get; set; } = new List<TbOrder>();
}
