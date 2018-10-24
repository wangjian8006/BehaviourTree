using System;
using System.Collections.Generic;
using UnityEngine;
using BTFrame;

public class MoveAction : GameAction
{
    protected override bool OnEnter(BTFrame.Agent agent)
    {
        base.OnEnter(agent);
        this.Owner.PlayAnimation("run");
        return true;
    }

    protected override EBTStatus OnExec(Agent agent, BTFrame.EBTStatus childStatus)
    {
        PlayerAgent playerAgent = agent as PlayerAgent;
        BTG.Assert(playerAgent != null, "agent is null.");

        if (playerAgent.CheckMove() == false) return EBTStatus.Failure;
        Vector3 vEndPos = (Vector3)playerAgent.GetTreeValue("MovePos");

        if (TryMoveEnd(ref vEndPos) == true)
        {
            //playerAgent.OnStand();
            return EBTStatus.Success;
        }
        return EBTStatus.Running;
    }

    protected override void OnLeave(Agent agent)
    {
        (agent as PlayerAgent).OnStand();
    }

    private bool TryMoveEnd(ref Vector3 vEndPos)
    {
        float dis = Time.deltaTime * this.Owner.Speed;
        float totalDis = Vector3.Distance(vEndPos, this.Owner.transform.position);
        if (totalDis < 0.0001) return true;
        Vector3 pos = Vector3.Lerp(this.Owner.transform.position, vEndPos, dis / totalDis);
        this.Owner.transform.position = pos;
        this.Owner.SetForward(vEndPos - this.Owner.transform.position);
        return false;
    }
}