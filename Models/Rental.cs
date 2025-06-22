using System;
using System.Collections.Generic;

namespace GameVerse.Models;

public partial class Rental
{
    public int IdRent { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int? RentDays { get; set; }

    public int? Total { get; set; }

    public int? IdUser { get; set; }

    public bool? Status { get; set; }

    public virtual User? IdUserNavigation { get; set; }

    public virtual ICollection<RentalDetail> RentalDetails { get; set; } = new List<RentalDetail>();
}
