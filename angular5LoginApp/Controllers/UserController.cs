
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using angular5LoginApp.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace angular5LoginApp.Controllers
{
    public class UserController : Controller
    {
        UserDataAccessLayer objuser = new UserDataAccessLayer();

        [HttpGet]
        [Route("api/User/Index")]
        public IEnumerable<User> Index()
        {
            return objuser.GetAllUsers();
        }
        [HttpPost]
        [Route("api/User/Create")]
        public int Create([FromBody] User user)
        {
            return objuser.AddUser(user);
        }
        [HttpGet]
        [Route("api/User/Details/{id}")]
        public User Details(int id)
        {
            return objuser.GetUserData(id);
        }
        [HttpPut]
        [Route("api/User/Edit")]
        public int Edit([FromBody]User user)
        {
            return objuser.UpdateUser(user);
        }
        [HttpDelete]
        [Route("api/User/Delete/{id}")]
        public int Delete(int id)
        {
            return objuser.DeleteUser(id);
        }
    }
}
