namespace NutriTEc_Backend.Dtos
{
    public class PatientProductDto
    {
        public int ProductId { get; set; }
        public int PatientId { get; set; }
        public string Mealtime { get; set; }
        public DateTime Consumedate { get; set; }
    }
}
