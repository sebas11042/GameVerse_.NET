using System;
using System.Collections.Generic;

namespace GameVerse.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Rol { get; set; }

    public string? Username { get; set; }

    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();

    public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();

    public virtual ICollection<Shopping> Shoppings { get; set; } = new List<Shopping>();

    public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
}
