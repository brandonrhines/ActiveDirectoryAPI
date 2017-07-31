using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ActiveDirectoryAPI.Controllers
{
    [Route("api/users")]
    public class UsersController : Controller
    {
        [HttpGet()]
        public IActionResult GetUsers()
        {
            return Ok(UserDataStore.Current.Users);

            //var temp = new JsonResult(UserDataStore.Current.Users);
            //temp.StatusCode = 200;
            //return temp;

            //return new JsonResult(UserDataStore.Current.Users);

            //return new JsonResult(new List<object>() {
            //    new {id = 1, UserName = "TheDude", Password = "mypassword", Domain = "mydomain"},
            //    new {id = 2, UserName = "TheOtherDude", Password = "mypassword", Domain = "mydomain"}
            //});
        }
        
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var userToReturn = UserDataStore.Current.Users.FirstOrDefault(u => u.ID == id);
            if(userToReturn == null)
            {
                return NotFound();
            }
            return Ok(userToReturn);

            //return new JsonResult(
            //    UserDataStore.Current.Users.First(u => u.ID == id));
        }
    }
}
