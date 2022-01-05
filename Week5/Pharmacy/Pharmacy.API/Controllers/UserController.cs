using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Pharmacy.API.Infrastructure;
using Pharmacy.Model;
using Pharmacy.Model.ModelLogin;
using Pharmacy.Model.ModelUser;
using Pharmacy.Service.UserServiceLayer;
using System.Collections.Generic;

namespace Pharmacy.API.Controllers
{
    [Route("[controller]s")]
    [ApiController]
    
    public class UserController : ControllerBase
    {
        private readonly IUserService userService; //User service i çağırıyoruz.
        private readonly IMapper mapper;

        public UserController(IUserService _userService, IMapper _mapper) 
        {
            userService = _userService;
            mapper = _mapper;
        }

        
        //Kullanici kayit ekleme
        [HttpPost]
        [ServiceFilter(typeof(LoginFilter))] //Attribute
        public General<UserViewModel> Insert([FromBody] UserViewModel newUser)
        {
            return userService.Insert(newUser);//CurrentUser ın Id si birden büyükse insert edip devam edicek.
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

        /*---------------------------------- Week 3 --------------------------------*/
        //Hastalari Listeleme
        [HttpGet("patients")]
        public List<Pharmacy.DB.Entities.User> GetPatients()
        {
            return userService.GetPatients();
        }
    }
}
