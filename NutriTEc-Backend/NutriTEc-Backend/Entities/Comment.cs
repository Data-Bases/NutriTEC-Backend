using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace NutriTEc_Backend.Entities
{
    public class Comment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int PatientId { get; set; }
        public int NutritionistId { get; set; }
        public string Date { get; set; }
        public string Meal { get; set; }
        public string CommentText { get; set; }
       
    }
}
