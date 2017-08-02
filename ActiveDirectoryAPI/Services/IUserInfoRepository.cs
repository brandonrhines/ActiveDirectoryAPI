using ActiveDirectoryAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActiveDirectoryAPI.Services
{
    public interface IUserInfoRepository
    {
        bool UserExists(int userID);
        IEnumerable<User> GetUsers();
        User GetUser(int userID, bool includeGroups);
        IEnumerable<Group> GetGroupsForUser(int userID);
        Group GetGroupForUser(int userID, int groupID);


    }
}
