using ActiveDirectoryAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActiveDirectoryAPI
{
    public static class UserInfoExtensions
    {
        public static void EnsureSeedDataForContext(this UserInfoContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            //init seed data
            var users = new List<User>()
            {
                new User()
                {
                    UserName = "The Dude",
                    Password = "mypassword",
                    Domain = "mydomain",
                    Groups = new List<Group>()
                    {
                        new Group()
                        {
                            GroupName = "Project Management",
                            Description = "Full access Project Management",
                        },
                        new Group()
                        {
                            GroupName = "Payroll",
                            Description = "Full access Payroll"
                        }
                    }
                }, 
                new User()
                {
                    UserName = "The Othe Dude",
                    Password = "password", 
                    Domain = "domain", 
                    Groups = new List<Group>()
                    {
                        new Group()
                        {
                            GroupName = "Project Management",
                            Description = "Full access Project Management",
                        }
                    }
                }
            };

            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}
