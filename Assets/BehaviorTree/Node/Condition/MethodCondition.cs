using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    public class MethodCondition : ConditionNode
    {
        protected BTEvaluatesMethods method;

        public MethodCondition()
        {
            this.iMinChildCount = this.iMaxChildCount = 0;
        }

        public override void ParserProperty(string propertyName, string propertyValue)
        {
            if (propertyName == "method") method = BTG.GetMethods(propertyValue) as BTEvaluatesMethods;
        }

        public override bool Evaluate(Agent agent)
        {
            return this.method.method(agent);
        }
    }
}
