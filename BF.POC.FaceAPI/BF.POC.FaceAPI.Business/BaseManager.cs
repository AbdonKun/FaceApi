using NLog;

namespace BF.POC.FaceAPI.Business
{
    public abstract class BaseManager
    {
        // Common properties
        protected readonly ILogger logger = LogManager.GetCurrentClassLogger();

    }
}
