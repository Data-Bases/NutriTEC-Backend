using Nest;
using NutriTEc_Backend.Dtos;
using NutriTEc_Backend.Entities;
using NutriTEc_Backend.Models;

namespace NutriTEc_Backend.Repository.Interface
{
    public interface IForumRepository
    {
        /// <summary>
        /// Create a new comment
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        Result Create(CommentDto comment);

        /// <summary>
        /// Get comments filter by patient id
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        List<Comment> GetFilteredComments(int patientId, DateTime dateTime, string meal);

        /// <summary>
        /// Get all comments
        /// </summary>
        /// <returns></returns>
        List<Comment> GetAll();
    }
}
