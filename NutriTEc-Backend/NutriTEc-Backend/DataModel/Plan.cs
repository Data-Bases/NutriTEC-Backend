using System;
using System.Collections.Generic;

namespace NutriTEc_Backend.DataModel;

public partial class Plan
{
    public int Id { get; set; }

    public int Nutriid { get; set; }

    public string Name { get; set; } = null!;

    public virtual Nutritionist Nutri { get; set; } = null!;

    public virtual ICollection<Planpatient> Planpatients { get; set; } = new List<Planpatient>();

    public virtual ICollection<Planproduct> Planproducts { get; set; } = new List<Planproduct>();

    public virtual ICollection<Planrecipe> Planrecipes { get; set; } = new List<Planrecipe>();
}
