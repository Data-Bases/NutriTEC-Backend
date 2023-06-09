﻿using System;
using System.Collections.Generic;

namespace NutriTEc_Backend.DataModel;

public partial class Nutritionist
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Lastname1 { get; set; } = null!;

    public string? Lastname2 { get; set; }

    public DateOnly Birthdate { get; set; }

    public int? Weight { get; set; }

    public int? Imc { get; set; }

    public int Nutritionistcode { get; set; }

    public string? Cardnumber { get; set; }

    public string Province { get; set; } = null!;

    public string Canton { get; set; } = null!;

    public string District { get; set; } = null!;

    public string Picture { get; set; } = null!;

    public int Adminid { get; set; }

    public int Chargetypeid { get; set; }

    public virtual Administrator Admin { get; set; } = null!;

    public virtual Chargetype Chargetype { get; set; } = null!;

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();

    public virtual ICollection<Plan> Plans { get; set; } = new List<Plan>();
}
