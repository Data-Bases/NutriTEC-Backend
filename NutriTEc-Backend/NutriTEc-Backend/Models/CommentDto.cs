using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace NutriTEc_Backend.Models
{
    public class CommentDto
    {
        public int PatientId { get; set; }
        public int NutritionistId { get; set; }
        public DateTime Date { get; set; }
        public string Meal { get; set; }
        public string CommentText { get; set; }

        
    }
}
