using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Modules;
using DAL;
using ENTITIES;
namespace BL
{
    public class UserManager : IDisposable
    {

        public void Dispose()
        {
        }

        public LoginResponse Login(LoginRequest loginRequest)
        {
            LoginResponse response;
            //string pass = SHA.MD5Hash(loginRequest.pass);

            using(DalUser context = new DalUser()){
                //response = context.Login(loginRequest.username, pass);
                response = context.Login(loginRequest.userId, loginRequest.pass);

            }


            return response;




        }
    }
}
