using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTFrame;

public class behaviortree_sample : MonoBehaviour
{
    protected BehaviourTree tree;

    protected EBTStatus treeStatus = EBTStatus.Invalid;

	// Use this for initialization
	void Start () {
        BTMapping.RegisterNodeType();
        BTMapping.RegisterAgentType();
        string content = Resources.Load<TextAsset>("boss_behaviour").text;
        tree = BTG.xmlGenaral.Parser(content);
	}
	
	// Update is called once per frame
	void Update () {
        if (treeStatus == EBTStatus.Success ||
            treeStatus == EBTStatus.Failure) return;
        treeStatus = tree.Tick();
        Debug.Log(treeStatus);
	}
}
