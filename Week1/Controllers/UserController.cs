using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserSystemApi;

namespace UserSystem.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class UserController : ControllerBase
    {
        private List<User> UserList = new List<User>()
    {
        new User
        {
            Id=1,
            Name="Harun",
            Surname="Delipoyraz",
            Email="harundelipoyraz@patika.com",
            Password="harun123123.",
            RePassword="harun123123."
        },

        new User
        {
            Id=2,
            Name="Feyza",
            Surname="Keles",
            Email="fyzakeles@gmail.com",
            Password="feyza123123.",
            RePassword="feyza123123."
        },

        new User
        {
            Id=3,
            Name="Ali",
            Surname="Demir",
            Email="alidemir@gmail.com",
            Password="ali123123.",
            RePassword="ali123123."
        }
    };

        [HttpGet]
        public List<User> GetUsers()
        {
            var userList = UserList.OrderBy(x => x.Id).ToList<User>();
            return userList;
        }

        [HttpGet(template: "{id}")]
        public User GetById(int id)
        {
            var user = UserList.Where(user => user.Id == id).SingleOrDefault();
            return user;
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] User newUser)
        {
            var user = UserList.SingleOrDefault(x => x.Name == newUser.Name);

            if (user is not null)
                return BadRequest();

            UserList.Add(newUser);
            return Ok();
        }

        [HttpPut(template: "{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            var user = UserList.SingleOrDefault(x => x.Id == id);

            if (user is null)
                return BadRequest();

            user.Name = updatedUser.Name != default ? updatedUser.Name : user.Name;
            user.Surname = updatedUser.Surname != default ? updatedUser.Surname : user.Surname;
            user.Email = updatedUser.Email != default ? updatedUser.Email : user.Email;
            user.Password = updatedUser.Password != default ? updatedUser.Password : user.Password;
            user.RePassword = updatedUser.RePassword != default ? updatedUser.RePassword : user.RePassword;

            return Ok();
        }

        [HttpDelete(template: "{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = UserList.SingleOrDefault(x => x.Id == id);

            if (user is null)
                return BadRequest();


            UserList.Remove(user);
            return Ok();
        }
    }
}
