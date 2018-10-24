using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTFrame
{
    public abstract class BehaviourPoolNode
    {
        /// <summary>
        /// 是否被销毁
        /// </summary>
        protected bool m_bIsDestory = false;

        protected internal virtual void PoolInit()
        {

        }

        protected internal virtual bool Destory()
        {
            if (m_bIsDestory == true)
            {
                BTG.Error("Repet destory.");
                return false;
            }

            m_bIsDestory = true;
            return true;
        }
    }
}
