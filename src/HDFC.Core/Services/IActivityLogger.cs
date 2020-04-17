using HDFC.Core.Entities.Misc;

namespace HDFC.Core.Services
{
    public interface IActivityLogger
    {
        void Log(ActivityLog activityLog);
    }
}
