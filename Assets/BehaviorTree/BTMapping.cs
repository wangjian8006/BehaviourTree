using System;
using System.Collections.Generic;
using BTFrame;
using BTExtends;

/// <summary>
/// 将所有类型注册好
/// 避免反射
/// </summary>
public class BTMapping
{
    private static bool isRegisterAgent = false;

    private static bool isRegisterNode = false;

    public static void RegisterNodeType()
    {
        if (isRegisterNode == true) return;
        isRegisterNode = true;
        BTG.RegisterNode<EntryNode>("EntryNode", () => new EntryNode());

        //Decorators
        BTG.RegisterNode<DecoratorAlwaysFailure>("DecoratorAlwaysFailure", () => new DecoratorAlwaysFailure());
        BTG.RegisterNode<DecoratorAlwaysRunning>("DecoratorAlwaysRunning", () => new DecoratorAlwaysRunning());
        BTG.RegisterNode<DecoratorAlwaysSuccess>("DecoratorAlwaysSuccess", () => new DecoratorAlwaysSuccess());
        BTG.RegisterNode<DecoratorCount>("DecoratorCount", () => new DecoratorCount());
        BTG.RegisterNode<DecoratorFailureUntil>("DecoratorFailureUntil", () => new DecoratorFailureUntil());
        BTG.RegisterNode<DecoratorFrames>("DecoratorFrames", () => new DecoratorFrames());
        BTG.RegisterNode<DecoratorLog>("DecoratorLog", () => new DecoratorLog());
        BTG.RegisterNode<DecoratorLoop>("DecoratorLoop", () => new DecoratorLoop());
        BTG.RegisterNode<DecoratorLoopUntil>("DecoratorLoopUntil", () => new DecoratorLoopUntil());
        BTG.RegisterNode<DecoratorNot>("DecoratorNot", () => new DecoratorNot());
        BTG.RegisterNode<DecoratorSuccessUntil>("DecoratorSuccessUntil", () => new DecoratorSuccessUntil());
        BTG.RegisterNode<DecoratorTime>("DecoratorTime", () => new DecoratorTime());
        BTG.RegisterNode<DecoratorWeight>("DecoratorWeight", () => new DecoratorWeight());
        BTG.RegisterNode<DecoratorMethod>("DecoratorMethod", () => new DecoratorMethod());

        //Condition
        BTG.RegisterNode<And>("And", () => new And());
        BTG.RegisterNode<False>("False", () => new False());
        BTG.RegisterNode<Or>("Or", () => new Or());
        BTG.RegisterNode<True>("True", () => new True());
        BTG.RegisterNode<MethodCondition>("MethodCondition", () => new MethodCondition());
        BTG.RegisterNode<AgentValueEquals>("AgentValueEquals", () => new AgentValueEquals());
        BTG.RegisterNode<AgentValueExsists>("AgentValueExsists", () => new AgentValueExsists());
        

        //Composites
        BTG.RegisterNode<IfElse>("IfElse", () => new IfElse());
        BTG.RegisterNode<Parallel>("Parallel", () => new Parallel());
        BTG.RegisterNode<Selector>("Selector", () => new Selector());
        BTG.RegisterNode<SelectorLoop>("SelectorLoop", () => new SelectorLoop());
        BTG.RegisterNode<SelectorProbability>("SelectorProbability", () => new SelectorProbability());
        BTG.RegisterNode<SelectorStochastic>("SelectorStochastic", () => new SelectorStochastic());
        BTG.RegisterNode<Sequence>("Sequence", () => new Sequence());
        BTG.RegisterNode<SequenceStochastic>("SequenceStochastic", () => new SequenceStochastic());
        BTG.RegisterNode<WithPrecondition>("WithPrecondition", () => new WithPrecondition());

        //Action
        BTG.RegisterNode<Assignment>("Assignment", () => new Assignment());
        BTG.RegisterNode<End>("End", () => new End());
        BTG.RegisterNode<Noop>("Noop", () => new Noop());
        BTG.RegisterNode<WaitForSignal>("WaitForSignal", () => new WaitForSignal());
        BTG.RegisterNode<WaitFrames>("WaitFrames", () => new WaitFrames());
        BTG.RegisterNode<WaitTime>("WaitTime", () => new WaitTime());
        BTG.RegisterNode<MethodAction>("MethodAction", () => new MethodAction());
        BTG.RegisterNode<AgentAssignment>("AgentAssignment", () => new AgentAssignment());

        //Custom
        BTG.RegisterNode<DeadAction>("DeadAction", () => new DeadAction());
        BTG.RegisterNode<MoveAction>("MoveAction", () => new MoveAction());
        BTG.RegisterNode<SpellAction>("SpellAction", () => new SpellAction());
        BTG.RegisterNode<StandAction>("StandAction", () => new StandAction());
        BTG.RegisterNode<BossSpellAction>("BossSpellAction", () => new BossSpellAction());
        BTG.RegisterNode<BossMoveToTargetAction>("BossMoveToTargetAction", () => new BossMoveToTargetAction());
        BTG.RegisterNode<PlayAnimationAction>("PlayAnimationAction", () => new PlayAnimationAction());
    }

    public static void RegisterAgentType()
    {
        if (isRegisterAgent == true) return;
        isRegisterAgent = true;

        //Custom
        BTG.RegisterAgent("PlayerAgent", () => new PlayerAgent());
        BTG.RegisterAgent("BossAgent", () => new BossAgent());
    }
}
