using System;
using System.Collections.Generic;
using BTExtends;
using BTFrame;


public enum ActionType
{
    Move,
    Dead,
    Spell,
    Stand
}

public class GameAction : ActionNode
{
    protected BaseGameObject Owner;

    protected override bool OnEnter(Agent agent)
    {
        Owner = agent.GetTreeValue("owner") as BaseGameObject;
        return base.OnEnter(agent);
    }
}