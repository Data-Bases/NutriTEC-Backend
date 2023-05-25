using System;
using System.Collections.Generic;

namespace NutriTEc_Backend.DataModel;

public partial class Administrator
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Nutritionist> Nutritionists { get; set; } = new List<Nutritionist>();
}
