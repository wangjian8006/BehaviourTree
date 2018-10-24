using System;
using System.Collections.Generic;
using MiniXml;
using System.Security;

namespace BTFrame
{
    /// <summary>
    /// 数据解析器
    /// </summary>
    public class BehaviourXmlGenaral
    {
        protected bool CheckChildCount(BehaviourTreeNode node)
        {
            for (int i = 0; i < node.ChildCount; ++i)
            {
                BehaviourTreeNode child = node.GetChild(i);
                if (child.iMinChildCount >= 0 &&
                    child.iMinChildCount > child.ChildCount)
                {
                    return false;
                }
                if (child.iMaxChildCount >= 0 &&
                    child.iMaxChildCount < child.ChildCount)
                {
                    return false;
                }
                if (CheckChildCount(child) == false) return false;
            }
            return true;
        }

        protected bool CheckSafe(BehaviourTree tree)
        {
            if (CheckChildCount(tree.Root) == false) return false;
            return true;
        }

        protected void ParserNode(BehaviourTree tree, BehaviourNode parent, SecurityElement parentXmlDoc)
        {
            if (parentXmlDoc.Children == null) return;
            foreach (SecurityElement c in parentXmlDoc.Children)
            {
                if (c.Tag == "node"){
                    string id = c.Attribute("id");
                    string className = c.Attribute("class");
                    //BTG.Log(c.Attribute("class") + "\t" + id + "\t");
                    BehaviourNode childNode = BTG.GetNode(className);
                    if (childNode == null)
                    {
                        BTG.Error("Can't found class " + c.Attribute("class"));
                        return;
                    }
                    if (className == null)
                    {
                        BTG.Error("Can't found class empty.");
                        return;
                    }
                    childNode.ID = int.Parse(id);
                    childNode.Name = c.Attribute("name");
                    childNode.BindTree(tree);
                    parent.AddChild(childNode);
                    ParserNode(tree, childNode, c);
                }
                else if (c.Tag == "property")
                {
                    foreach (string propName in c.Attributes.Keys)
                    {
                        parent.ParserProperty(propName, (string)c.Attributes[propName]);
                        break;
                    }
                }
            }
        }

        public BehaviourTree Parser(string xmlContent)
        {
            SecurityParser xmlDoc = new SecurityParser();
            xmlDoc.LoadXml(xmlContent);

            SecurityElement behaviorNode = xmlDoc.ToXml();
            if (behaviorNode.Tag != "behaviour") return null;

            string name = behaviorNode.Attribute("name");
            string agentType = behaviorNode.Attribute("agent");
            int version = int.Parse(behaviorNode.Attribute("version"));

            BehaviourNode root = BTG.GetNode("EntryNode");
            root.ID = 0;
            BehaviourTree tree = new BehaviourTree(name, agentType, version, root);

            root.BindTree(tree);

            ParserNode(tree, root, behaviorNode);

            if (CheckSafe(tree) == false)
            {
                BTG.Error("The tree is not safe.");
                return null;
            }

            return tree;
        }
    }
}
