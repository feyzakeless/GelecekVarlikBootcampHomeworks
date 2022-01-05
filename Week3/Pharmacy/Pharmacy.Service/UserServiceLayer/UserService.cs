using AutoMapper;
using Pharmacy.DB.Entities.DataContext;
using Pharmacy.Model;
using Pharmacy.Model.ModelUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Pharmacy.Service.UserServiceLayer
{
    public class UserService : IUserService 
    {
        private readonly IMapper mapper; //Mapper çagrildi

        public UserService(IMapper _mapper)
        {
            mapper = _mapper;
        }


        //kullanici kayıt islemi
        public General<UserViewModel> Insert(UserViewModel newUser)
        {
            var result = new General<UserViewModel>() { IsSuccess = false };
            try
            {
                var model = mapper.Map<Pharmacy.DB.Entities.User>(newUser);
                using (var srv = new PharmacyContext())
                {
                    model.Idatetime = DateTime.Now;
                    srv.User.Add(model);
                    srv.SaveChanges();
                    result.Entity = mapper.Map<UserViewModel>(model);
                    result.IsSuccess = true;
                }
            }
            catch (Exception)
            {

                result.ExceptionMessage = "Kayıt işlemi gerçekleşmedi.";
            }

            return result;
        }

        // kullanici giris islemi
        public bool Login(string mail, string password)
        {
            bool result = false;
            using (var srv = new PharmacyContext())
            {
                result = srv.User.Any(a => !a.IsDeleted && a.Email == mail && a.Password == password);

            }
            return result;
        }


        //kullanicilarin listelendigi metod
        public General<UserViewModel> GetUsers()
        {
            var result = new General<UserViewModel>();

            using (var context = new PharmacyContext())
            {
                var data = context.User
                    .Where(x => !x.IsDeleted)
                    .OrderBy(x => x.Id);

                if (data.Any())
                {
                    result.List = mapper.Map<List<UserViewModel>>(data);
                    result.IsSuccess = true;
                }
                else
                {
                    result.ExceptionMessage = "Hiçbir kullanıcı bulunamadı.";
                }
            }

            return result;
        }

        //kullanici guncelleme islemi
        public General<UserViewModel> Update(int id, UserViewModel user)
        {
            var result = new General<UserViewModel>();

            using (var context = new PharmacyContext())
            {
                var updateUser = context.User.SingleOrDefault(i => i.Id == id);

                if (updateUser is not null)
                {
                    updateUser.AuthorizeId = user.AuthorizeId;
                    updateUser.Name = user.Name;
                    updateUser.Surname = user.Surname;
                    updateUser.Email = user.Email;
                    updateUser.Password = user.Password;

                    
                    context.SaveChanges();

                    result.Entity = mapper.Map<UserViewModel>(updateUser);
                    result.IsSuccess = true;
                }
                else
                {
                    result.ExceptionMessage = "Bir hata oluştu.";
                }
            }

            return result;
        }

        //kullanici silme islemi
        public General<UserViewModel> Delete(int id)
        {
            var result = new General<UserViewModel>();

            using (var context = new PharmacyContext())
            {
                var user = context.User.SingleOrDefault(i => i.Id == id);

                if (user is not null)
                {
                    context.User.Remove(user);
                    context.SaveChanges();

                    result.Entity = mapper.Map<UserViewModel>(user);
                    result.IsSuccess = true;
                }
                else
                {
                    result.ExceptionMessage = "Kullanıcı bulunamadı.";
                    result.IsSuccess = false;
                }
            }

            return result;
        }


        //Authorized id si 1 olanlari(Hasta olanlari) cekme
        public List<Pharmacy.DB.Entities.User> GetPatients()
        {
            using (var context = new PharmacyContext())
            {
                var data = context.User
                    .Where(x => x.AuthorizeId == 1)
                    .OrderBy(x => x.Id).ToList();

                return data;
            }

            
        }

    }
}
