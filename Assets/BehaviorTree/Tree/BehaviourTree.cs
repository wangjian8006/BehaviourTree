using System;
using System.Collections.Generic;
using UnityEngine;

namespace BTFrame
{
    /// <summary>
    /// 行为树驱动器
    /// </summary>
    public class BehaviourTree
    {
        /// <summary>
        /// 根节点
        /// </summary>
        protected BehaviourTreeNode m_root;

        /// <summary>
        /// 当前节点
        /// </summary>
        protected BehaviourTreeNode m_nowNode = null;

        /// <summary>
        /// 是否正在运行
        /// </summary>
        protected bool m_bRunning = false;

        /// <summary>
        /// 树的名字
        /// </summary>
        protected string m_sTreeName = "";

        /// <summary>
        /// BlackBoard
        /// </summary>
        protected Agent m_agent;

        /// <summary>
        /// 版本
        /// </summary>
        protected int m_version = 0;

        protected bool canSetNowNode = true;

        /// <summary>
        /// 结束的状态
        /// </summary>
        protected EBTStatus m_endStatus = EBTStatus.Invalid;
        
        /// <summary>
        /// 树的名字
        /// </summary>
        public string TreeName { get { return m_sTreeName; } }

        /// <summary>
        /// 获得根节点
        /// </summary>
        /// <returns></returns>
        public BehaviourTreeNode Root { get { return m_root; } }

        /// <summary>
        /// 获得版本号
        /// </summary>
        public int Version { get { return m_version; } }

        /// <summary>
        /// BlackBoard
        /// </summary>
        public Agent Agent { get { return m_agent; } }

        public BehaviourTree(string name, string agentType, int version, BehaviourNode root)
        {
            m_sTreeName = name;
            m_version = version;
            m_agent = BTG.GetAgent(agentType);
            m_root = root;
            SetNowNode(m_root);
        }

        /// <summary>
        /// 设置结束的状态
        /// </summary>
        /// <param name="s"></param>
        public void SetEndStatus(EBTStatus s)
        {
            this.m_endStatus = s;
        }

        /// <summary>
        /// 设置当前节点
        /// </summary>
        /// <param name="node"></param>
        public void SetNowNode(BehaviourTreeNode node)
        {
            if (canSetNowNode == false) return;
            canSetNowNode = false;
            this.m_nowNode = node;
            //BTG.Log("SetNowNode" + (node as BehaviourNode).ID);
        }

        /// <summary>
        /// 整棵树事件
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="eventParams"></param>
        public void OnTreeEvent(string eventName, object eventParams = null)
        {
            this.Root.OnTreeEvent(this.Agent, eventName, eventParams);
        }

        /// <summary>
        /// 对某个节点的事件
        /// </summary>
        /// <param name="childID"></param>
        /// <param name="eventName"></param>
        /// <param name="eventParam"></param>
        public void OnEvent(int childID, string eventName, object eventParam)
        {
            BehaviourTreeNode node = (this.Root as BehaviourNode).GetChildByID(childID);
            if (node != null)
            {
                node.OnEvent(this.Agent, eventName, eventParam);
            }
        }

        /// <summary>
        /// 帧更新
        /// </summary>
        public EBTStatus Tick()
        {
            canSetNowNode = true;
            if (m_nowNode == null) return EBTStatus.Success;

            EBTStatus childStatus = EBTStatus.Running;

            childStatus = m_nowNode.Tick(m_agent, childStatus);
            if (this.m_endStatus != EBTStatus.Invalid) return this.m_endStatus;

            if (childStatus != EBTStatus.Running)       //回溯
            {
                BehaviourTreeNode parentBranch = this.m_nowNode.GetParent();

                while (parentBranch != null)
                {
                    childStatus = parentBranch.Tick(m_agent, childStatus);
                    if (childStatus == EBTStatus.Running)
                    {
                        return EBTStatus.Running;
                    }
                    parentBranch = parentBranch.GetParent();
                }
            }

            return childStatus;
        }

        /// <summary>
        /// 当行为树执行结束
        /// </summary>
        /// <param name="s"></param>
        protected void ExecuteEnd(EBTStatus s)
        {
            m_bRunning = false;
        }

        /// <summary>
        /// 行为树销毁
        /// </summary>
        public void OnTreeDestory()
        {
            m_root.Destory();
            BTG.DestoryAgent(this.m_agent);
        }
    }
}
