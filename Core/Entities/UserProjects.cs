using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class UserProjects
    {
        public UserProjects(){

        }
        public UserProjects(string id){
            this.Id = id;
        }
        public string Id { get; set; }
        public List<string> Projects { get; set; }
    }
}