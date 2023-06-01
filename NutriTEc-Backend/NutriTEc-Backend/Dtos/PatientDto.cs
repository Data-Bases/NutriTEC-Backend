namespace NutriTEc_Backend.Dtos
{
    public class PatientDto
    {
        public int? Nutriid { get; set; }

        public string Email { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Lastname1 { get; set; } = null!;

        public string? Lastname2 { get; set; }

        public int Age { get; set; }

        public DateTime Birthdate { get; set; }

        public string Password { get; set; } = null!;

        public string Country { get; set; } = null!;

        public int? Caloriesintake { get; set; }
    }
}
