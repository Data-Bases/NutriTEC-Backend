using System;
using System.Collections.Generic;

namespace NutriTEc_Backend.Repository.DataModel;

public partial class Productvitamin
{
    public int Id { get; set; }

    public int Productbarcode { get; set; }

    public int Vitaminid { get; set; }

    public virtual Product ProductbarcodeNavigation { get; set; } = null!;

    public virtual Vitamin Vitamin { get; set; } = null!;
}
