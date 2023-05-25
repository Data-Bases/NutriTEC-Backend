using System;
using System.Collections.Generic;

namespace NutriTEc_Backend.DataModel;

public partial class Planpatient
{
    public int Id { get; set; }

    public int Planid { get; set; }

    public int Patientid { get; set; }

    public DateOnly Initialdate { get; set; }

    public DateOnly Enddate { get; set; }

    public virtual Patient Patient { get; set; } = null!;

    public virtual Plan Plan { get; set; } = null!;
}
