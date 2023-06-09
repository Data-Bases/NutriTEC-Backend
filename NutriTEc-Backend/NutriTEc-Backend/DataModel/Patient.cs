﻿using System;
using System.Collections.Generic;
using NutriTEc_Backend.DataModel;

namespace NutriTEc_Backend.DataModel;

public partial class Patient
{
    public int Id { get; set; }

    public int? Nutriid { get; set; }

    public string Email { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Lastname1 { get; set; } = null!;

    public string? Lastname2 { get; set; }

    public DateOnly Birthdate { get; set; }

    public string Password { get; set; } = null!;

    public string Country { get; set; } = null!;

    public int? Caloriesintake { get; set; }

    public virtual ICollection<Measurement> Measurements { get; set; } = new List<Measurement>();

    public virtual Nutritionist? Nutri { get; set; }

    public virtual ICollection<Patientproduct> Patientproducts { get; set; } = new List<Patientproduct>();

    public virtual ICollection<Patientrecipe> Patientrecipes { get; set; } = new List<Patientrecipe>();

    public virtual ICollection<Planpatient> Planpatients { get; set; } = new List<Planpatient>();
}
