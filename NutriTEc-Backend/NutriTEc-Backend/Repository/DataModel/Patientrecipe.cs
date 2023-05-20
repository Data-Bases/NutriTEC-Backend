﻿using System;
using System.Collections.Generic;

namespace NutriTEc_Backend.Repository.DataModel;

public partial class Patientrecipe
{
    public int Id { get; set; }

    public int Recipeid { get; set; }

    public int Patientid { get; set; }

    public string Mealtime { get; set; } = null!;

    public DateOnly Consumedate { get; set; }

    public virtual Patient Patient { get; set; } = null!;

    public virtual Patient Recipe { get; set; } = null!;
}
