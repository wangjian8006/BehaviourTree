using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTFrame
{
    /// <summary>
    /// 用代码生成一颗行为树
    /// 所有接口调用这个
    /// </summary>
    public class BehaviourCodeGenaral
    {
        /// <summary>
        /// 创建一棵树
        /// </summary>
        /// <param name="treeName"></param>
        /// <param name="agentType"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public BehaviourTree CreateTree(string treeName, string agentType, int version)
        {
            BehaviourNode root = BTG.GetNode("EntryNode");
            BehaviourTree tree = new BehaviourTree(treeName, agentType, version, root);
            return tree;
        }

        /// <summary>
        /// 添加一个孩子节点到树
        /// </summary>
        /// <param name="classType"></param>
        /// <param name="nodeName"></param>
        /// <param name="nodeID"></param>
        public BehaviourNode AddChildToTree(BehaviourNode parentNode, string classType, string nodeName, int nodeID)
        {
            BehaviourNode childNode = BTG.GetNode(classType);
            childNode.ID = nodeID;
            childNode.Name = nodeName;
            parentNode.AddChild(childNode);
            return childNode;
        }

        /// <summary>
        /// 设置节点的属性
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public void SetNodeProperty(BehaviourNode node, string propertyName, string value)
        {
            node.ParserProperty(propertyName, value);
        }
    }
}
