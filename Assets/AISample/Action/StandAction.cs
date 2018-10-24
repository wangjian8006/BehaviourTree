using System;
using System.Collections.Generic;
using BTFrame;
using BTExtends;

public class StandAction : GameAction
{
    protected override bool OnEnter(Agent agent)
    {
        base.OnEnter(agent);
        this.Owner.PlayAnimation("stand1");
        return true;
    }
}