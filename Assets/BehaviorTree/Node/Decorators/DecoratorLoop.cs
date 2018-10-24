using System;
using System.Collections.Generic;
using BTFrame;

namespace BTExtends
{
    /// <summary>
    /// 1帧之类循环完
    /// 循环制定次数，-1无限循环
    /// </summary>
    public class DecoratorLoop : DecoratorNode
    {
        /// <summary>
        /// 循环次数
        /// </summary>
        protected int m_loops = 0;

        /// <summary>
        /// 是否一帧运行完毕
        /// </summary>
        protected bool m_bDoneWithinFrame = false;

        public override void ParserProperty(string propertyName, string propertyValue)
        {
            if (propertyName == "inframe") m_bDoneWithinFrame = bool.Parse(propertyValue);
            else if (propertyName == "loops") m_loops = int.Parse(propertyValue);
        }

        /// <summary>
        /// 一帧执行完，用这个
        /// </summary>
        /// <param name="pAgent"></param>
        /// <param name="childStatus"></param>
        /// <returns></returns>
        protected override EBTStatus OnExec(Agent pAgent, EBTStatus childStatus)
        {
            if (m_bDoneWithinFrame)
            {
                EBTStatus status = EBTStatus.Invalid;

                for (int i = 0; i < this.m_loops; ++i)
                {
                    status = this.Tick(pAgent, childStatus);

                    if (m_bDecorateWhenChildEnds == true)
                    {
                        while (status == EBTStatus.Running)
                        {
                            status = base.OnExec(pAgent, childStatus);
                        }
                    }

                    if (status == EBTStatus.Failure)
                    {
                        return EBTStatus.Failure;
                    }
                }

                return EBTStatus.Success;
            }

            return base.OnExec(pAgent, childStatus);

        }

        /// <summary>
        /// 分帧执行用这个
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        protected override EBTStatus OnDecorator(Agent agent, EBTStatus status)
        {
            if (this.m_loops > 0)
            {
                this.m_loops--;

                if (this.m_loops == 0)
                {
                    return EBTStatus.Success;
                }

                return EBTStatus.Running;
            }

            if (this.m_loops == -1)
            {
                return EBTStatus.Running;
            }

            return EBTStatus.Success;
        }
    }
}
