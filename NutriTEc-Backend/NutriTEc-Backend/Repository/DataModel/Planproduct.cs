using System;
using System.Collections.Generic;

namespace NutriTEc_Backend.Repository.DataModel;

public partial class Planproduct
{
    public int Id { get; set; }

    public int Productbarcode { get; set; }

    public int Planid { get; set; }

    public double Servings { get; set; }

    public string Mealtime { get; set; } = null!;

    public string Consumeweekday { get; set; } = null!;

    public virtual Plan Plan { get; set; } = null!;

    public virtual Product ProductbarcodeNavigation { get; set; } = null!;
}
