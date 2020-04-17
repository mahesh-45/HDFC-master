using HDFC.Core.Enums;
using HDFC.Core.Interfaces;
using HDFC.Core.SharedKernel;

namespace HDFC.Core.Entities.Masters
{
  public class CostCode: AggregateRoot, IStatus
    {

        private CostCode()
        { }

        public CostCode(string code ,string name ,string bhEmpCode ,string bh , string adGroup, string adEmpCode, string head, 
                        StatusEnum status, long userId )
        {
            Code = code;
            Name = name;
            BHEmpCode = bhEmpCode;
            BH = bh;
            ADGroup = adGroup;
            ADEmpCode = adEmpCode;
            Head = head;
            Status = status;
            UpdateAudit(userId);
        }

        public string Code { get; private set; }
        public string Name { get; private set; }
        public string BHEmpCode { get; private set; }
        public string BH { get; private set; }
        public string ADGroup { get; private set; }
        public string ADEmpCode { get; private set; }
        public string Head { get; private set; }                               //Business Head/Group Head
        public StatusEnum Status { get; private set; }

        public void Update(string code, string name, string bhEmpCode,string bh, string adGroup, string adEmpCode, string head, 
                           StatusEnum status, long userId)
        {
            Code = code;
            Name = name;
            BHEmpCode = bhEmpCode;
            BH = bh;
            ADGroup = adGroup;
            ADEmpCode = adEmpCode;
            Head = head;
            Status = status;
            UpdateAudit(userId);
        }
    }
}
