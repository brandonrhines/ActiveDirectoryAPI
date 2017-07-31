using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActiveDirectoryAPI.Controllers
{
    [Route("api/users")]
    public class GroupsController : Controller
    {
        [HttpGet("{userID}/groups")]
        public IActionResult GetGroups(int userID)
        {
            var user = UserDataStore.Current.Users.FirstOrDefault(u => u.ID == userID);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user.Groups);
        }
        [HttpGet("{userID}/{groupID}")]
        public IActionResult GetGroup(int userID, int groupID)
        {
            var user = UserDataStore.Current.Users.FirstOrDefault(u => u.ID == userID);
            if (user == null)
            {
                return NotFound();
            }

            var group = user.Groups.FirstOrDefault(g => g.ID == groupID);
            if (group == null)
            {
                return NotFound();
            }
            return Ok(group);
        }
    }
}
