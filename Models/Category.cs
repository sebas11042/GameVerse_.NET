using System;
using System.Collections.Generic;

namespace GameVerse.Models;

public partial class Category
{
    public int IdCategory { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Game> IdGames { get; set; } = new List<Game>();
}
