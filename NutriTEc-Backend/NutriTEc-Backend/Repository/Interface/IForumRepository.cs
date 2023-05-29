using Nest;
using NutriTEc_Backend.Dtos;
using NutriTEc_Backend.Entities;

namespace NutriTEc_Backend.Repository.Interface
{
    public interface IForumRepository
    {
        /// <summary>
        /// Create a new comment
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        Comment Create(Comment comment);

        /// <summary>
        /// Get comments filter by patient id
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        List<Comment> GetFilteredComments(int patientId);

        /// <summary>
        /// Get all comments
        /// </summary>
        /// <returns></returns>
        List<Comment> GetAll();
    }
}
