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

    }

    public override void HandleInput()
    {
    }

    public override void LogicUpdate()
    {
        ps.CheckForXInput();
    }

    public override void PhysicsUpdate()
    {

    }

    public override void Exit()
    {
    }
}
