using System;
using System.Collections.Generic;

namespace GameVerse.Models;

public partial class Shopping
{
    public int IdBuy { get; set; }

    public DateTime? BuyDate { get; set; }

    public int? Total { get; set; }

    public int? IdUser { get; set; }

    public virtual User? IdUserNavigation { get; set; }

    public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new List<PurchaseDetail>();
}
