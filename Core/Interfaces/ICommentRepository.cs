using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ICommentRepository
    {
        public Task<Comment> GetCommentByIdAsync(string id);
        public Task<Comment> UpdateComment(string id, Comment comment);
    }
}