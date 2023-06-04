using NutriTEc_Backend.Models;

namespace NutriTEc_Backend.Dtos
{
    public class MealtimeDto
    {
        public string Mealtime { get; set; }
        public double Calories { get; set; }
        public List<ConsumedByPatientDto> Products { get; set; }
        public List<ConsumedByPatientDto> Recipes { get; set; }


    }
}
