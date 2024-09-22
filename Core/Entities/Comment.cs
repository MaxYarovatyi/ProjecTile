using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Comment
    {
        public Comment()
        {
        }

        public Comment(string id)
        {
            Guid = id;
        }

        public string Guid { get; set; }
        public string AuthorGuid { get; set; }
        public string CommentText { get; set; }

    }
}