using System;
using System.Collections.Generic;

namespace GameVerse.Models;

public partial class Game
{
    public int IdGame { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public string? Name { get; set; }

    public int? PriceBuy { get; set; }

    public string? Title { get; set; }

    public string? Url { get; set; }

    public int? PriceRental { get; set; }

    public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new List<PurchaseDetail>();

    public virtual ICollection<RentalDetail> RentalDetails { get; set; } = new List<RentalDetail>();

    public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();

    public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();

    public virtual ICollection<Category> IdCategories { get; set; } = new List<Category>();
}
