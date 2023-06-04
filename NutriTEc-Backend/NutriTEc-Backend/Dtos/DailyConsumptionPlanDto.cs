using NutriTEc_Backend.Models;

namespace NutriTEc_Backend.Dtos
{
    public class DailyConsumptionPlanDto
    {
        public string PlanName { get; set; }
        public double TotalCalories { get; set; }
        public List<MealsPerDayDto> Meals { get; set; }
    }
}
