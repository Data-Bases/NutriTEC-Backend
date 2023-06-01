namespace NutriTEc_Backend.Dtos
{
    public class PatientRecipeDto
    {
        public int Recipeid { get; set; }

        public int Patientid { get; set; }

        public string Mealtime { get; set; } = null!;

        public DateTime Consumedate { get; set; }
    }
}
