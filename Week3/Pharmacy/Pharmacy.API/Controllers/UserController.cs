using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Model;
using Pharmacy.Model.ModelUser;
using Pharmacy.Service.UserServiceLayer;
using System.Collections.Generic;

namespace Pharmacy.API.Controllers
{
    [Route("[controller]s")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public UserController(IUserService _userService, IMapper _mapper)
        {
            userService = _userService;
            mapper = _mapper;
        }

        //Kullanici kayit ekleme
        [HttpPost]
        public General<UserViewModel> Insert([FromBody] UserViewModel newUser)
        {
            var result = false;
            return userService.Insert(newUser);
        }

        //Login islemi
        [LoginFilter] //Attribute
        [HttpPost("login")]
        public bool Login(string mail, string password)
        {
            return userService.Login(mail, password);
        }

        //Tum kullacilari listeleme
        [HttpGet]
        public General<UserViewModel> GetUsers()
        {
            return userService.GetUsers();
        }

        //id ye göre kullanıcı guncelleme
        [HttpPut("{id}")]
        public General<UserViewModel> Update(int id, [FromBody] UserViewModel user)
        {
            return userService.Update(id, user);
        }

        //kullanici silme
        [HttpDelete]
        public General<UserViewModel> Delete(int id)
        {
            return userService.Delete(id);
        }

        //Hastalari Listeleme
        [HttpGet("patients")]
        public List<Pharmacy.DB.Entities.User> GetPatients()
        {
            return userService.GetPatients();
        }
    }
}
