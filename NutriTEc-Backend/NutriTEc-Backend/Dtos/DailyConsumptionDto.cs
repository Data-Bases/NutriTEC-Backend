namespace NutriTEc_Backend.Dtos
{
    public class DailyConsumptionDto
    {
        public DateTime Date { get; set; }
        public double TotalCalories { get; set; }
        public double TotalCaloriesBreakfast { get; set; }
        public double TotalCaloriesLunch { get; set; }
        public double TotalCaloriesDinner { get; set; }
        public double TotalCaloriesSnack { get; set; }
        public List<ConsumedByPatient> Breakfast { get; set; }
        public List<ConsumedByPatient> Lunch { get; set; }
        public List<ConsumedByPatient> Dinner { get; set; }
        public List<ConsumedByPatient> Snack { get; set; }
    }
}
