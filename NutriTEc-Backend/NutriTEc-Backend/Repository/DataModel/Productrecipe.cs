using System;
using System.Collections.Generic;

namespace NutriTEc_Backend.Repository.DataModel;

public partial class Productrecipe
{
    public int Id { get; set; }

    public int Productbarcode { get; set; }

    public int Recipeid { get; set; }

    public double Servings { get; set; }

    public virtual Product ProductbarcodeNavigation { get; set; } = null!;

    public virtual Recipe Recipe { get; set; } = null!;
}
