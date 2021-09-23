using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace WebApi.Controllers
{
    public class TrackActivityController : ApiController
    {

        // GET: api/TrackActivity/ButtonClicked
        [HttpGet]
        [Route("api/TrackActivity/ButtonClicked/{id}/{button}")]
        public void ButtonClicked(int id, int button)
        {
            try
            {
                using (var manager = new TrackActivityManager())
                {
                    manager.SetButtonClicked(id, button);
                }

            }
            catch (Exception ex)
            {
                //-----------------------------------------------------------------------------adderror to db---------------------------------------------------------------------------------------------
               
            }
        }

        //public HttpResponseMessage OpenMail(int id)

        // GET: api/TrackActivity/ButtonClicked
        [HttpGet]
        [Route("api/TrackActivity/OpenMail/{id}")]
        public void OpenMail(int id)
        {
            try
            {
                using (var manager = new TrackActivityManager())
                {
                    manager.OpenMail(id);
                }
                //HttpResponseMessage response = new HttpResponseMessage();
                //Byte[] b = (GetImageByteArray());
                //response.Content = new ByteArrayContent(b);
                //response.Content.LoadIntoBufferAsync(b.Length).Wait();
                //response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                //return response;

            }
            catch (Exception ex)
            {
                //-----------------------------------------------------------------------------adderror to db---------------------------------------------------------------------------------------------

            }
        }

        
    }
}
