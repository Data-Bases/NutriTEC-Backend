namespace NutriTEc_Backend.Models
{
    public class ProductRecipeNutrients
    {
        public int RecipeId { get; set; }

        public string RecipeName { get; set; }

        public string ProductName { get; set; } = null!;

        public double Servings { get; set; }

        public double PortionSize { get; set; }

        public double Energy { get; set; }

        public double Fat { get; set; }

        public double Sodium { get; set; }

        public double Carbs { get; set; }

        public double Protein { get; set; }

        public double Calcium { get; set; }

        public double Iron { get; set; }

    }
}
