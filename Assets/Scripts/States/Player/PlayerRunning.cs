using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunning : State
{
    public PlayerRunning(PlayerScript player, StateMachine sm) : base(player, sm)
    {
    }
    public override void Enter()
    {
        if (ps.xInput > 0)
        {
            ps.vel = ps.baseVel;
        }
        else if (ps.xInput < 0)
        {
            //Flip player

            ps.vel = ps.baseVel * -1;
        }

        //Play the run animation

    }

    public override void HandleInput()
    {
    }

    public override void LogicUpdate()
    {
        ps.CheckForIdle();
    }

    public override void PhysicsUpdate()
    {
        Vector2 moveDirect = new Vector2(ps.vel, 0);
        ps.rb.AddForce(moveDirect.normalized * ps.baseVel * 10f, ForceMode2D.Force);
    }

    public override void Exit()
    {
    }
}
