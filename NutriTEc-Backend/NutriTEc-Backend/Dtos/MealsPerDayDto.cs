namespace NutriTEc_Backend.Dtos
{
    public class MealsPerDayDto
    {
        public string DayOfTheWeek { get; set; }
        public double Calories { get; set; }
        public List<MealtimeDto> mealtimes { get; set; } 
    }
}
