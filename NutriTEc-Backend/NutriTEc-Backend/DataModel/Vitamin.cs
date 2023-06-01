using System;
using System.Collections.Generic;

namespace NutriTEc_Backend.DataModel;

public partial class Vitamin
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public double Amount { get; set; }

    public virtual ICollection<Productvitamin> Productvitamins { get; set; } = new List<Productvitamin>();
}
