using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Data
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IDatabase _redis;
        public CommentRepository(IConnectionMultiplexer connection)
        {
            _redis = connection.GetDatabase(2);
        }

        public async Task<Comment> GetCommentByIdAsync(string id)
        {
            var data = await _redis.StringGetAsync(id);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Comment>(data, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<Comment> UpdateComment(string id, Comment comment)
        {
            var data = await GetCommentByIdAsync(id);
            var updatedComment = data == null ? new Comment(id) : comment;
            var created = await _redis.StringSetAsync(id, JsonSerializer.Serialize(updatedComment));
            return !created ? null : await GetCommentByIdAsync(id);
        }
    }
}