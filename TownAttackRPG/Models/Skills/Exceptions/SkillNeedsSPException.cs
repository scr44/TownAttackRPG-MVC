using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TownAttackRPG.Models.Skills
{
    public class SkillNeedsSPException : Exception
    {
        public override IDictionary Data => base.Data;

        public override string HelpLink { get => base.HelpLink; set => base.HelpLink = value; }

        public override string Message => base.Message;

        public override string Source { get => base.Source; set => base.Source = value; }

        public override string StackTrace => base.StackTrace;
    }
}
