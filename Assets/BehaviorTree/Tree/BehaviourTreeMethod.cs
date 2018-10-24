using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTFrame
{
    public delegate bool EvaluatesMethods(Agent agent);

    public delegate EBTStatus ActionsMethods(Agent agent);

    public delegate EBTStatus DecoratorsMethods(Agent agent, EBTStatus childStatus);

    public abstract class BTBaseMethod
    {
        
    }

    public class BTEvaluatesMethods : BTBaseMethod
    {
        public EvaluatesMethods method;

        public BTEvaluatesMethods(EvaluatesMethods method)
        {
            this.method = method;
        }
    }

    public class BTDecoratorsMethods : BTBaseMethod
    {
        public DecoratorsMethods method;

        public BTDecoratorsMethods(DecoratorsMethods method)
        {
            this.method = method;
        }
    }

    public class BTActionsMethods : BTBaseMethod
    {
        public ActionsMethods method;

        public BTActionsMethods(ActionsMethods method)
        {
            this.method = method;
        }
    }
}
