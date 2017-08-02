using ActiveDirectoryAPI.Model;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using ActiveDirectoryAPI.Services;

namespace ActiveDirectoryAPI.Controllers
{
    [Route("api/users")]
    public class GroupsController : Controller
    {
        private ILogger<GroupsController> _logger;
        private IMailService _mailService;
        private IUserInfoRepository _userInfoRepository;

        public GroupsController(ILogger<GroupsController> logger, IMailService mailService, IUserInfoRepository userInfoRepository)
        {
            _logger = logger;
            _mailService = mailService;
            _userInfoRepository = userInfoRepository;
        }
        [HttpGet("{userID}/groups")]
        public IActionResult GetGroups(int userID)
        {
            try
            {
                //var user = UserDataStore.Current.Users.FirstOrDefault(u => u.ID == userID);

                if (!_userInfoRepository.UserExists(userID))
                {
                    _logger.LogInformation($"User with ID {userID} wasn't found when accessing points of interest");
                    return NotFound();
                }

                var groupsForUser = _userInfoRepository.GetGroupsForUser(userID);

                var groupsForUserResults = new List<Group>();

                foreach(var gp in groupsForUser)
                {
                    groupsForUserResults.Add(new Group()
                    {
                        ID = gp.ID,
                        GroupName = gp.GroupName,
                        Description = gp.Description
                    });
                }

                return Ok(groupsForUserResults);
                
                //if (user == null)
                //{
                //    _logger.LogInformation($"User with ID {userID} was not found.");
                //    return NotFound();
                //}
                //return Ok(user.Groups);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while getting groups for user with ID {userID}.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }
        [HttpGet("{userID}/groups/{groupID}", Name = "GetGroup")]
        public IActionResult GetGroup(int userID, int groupID)
        {
            if (!_userInfoRepository.UserExists(userID))
            {
                return NotFound();
            }

            var group = _userInfoRepository.GetGroupForUser(userID, groupID);

            if(group == null)
            {
                return NotFound();
            }

            var groupResult = new Group()
            {
                ID = group.ID,
                GroupName = group.GroupName,
                Description = group.Description
            };

            return Ok(groupResult);

            //var user = UserDataStore.Current.Users.FirstOrDefault(u => u.ID == userID);
            //if (user == null)
            //{
            //    return NotFound();
            //}

            //var group = user.Groups.FirstOrDefault(g => g.ID == groupID);
            //if (group == null)
            //{
            //    return NotFound();
            //}
            //return Ok(group);
        }
        [HttpPost("{userID}/groups")]
        public IActionResult CreateGroup(int userID, [FromBody] GroupForCreation group)
        {
            if(group == null)
            {
                return BadRequest();
            }

            if(group.Description == group.GroupName)
            {
                ModelState.AddModelError("Description", "The description should be different than the name.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = UserDataStore.Current.Users.FirstOrDefault(u => u.ID == userID);
            if (user == null)
            {
                return NotFound();
            }

            //-- to be changed
            var maxGroupID = UserDataStore.Current.Users.SelectMany(
                u => u.Groups).Max(g => g.ID);

            var finalGroup = new Group()
            {
                ID = ++maxGroupID,
                GroupName = group.GroupName,
                Description = group.Description
            };

            user.Groups.Add(finalGroup);

            return CreatedAtRoute("GetGroup", new {
                userID = userID, groupID = finalGroup.ID}, finalGroup);
        }

        [HttpPut("{userID}/groups/{groupID}")]
        public IActionResult UpdateGroup(int userID, int groupID, [FromBody] GroupForUpdate group)
        {
            if (group == null)
            {
                return BadRequest();
            }

            if (group.Description == group.GroupName)
            {
                ModelState.AddModelError("Description", "The description should be different than the name.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = UserDataStore.Current.Users.FirstOrDefault(u => u.ID == userID);
            if (user == null)
            {
                return NotFound();
            }

            var groupFromStore = user.Groups.FirstOrDefault(g => g.ID == groupID);
            if (groupFromStore == null)
            {
                return NotFound();
            }

            groupFromStore.GroupName = group.GroupName;
            groupFromStore.Description = group.Description;

            //typically return no content with put
            return NoContent();
        }

        [HttpPatch("{userID}/groups/{groupID}")]
        public IActionResult PartialUpdateGroup(int userID, int groupID,
            [FromBody] JsonPatchDocument<GroupForUpdate> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var user = UserDataStore.Current.Users.FirstOrDefault(u => u.ID == userID);
            if (user == null)
            {
                return NotFound();
            }

            var groupFromStore = user.Groups.FirstOrDefault(g => g.ID == groupID);
            if (groupFromStore == null)
            {
                return NotFound();
            }

            var groupToPatch =
                new GroupForUpdate()
                {
                    GroupName = groupFromStore.GroupName,
                    Description = groupFromStore.Description
                };

            patchDoc.ApplyTo(groupToPatch, ModelState);

            //checks patchDoc model state not GroupForUpdate model state
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (groupToPatch.Description == groupToPatch.GroupName)
            {
                ModelState.AddModelError("Description", "The description should be different than the name.");
            }

            TryValidateModel(groupToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            groupFromStore.GroupName = groupToPatch.GroupName;
            groupFromStore.Description = groupToPatch.Description;

            return NoContent();
        }
        [HttpDelete("{userID}/groups/{groupID}")]
        public IActionResult DeleteGroup(int userID, int groupID)
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

            user.Groups.Remove(group);

            _mailService.Send("Group delete", $"Group {group.GroupName} with ID {group.ID} was deleted.");

            return NoContent();

        }
    }
}
