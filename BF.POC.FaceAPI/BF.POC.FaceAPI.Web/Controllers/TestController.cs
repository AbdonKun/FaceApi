using BF.POC.FaceAPI.Domain.Contracts.Clients;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BF.POC.FaceAPI.Web.Controllers
{
    public class TestController : BaseController
    {
        private readonly IFaceAPIClient faceAPIClient;

        public TestController(IFaceAPIClient faceAPIClient)
        {
            this.faceAPIClient = faceAPIClient;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Analyze()
        {
            try
            {
                var image = Convert.FromBase64String(System.Web.HttpContext.Current.Request.Form["IMAGE"]);

                var response = await faceAPIClient.FaceDetectAsync(image);

                return Json(response);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}