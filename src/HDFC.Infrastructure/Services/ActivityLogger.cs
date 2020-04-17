using HDFC.Core.Entities.Misc;
using HDFC.Core.Services;
using HDFC.Infrastructure.Persistence;

namespace HDFC.Infrastructure.Services
{
    public class ActivityLogger : IActivityLogger
    {
        private readonly AppDbContext _dbContext;

        public ActivityLogger(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Log(ActivityLog activityLog)
        {
            _dbContext.ActivityLogs.Add(activityLog);
        }
    }
}
