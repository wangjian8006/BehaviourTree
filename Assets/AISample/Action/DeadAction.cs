using System;
using System.Collections.Generic;
using BTFrame;

public class DeadAction : GameAction
{
    protected override bool OnEnter(Agent agent)
    {
        base.OnEnter(agent);
        this.Owner.PlayAnimation("death", false);
        return true;
    }

    protected override EBTStatus OnExec(Agent agent, EBTStatus childStatus)
    {
        return EBTStatus.Running;
    }
}