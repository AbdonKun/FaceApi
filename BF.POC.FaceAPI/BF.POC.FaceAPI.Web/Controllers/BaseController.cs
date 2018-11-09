using NLog;
using System.Web.Mvc;

namespace BF.POC.FaceAPI.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        // Common properties
        protected readonly ILogger logger = LogManager.GetCurrentClassLogger();

    }
}