using System;
using System.Collections.Generic;

namespace NutriTEc_Backend.DataModel;

public partial class Recipe
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Patientrecipe> Patientrecipes { get; set; } = new List<Patientrecipe>();

    public virtual ICollection<Planrecipe> Planrecipes { get; set; } = new List<Planrecipe>();

    public virtual ICollection<Productrecipe> Productrecipes { get; set; } = new List<Productrecipe>();
}
