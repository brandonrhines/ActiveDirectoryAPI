using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActiveDirectoryAPI.Model
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; }
        public int NumberOfGroups { get
            {
                return Groups.Count;
            }
        }
        public ICollection<Group> Groups { get; set; }
        = new List<Group>();
    }
}
