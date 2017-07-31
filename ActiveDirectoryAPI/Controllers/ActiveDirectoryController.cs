using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActiveDirectoryAPI.Controllers
{
    [Route("api/activedirectory")]
    public class ActiveDirectoryController : Controller
    {
        [HttpGet()]
        public JsonResult GetUsers()
        {
            return new JsonResult(UserDataStore.Current.Users);

            //return new JsonResult(new List<object>() {
            //    new {id = 1, UserName = "TheDude", Password = "mypassword", Domain = "mydomain"},
            //    new {id = 2, UserName = "TheOtherDude", Password = "mypassword", Domain = "mydomain"}
            //});
        }
        
        [HttpGet("{id}")]
        public JsonResult GetUser(int id)
        {
            return new JsonResult(
                UserDataStore.Current.Users.First(u => u.ID == id));
        }
    }
}
