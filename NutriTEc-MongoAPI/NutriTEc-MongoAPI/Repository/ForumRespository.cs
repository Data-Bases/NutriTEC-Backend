using Nest;
using NutriTEc_Backend.Dtos;
using NutriTEc_Backend.Repository.Interface;
using MongoDB.Driver;
using NutriTEc_Backend.Entities;
using static Nest.JoinField;

namespace NutriTEc_Backend.Repository
{
    public class ForumRepository : IForumRepository
    {

        private readonly IMongoCollection<Comment> _comments;

        public ForumRepository(ICommentsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _comments = database.GetCollection<Comment>(settings.CommentsCollectionName);
        }

        public List<Comment> GetAll()
          {
            var comments = new List<Comment>();
            comments = _comments.Find(comment => true).ToList();
            return comments;

        }

        public List<Comment> GetFilteredComments(int patientId, DateTime dateTime, string meal)
        {
            var comments = _comments.Find(comment => comment.PatientId == patientId && comment.Date == dateTime.ToString("yyyy-MM-dd") && comment.Meal == meal).ToList();
            return comments;
        }

        public Result Create(CommentDto comment)
        {
            try
            {
                var commentToInsert = new Comment()
                {
                    CommentText = comment.CommentText,
                    Date = DateOnly.FromDateTime(comment.Date).ToString("yyyy-MM-dd"),
                    Meal = comment.Meal,
                    NutritionistId = comment.NutritionistId,
                    PatientId = comment.PatientId
                };

                _comments.InsertOne(commentToInsert);
                return Result.Created;
            }
            catch (Exception ex)
            {
                return Result.Error;
            }
        }
        
    }
}
