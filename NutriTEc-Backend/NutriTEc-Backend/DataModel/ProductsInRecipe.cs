using System;
using System.Collections.Generic;

namespace NutriTEc_Backend.DataModel;

public partial class ProductsInRecipe
{
    public string? Recipename { get; set; }

    public int? Recipeid { get; set; }

    public string? Productname { get; set; }

    public double? Portionsize { get; set; }

    public double? Servings { get; set; }

    public double? Energy { get; set; }

    public double? Fat { get; set; }

    public double? Sodium { get; set; }

    public double? Carbs { get; set; }

    public double? Protein { get; set; }

    public double? Calcium { get; set; }

    public double? Iron { get; set; }
}
