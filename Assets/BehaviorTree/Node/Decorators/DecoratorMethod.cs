using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    public class DecoratorMethod : DecoratorNode
    {
        protected BTDecoratorsMethods method;

        public override void ParserProperty(string propertyName, string propertyValue)
        {
            if (propertyName == "method") method = BTG.GetMethods(propertyValue) as BTDecoratorsMethods;
        }

        protected override EBTStatus OnDecorator(Agent agent, EBTStatus childStatus)
        {
            return this.method.method(agent, childStatus);
        }
    }
}
