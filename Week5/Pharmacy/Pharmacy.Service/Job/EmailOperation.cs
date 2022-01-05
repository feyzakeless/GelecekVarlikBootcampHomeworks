using AutoMapper;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Pharmacy.DB.Entities.DataContext;
using Pharmacy.Model;
using Pharmacy.Model.ModelUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Service.Job
{
    public class EmailOperation :IEmailOperation
    {
        private readonly IMapper mapper; //Mapper çagrildi

        public EmailOperation(IMapper _mapper)
        {
            mapper = _mapper;
        }

        //Mail gönderme methodu
        public void sendEmail()
        {
            var result = new General<UserViewModel>();
            using (var context = new PharmacyContext())
            {
                //Kullanci tablosundan Silinmemis ve Mail gönderilmemis olanları çekiyoruz.
                var userList = context.User.Where(x => !x.IsDeleted && !x.IsSendEmail).OrderBy(x => x.Id);


                foreach (var user in userList)
                { 
                    var message = new MimeMessage(); //Mailkit objesi
                    message.From.Add(new MailboxAddress("Pharmacy", "info@pharmacy.com")); //kimden gidecek
                    message.To.Add(new MailboxAddress("User", user.Email)); //kime gidecek
                    message.Subject = "Welcome to the pharmacy"; //Mailin konusu
                    message.Body = new TextPart("plain") //Mailin body si
                    {
                        Text = "Dear"+user.Name+ ", Welcome To the Pharmacy!"
                    };

                    using (var client = new SmtpClient()) //smptp sağlayıcı
                    {
                        //client.Connect("smtp@gmail.com", 465, false);
                        //client.Connect("smtp@gmail.com", 587, SecureSocketOptions.StartTls);
                        //client.Connect("smtp.yandex.com", 465, true);
                        client.Connect("smtp@gmail.com", 587, false); //port a bağlanıyoruz
                        client.Authenticate("info@pharmacy.com", "visualstudio"); //kullanıcı adı ve şifreyi veriyoruz
                        client.Send(message); //mesajı gönderiyoruz
                        client.Disconnect(true);
                    }

                    user.IsSendEmail = true;
                    context.SaveChanges();

                }
            }

        }
    }
}
