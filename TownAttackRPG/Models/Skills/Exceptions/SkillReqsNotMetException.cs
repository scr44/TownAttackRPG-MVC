using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TownAttackRPG.Models.Skills
{
    public class SkillReqsNotMetException : Exception
    {
        public SkillReqsNotMetException()
        {
        }

        public SkillReqsNotMetException(string message) : base(message)
        {
        }

        public SkillReqsNotMetException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SkillReqsNotMetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
