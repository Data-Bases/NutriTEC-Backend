using System;
using System.Collections.Generic;

namespace NutriTEc_Backend.Repository.DataModel;

public partial class Plan
{
    public int Id { get; set; }

    public int Nutriid { get; set; }

    public string Name { get; set; } = null!;

    public virtual Nutritionist Nutri { get; set; } = null!;

    public virtual ICollection<Planpatient> Planpatients { get; set; } = new List<Planpatient>();

    public virtual ICollection<Planproduct> Planproducts { get; set; } = new List<Planproduct>();
}
