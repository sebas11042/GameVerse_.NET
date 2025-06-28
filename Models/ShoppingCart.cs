using System;
using System.Collections.Generic;

namespace GameVerse.Models;

public partial class ShoppingCart
{
    public int IdUser { get; set; }

    public int IdGame { get; set; }

    public int? Amount { get; set; }

    public int? Price { get; set; }

    public DateTime? AddDate { get; set; }

    public virtual Game IdGameNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
