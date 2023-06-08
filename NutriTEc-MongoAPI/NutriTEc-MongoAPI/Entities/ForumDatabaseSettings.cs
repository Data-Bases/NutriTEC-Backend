namespace NutriTEc_Backend.Entities
{
    public class ForumDatabaseSettings : ICommentsDatabaseSettings
    {
        public string CommentsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ICommentsDatabaseSettings
    {
        string CommentsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
