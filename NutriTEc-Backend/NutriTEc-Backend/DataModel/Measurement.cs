using System;
using System.Collections.Generic;

namespace NutriTEc_Backend.DataModel;

public partial class Measurement
{
    public int Id { get; set; }

    public int Patientid { get; set; }

    public double Height { get; set; }

    public double Fatpercentage { get; set; }

    public double Musclepercentage { get; set; }

    public double Weight { get; set; }

    public double Waist { get; set; }

    public double Neck { get; set; }

    public double Hips { get; set; }

    public DateOnly Revisiondate { get; set; }

    public virtual Patient Patient { get; set; } = null!;
}
