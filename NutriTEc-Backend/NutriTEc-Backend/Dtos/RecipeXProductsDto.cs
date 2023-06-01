namespace NutriTEc_Backend.Dtos
{
    public class RecipeXProductsDto
    {
        public string RecipeName { get; set; }
        public List<ProductInRecipeDto> Products { get; set; }
    }
}
