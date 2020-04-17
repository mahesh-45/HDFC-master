using HDFC.Core.Enums;
using HDFC.Core.Interfaces;
using HDFC.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDFC.Core.Entities.Masters.Division
{
    public class Division : AggregateRoot, IStatus
    {
        private Division()
        {
        }

        public Division(string name, string code, StatusEnum status,long userId)
        {
            Name = name;
            Code = code;
            Status = status;
            UpdateAudit(userId);
        }
        public void Update(string name, string code, StatusEnum status,long userId)
        {
            Name = name;
            Code = code;
            Status = status;
            UpdateAudit(userId);
        }

        public void AddSubDivisions(List<SubDivision> subDivisions, long userId)
        {
            SubDivisions = new List<SubDivision>();
            foreach (var item in subDivisions)
            {
                var subDivision = new SubDivision(item.Name, item.Code);
                SubDivisions.Add(subDivision);
            }
            UpdateAudit(userId);
        }

        public void RemoveSubDivision(int subDivisionId, long userId)
        {
            var item = SubDivisions.Single(s => s.Id == subDivisionId);
            SubDivisions.Remove(item);
            UpdateAudit(userId);
        }
        public string Name { get; private set; }
        public string Code { get; private set; }
        public StatusEnum Status { get; private set; }

        public IList<SubDivision> SubDivisions { get; private set; }
    }
}
