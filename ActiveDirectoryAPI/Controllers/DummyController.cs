using ActiveDirectoryAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActiveDirectoryAPI.Controllers
{
    public class DummyController : Controller
    {
        private UserInfoContext _ctx;
        public DummyController(UserInfoContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet("api/testdatabase")]
        public IActionResult TestDatabase()
        {
            return Ok();
        }
    }
}
