using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    /// <summary>
    /// 指定的帧数内调用子节点
    /// </summary>
    public class DecoratorFrames : DecoratorNode
    {
        protected int m_start = 0;

        protected int m_frames = 0;

        public override void ParserProperty(string propertyName, string propertyValue)
        {
            if (propertyName == "start") m_start = int.Parse(propertyValue);
            else if (propertyName == "frames") m_frames = int.Parse(propertyValue);
        }

        protected override bool OnEnter(Agent pAgent)
        {
            base.OnEnter(pAgent);

            this.m_start = BTG.FrameSinceStartup;

            return (this.m_frames >= 0);
        }

        protected override EBTStatus OnDecorator(Agent agent, EBTStatus status)
        {
            if (BTG.FrameSinceStartup - this.m_start + 1 >= this.m_frames)
            {
                return EBTStatus.Success;
            }

            return EBTStatus.Running;
        }
    }
}
