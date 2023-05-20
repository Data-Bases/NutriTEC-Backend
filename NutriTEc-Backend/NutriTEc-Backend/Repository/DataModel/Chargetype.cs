using System;
using System.Collections.Generic;

namespace NutriTEc_Backend.Repository.DataModel;

public partial class Chargetype
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Nutritionist> Nutritionists { get; set; } = new List<Nutritionist>();
}
