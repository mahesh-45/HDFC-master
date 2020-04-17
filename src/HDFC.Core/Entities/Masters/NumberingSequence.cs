using HDFC.Core.Enums;
using HDFC.Core.Interfaces;
using HDFC.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace HDFC.Core.Entities.Masters
{
    public class NumberingSequence : AggregateRoot, IStatus
    {
        private NumberingSequence()
        {
        }

        public NumberingSequence(string prefix, string number, StatusEnum status, NumberingSequenceTypeEnum numberingSequenceType, long userId)
        {
            Prefix = prefix;
            Number = number;
            Status = status;
            NumberingSequenceType = numberingSequenceType;
            UpdateAudit(userId);
        }
        public void Update(string prefix, string number, StatusEnum status, NumberingSequenceTypeEnum numberingSequenceType, long userId)
        {
            Prefix = prefix;
            Number = number;
            Status = status;
            NumberingSequenceType = numberingSequenceType;
            UpdateAudit(userId);
        }

        public string Prefix { get; private set; }
        public string Number { get; set; }
        public StatusEnum Status { get; private set; }
        public NumberingSequenceTypeEnum NumberingSequenceType { get; private set; }
    }
}
