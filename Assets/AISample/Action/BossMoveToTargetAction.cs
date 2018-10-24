using System;
using System.Collections.Generic;
using BTFrame;
using BTExtends;
using UnityEngine;

public class BossMoveToTargetAction : GameAction
{
    protected override EBTStatus OnExec(Agent agent, EBTStatus childStatus)
    {
        this.Owner.PlayAnimation("run");
        BaseGameObject target = agent.GetTreeValue("target") as BaseGameObject;

        float dis = Time.deltaTime * this.Owner.Speed;
        Vector3 dir = target.transform.position - this.Owner.transform.position;
        dir.Normalize();

        this.Owner.transform.position += dir * dis;
        this.Owner.SetForward(dir);

        return base.OnExec(agent, childStatus);
    }
}
