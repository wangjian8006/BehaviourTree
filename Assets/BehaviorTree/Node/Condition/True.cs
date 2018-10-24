using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    public class True : ConditionNode
    {
        public True()
        {
            this.iMaxChildCount = this.iMinChildCount = 0;
        }

        protected override EBTStatus OnExec(Agent pAgent, EBTStatus childStatus)
        {
            return EBTStatus.Success;
        }
    }
}
