using System;
using System.Collections.Generic;

namespace GameVerse.Models;

public partial class PurchaseDetail
{
    public int IdBuy { get; set; }

    public int IdGame { get; set; }

    public int? Amount { get; set; }

    public int? Price { get; set; }

    public virtual Shopping IdBuyNavigation { get; set; } = null!;

    public virtual Game IdGameNavigation { get; set; } = null!;
}
