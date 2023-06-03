using System;
using System.Collections.Generic;

namespace NutriTEc_Backend.DataModel;

public partial class Patientproduct
{
    public int Id { get; set; }

    public int Productbarcode { get; set; }

    public int Patientid { get; set; }

    public string Mealtime { get; set; } = null!;

    public DateOnly Consumedate { get; set; }

    public double Servings { get; set; }

    public virtual Patient Patient { get; set; } = null!;

    public virtual Product ProductbarcodeNavigation { get; set; } = null!;
}
