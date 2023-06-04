using System;
using System.Collections.Generic;

namespace NutriTEc_Backend.DataModel;

public partial class Planrecipe
{
    public int Id { get; set; }

    public int Recipeid { get; set; }

    public int Planid { get; set; }

    public double Servings { get; set; }

    public string Mealtime { get; set; } = null!;

    public string Consumeweekday { get; set; } = null!;

    public virtual Plan Plan { get; set; } = null!;

    public virtual Recipe Recipe { get; set; } = null!;
}
