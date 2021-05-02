using ENTITIES;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {

        // POST: api/User
        public LoginResponse PostValue([FromBody()] LoginRequest request ) {
            var response = new LoginResponse();
            var Responseloc = HttpContext.Current.Response;
            using (var manager = new UserManager()) {
                try
                {
                    if (!(request is null)) {


                        response = manager.Login(request);
                            //response.token = Token.GetToken(user)
                            //Responseloc.Cookies("bobsfog").Value = user.username
                            //Responseloc.Cookies("bobsfog").Secure = False
                            //Responseloc.Cookies("bobsfog").HttpOnly = False
                            //Responseloc.Cookies("bobsfog").Domain = ".my-epr.co.il"
                            //Responseloc.Cookies("betaver").Value = response.token
                            //Responseloc.Cookies("betaver").Domain = ".my-epr.co.il"
                            //Responseloc.Cookies("betaver").Expires = DateTime.MaxValue

                      }
                    else {
                        response.rc = 3;
                        response.body = "שם משתמש או הסיסמה אינם נכונים";
                        response.title = "😯שגיאה בהתחברות";

                    }

                }catch (Exception ex){

                    response.rc = 99;
                    response.body = "תקלה שאצלנו, אנחנו כנראה באמצע לתקן. אנא המתן מספר דקות";
                    response.title = "😯שגיאה בהתחברות";
                    response.desc = ex.Message;
                }



            }

                return response;
        }
     
        // GET: api/User
        public string GetValue()
        {
            return "ok";
        }

    }
}

