using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    public class MethodAction : ActionNode
    {
        protected BTActionsMethods method;

        public override void ParserProperty(string propertyName, string propertyValue)
        {
            if (propertyName == "method") method = BTG.GetMethods(propertyValue) as BTActionsMethods;
        }

        protected override EBTStatus OnExec(Agent agent, EBTStatus childStatus)
        {
            return this.method.method(agent);
        }
    }
}
