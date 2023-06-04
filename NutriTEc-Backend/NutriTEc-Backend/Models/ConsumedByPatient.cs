namespace NutriTEc_Backend.Models
{
    public class ConsumedByPatient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Servings { get; set; }
        public double Energy { get; set; }
        public string Mealtime { get; set; }
    }
}
