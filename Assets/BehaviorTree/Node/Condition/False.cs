using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    public class False : ConditionNode
    {
        public False()
        {
            this.iMaxChildCount = this.iMinChildCount = 0;
        }

        protected override EBTStatus OnExec(Agent pAgent, EBTStatus childStatus)
        {
            return EBTStatus.Failure;
        }
    }
}
