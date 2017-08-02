using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActiveDirectoryAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace ActiveDirectoryAPI.Services
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private UserInfoContext _context;
        public UserInfoRepository(UserInfoContext context)
        {
            _context = context;
        }

        public bool UserExists(int userID)
        {
            return _context.Users.Any(u => u.ID == userID);
        }
        public Group GetGroupForUser(int userID, int groupID)
        {
            return _context.Groups.Where(g => g.ID == groupID && g.UserID == userID).FirstOrDefault();
            
        }

        public IEnumerable<Group> GetGroupsForUser(int userID)
        {
            return _context.Groups.Where(g => g.UserID == userID).ToList();
        }

        public User GetUser(int userID, bool includeGroups)
        {
            if (includeGroups)
            {
                return _context.Users.Include(u => u.Groups)
                    .Where(u => u.ID == userID)
                    .FirstOrDefault();
            }

            return _context.Users.Where(u => u.ID == userID).FirstOrDefault();
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.OrderBy(u => u.UserName).ToList();
        }
    }
}
