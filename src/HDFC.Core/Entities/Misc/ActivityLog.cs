using HDFC.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace HDFC.Core.Entities.Misc
{
    public class ActivityLog : BaseEntity
    {
        private ActivityLog() { }

        public string EntityName { get; private set; }
        public string Uid { get; private set; }
        public long ActionBy { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedDate { get; private set; }

        public ActivityLog(string entityName, string uid, string description, long actionBy)
        {
            EntityName = entityName;
            Uid = uid;
            ActionBy = actionBy;
            Description = description;
            CreatedDate = DateTime.UtcNow;
        }
    }
}
