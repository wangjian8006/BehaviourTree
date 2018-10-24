using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    public class AgentValueExsists : ConditionNode
    {
        protected string agentKey = "";

        public override void ParserProperty(string propertyName, string propertyValue)
        {
            if (propertyName == "agentKey") agentKey = propertyValue;
        }

        public override bool Evaluate(Agent agent)
        {
            return agent.ContairsTreeKey(agentKey);
        }
    }
}
