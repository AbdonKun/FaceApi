using System.Web.Mvc;

namespace BF.POC.FaceAPI.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}