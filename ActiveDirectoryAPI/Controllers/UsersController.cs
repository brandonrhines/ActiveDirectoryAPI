using ActiveDirectoryAPI.Model;
using ActiveDirectoryAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ActiveDirectoryAPI.Controllers
{
    [Route("api/users")]
    public class UsersController : Controller
    {
        private IUserInfoRepository _userInfoRepository;
        public UsersController(IUserInfoRepository userInfoRepository)
        {
            _userInfoRepository = userInfoRepository;
        }
        [HttpGet()]
        public IActionResult GetUsers()
        {
            var userEntities = _userInfoRepository.GetUsers();

            var results = new List<UserWithoutGroups>();

            foreach (var userEntity in userEntities)
            {
                results.Add(new UserWithoutGroups
                {
                    ID = userEntity.ID,
                    UserName = userEntity.UserName,
                    Password = userEntity.Password,
                    Domain = userEntity.Domain
                });
            }

            return Ok(results);


            //return Ok(UserDataStore.Current.Users);

            //var temp = new JsonResult(UserDataStore.Current.Users);
            //temp.StatusCode = 200;
            //return temp;

            //return new JsonResult(UserDataStore.Current.Users);

            //return new JsonResult(new List<object>() {
            //    new {id = 1, UserName = "TheDude", Password = "mypassword", Domain = "mydomain"},
            //    new {id = 2, UserName = "TheOtherDude", Password = "mypassword", Domain = "mydomain"}
            //});
        }

        [HttpGet("{userID}")]
        public IActionResult GetUser(int userID, bool includeGroups = false)
        {
            var user = _userInfoRepository.GetUser(userID, includeGroups);

            if(user == null)
            {
                return NotFound();
            }

            if (includeGroups)
            {
                var userResult = new User()
                {
                    ID = user.ID,
                    UserName = user.UserName,
                    Password = user.Password,
                    Domain = user.Domain
                };

                foreach(var gp in user.Groups)
                {
                    userResult.Groups.Add(
                        new Group()
                        {
                            ID = gp.ID,
                            GroupName = gp.GroupName,
                            Description = gp.Description,
                        });
                }
                return Ok(userResult);
            }

            var userWithoutGroupsResult =
                new UserWithoutGroups()
                {
                    ID = user.ID,
                    UserName = user.UserName,
                    Password = user.Password,
                    Domain = user.Domain
                };

            return Ok(userWithoutGroupsResult);

            //var userToReturn = UserDataStore.Current.Users.FirstOrDefault(u => u.ID == id);
            //if(userToReturn == null)
            //{
            //    return NotFound();
            //}
            //return Ok(userToReturn);

            //return new JsonResult(
            //    UserDataStore.Current.Users.First(u => u.ID == id));
        }
    }
}
