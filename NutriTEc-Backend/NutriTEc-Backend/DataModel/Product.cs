using System;
using System.Collections.Generic;

namespace NutriTEc_Backend.DataModel;

public partial class Product
{
    public int Barcode { get; set; }

    public string Name { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public double Portionsize { get; set; }

    public double Energy { get; set; }

    public double Fat { get; set; }

    public double Sodium { get; set; }

    public double Carbs { get; set; }

    public double Protein { get; set; }

    public double Calcium { get; set; }

    public double Iron { get; set; }

    public bool? Isapproved { get; set; }

    public virtual ICollection<Patientproduct> Patientproducts { get; set; } = new List<Patientproduct>();

    public virtual ICollection<Planproduct> Planproducts { get; set; } = new List<Planproduct>();

    public virtual ICollection<Productrecipe> Productrecipes { get; set; } = new List<Productrecipe>();

    public virtual ICollection<Productvitamin> Productvitamins { get; set; } = new List<Productvitamin>();
}
