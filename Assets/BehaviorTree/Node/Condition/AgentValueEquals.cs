using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    public class AgentValueEquals : ConditionNode
    {
        protected string agentKey = "";

        protected string agentValue = "";

        public override void ParserProperty(string propertyName, string propertyValue)
        {
            if (propertyName == "agentKey") agentKey = propertyValue;
            else if (propertyName == "agentValue") agentValue = propertyValue;
        }

        public override bool Evaluate(Agent agent)
        {
            return agent.GetTreeValue(agentKey) == agentValue;
        }
    }
}
