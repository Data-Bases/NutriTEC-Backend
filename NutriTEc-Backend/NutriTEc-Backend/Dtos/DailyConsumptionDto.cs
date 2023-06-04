using NutriTEc_Backend.Models;

namespace NutriTEc_Backend.Dtos
{
    public class DailyConsumptionDto
    {
        public DateTime Date { get; set; }
        public double TotalCalories { get; set; }
        public List<MealtimeDto> Meals { get; set; }
    }
}
