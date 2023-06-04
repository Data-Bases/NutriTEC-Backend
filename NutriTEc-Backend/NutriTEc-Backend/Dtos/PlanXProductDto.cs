namespace NutriTEc_Backend.Dtos
{
    public class PlanDto
    {
        public string PlanName { get; set; }
        public int NutriId { get; set; }
        public List<ProductInPlanDto>? Products { get; set; }

        public List<RecipeInPlanDto>? Recipes { get; set; }
    }
}
