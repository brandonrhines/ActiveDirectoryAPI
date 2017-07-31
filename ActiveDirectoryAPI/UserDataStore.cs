using ActiveDirectoryAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActiveDirectoryAPI
{
    public class UserDataStore
    {
        public static UserDataStore Current { get; } = new UserDataStore();
        public List<User> Users { get; set; }
        public List<Group> Groups { get; set; }
        public List<UserGroup> UserGroups { get; set; }
        public UserDataStore()
        {
            Users = new List<User>()
            {
                new User()
                {
                    ID = 1,
                    UserName = "The Dude",
                    Password = "MyPassword", 
                    Domain = "MyDomain",
                },
                new User()
                {
                    ID = 2,
                    UserName = "The Other Dude",
                    Password = "MyPassword",
                    Domain = "MyDomain"
                }
            };

            Groups = new List<Group>()
            {
                new Group()
                {
                    ID = 1,
                    GroupName = "Project Management",
                    Description = "Full access Project Management"
                },
                new Group()
                {
                    ID = 2, 
                    GroupName = "Payroll",
                    Description = "Full access Payroll"
                }
            };

            UserGroups = new List<UserGroup>()
            {
                new UserGroup()
                {
                    ID = 1,
                    UserID = 1,
                    GroupID = 1
                },
                new UserGroup()
                {
                    ID = 2,
                    UserID = 1,
                    GroupID = 2
                },
                new UserGroup()
                {
                    ID = 3, 
                    UserID = 2,
                    GroupID = 2
                }
            };
        }
    }
}
