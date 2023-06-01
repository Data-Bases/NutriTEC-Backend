using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Nest;
using NutriTEc_Backend.Dtos;
using NutriTEc_Backend.Helpers;
using NutriTEc_Backend.DataModel;
using NutriTEc_Backend.Repository.Interface;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Npgsql;
using MongoDB.Driver;
using NutriTEc_Backend.Entities;

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

        public List<Comment> GetFilteredComments(int patientId) =>
            _comments.Find(comment => comment.PatientId == patientId).ToList();

        public Comment Create(Comment comment)
        {
            _comments.InsertOne(comment);
            return comment;
        }
        
    }
}
