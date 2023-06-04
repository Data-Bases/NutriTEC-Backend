using Microsoft.Identity.Client;

namespace NutriTEc_Backend.Dtos
{
    public class ProductInRecipeDto
    {
        public int Id { get; set; }
        public double Servings { get; set; }
        public string Mealtime { get; set; }
        public string ConsumedWeekDay { get; set; }
    }
}