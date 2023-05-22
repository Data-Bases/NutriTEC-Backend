using System;
using System.Collections.Generic;

namespace NutriTEc_Backend.Repository.DataModel;

public partial class Usercredential
{
    public int Id { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string Usertype { get; set; }
}
