namespace NutriTEc_Backend.Dtos
{
    public class RecipeInPlanDto
    {
        public int RecipeId { get; set; }
        public double Servings { get; set; }
        public string Mealtime { get; set; }
        public string ConsumeWeekDay { get; set; }
    }
}
