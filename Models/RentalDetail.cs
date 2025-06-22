using System;
using System.Collections.Generic;

namespace GameVerse.Models;

public partial class RentalDetail
{
    public int IdRental { get; set; }

    public int IdGame { get; set; }

    public int? Amount { get; set; }

    public DateOnly? RentalDate { get; set; }

    public DateOnly? ExpireDate { get; set; }

    public int? Price { get; set; }

    public virtual Game IdGameNavigation { get; set; } = null!;

    public virtual Rental IdRentalNavigation { get; set; } = null!;
}
