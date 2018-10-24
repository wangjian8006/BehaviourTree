using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    /// <summary>
    /// 持续时间内调用节点
    /// 单位秒
    /// </summary>
    public class DecoratorTime : DecoratorNode
    {
        protected float m_startTime;

        protected float m_totalTime;

        public override void ParserProperty(string propertyName, string propertyValue)
        {
            if (propertyName == "start") m_startTime = float.Parse(propertyValue);
            else if (propertyName == "total") m_totalTime = float.Parse(propertyValue);
        }

        protected override bool OnEnter(Agent pAgent)
        {
            base.OnEnter(pAgent);

            this.m_startTime = BTG.NowTime;

            return (this.m_totalTime >= 0);
        }

        protected override EBTStatus OnDecorator(Agent agent, EBTStatus status)
        {
            if (BTG.NowTime - this.m_startTime >= this.m_totalTime)
            {
                return EBTStatus.Success;
            }

            return EBTStatus.Running;
        }
    }
}