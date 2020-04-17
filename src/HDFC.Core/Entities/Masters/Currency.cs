using HDFC.Core.Enums;
using HDFC.Core.Interfaces;
using HDFC.Core.SharedKernel;


namespace HDFC.Core.Entities.Masters
{
    public class Currency : AggregateRoot, IStatus
    {
        private Currency()
        {
        }
        public Currency(string name, string code, string description, StatusEnum status, long userId)
        {
            Name = name;
            Code = code;
            Description = description;
            Status = status;

            UpdateAudit(userId);
        }
        public string Name { get; private set; }
        public string Code { get; private set; }
        public string Description { get; private set; }
        public StatusEnum Status { get; set; }

        public void Update(string name, string code, string description, StatusEnum status, long userId)
        {
            Name = name;
            Code = code;
            Description = description;
            Status =status;

            UpdateAudit(userId);
        }

        public void InActivate()
        {
          Status = StatusEnum.InActive;
        }
    }
}
