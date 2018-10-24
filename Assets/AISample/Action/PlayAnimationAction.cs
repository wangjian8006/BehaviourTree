using System;
using System.Collections.Generic;
using BTFrame;
using BTExtends;

public class PlayAnimationAction : GameAction
{
    protected string animName;

    protected string isLoop;

    public override void ParserProperty(string propertyName, string propertyValue)
    {
        base.ParserProperty(propertyName, propertyValue);
        if (propertyName == "name") this.animName = propertyValue;
        else if (propertyName == "loop") this.isLoop = propertyValue;
    }

    protected override bool OnEnter(Agent agent)
    {
        base.OnEnter(agent);
        if (isLoop == "false") Owner.PlayAnimation(animName, false);
        else Owner.PlayAnimation(animName);
        return true;
    }
}
