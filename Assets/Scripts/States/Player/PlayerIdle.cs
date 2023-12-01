using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : State
{
    public PlayerIdle(PlayerScript player, StateMachine sm) : base(player, sm)
    {
    }
    public override void Enter()
    {
        ps.yInput = 0;
        ps.col.size = new Vector2(0.21f, 0.4f);
        string animClip = ps.animC[0].name;
        ps.anim.Play(animClip);
    }

    public override void HandleInput()
    {
    }

    public override void LogicUpdate()
    {
        ps.CheckForCrouchInput();
        ps.CheckForClimbInput();
        ps.CheckForXInput();
    }

    public override void PhysicsUpdate()
    {

    }

    public override void Exit()
    {
    }
}
