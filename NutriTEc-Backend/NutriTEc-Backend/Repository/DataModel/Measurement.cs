using System;
using System.Collections.Generic;

namespace NutriTEc_Backend.Repository.DataModel;

public partial class Measurement
{
    public int Id { get; set; }

    public int Patientid { get; set; }

    public int Height { get; set; }

    public int Fatpercentage { get; set; }

    public int Musclepercentage { get; set; }

    public int Weight { get; set; }

    public int Waist { get; set; }

    public int Neck { get; set; }

    public int Hips { get; set; }

    public DateOnly Revisiondate { get; set; }

    public virtual Patient Patient { get; set; } = null!;
}
