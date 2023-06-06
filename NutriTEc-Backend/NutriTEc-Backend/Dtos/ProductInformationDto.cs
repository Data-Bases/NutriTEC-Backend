namespace NutriTEc_Backend.Dtos
{
    public class ProductInformationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
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
