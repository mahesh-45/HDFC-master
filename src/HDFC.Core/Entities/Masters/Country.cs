using HDFC.Core.Enums;
using HDFC.Core.Interfaces;
using HDFC.Core.SharedKernel;

namespace HDFC.Core.Entities.Masters
{
    public class Country : AggregateRoot, IStatus
    {
        private Country()
        {
        }

        public Country(string name, string code, StatusEnum status, long userId)
        {
            Name = name;
            Code = code;
            Status = status;
            UpdateAudit(userId);
        }

        public string Name { get; private set; }
        public string Code { get; private set; }
        public StatusEnum Status { get; private set; }

        public void Update(string name, string code, StatusEnum status, long userId)
        {
            Code = code;
            Name = name;
            Status = status;
            UpdateAudit(userId);
        }
    }
}
