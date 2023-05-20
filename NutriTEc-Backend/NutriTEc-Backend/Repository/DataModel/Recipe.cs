using System;
using System.Collections.Generic;

namespace NutriTEc_Backend.Repository.DataModel;

public partial class Recipe
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Productrecipe> Productrecipes { get; set; } = new List<Productrecipe>();
}
