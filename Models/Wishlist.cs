using System;
using System.Collections.Generic;

namespace GameVerse.Models;

public partial class Wishlist
{
    public int IdUser { get; set; }

    public int IdGame { get; set; }

    public DateOnly? AddedAt { get; set; }

    public virtual Game IdGameNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
