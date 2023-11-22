using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFalling : State
{
    public PlayerFalling(PlayerScript player, StateMachine sm) : base(player, sm)
    {
    }
    public override void Enter()
    {
        ps.xInput = 0;
        ps.yInput = 0;
        ps.vel = 0;
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
        if (ps.xInput > 0)
        {
            ps.vel = ps.baseVel;
        }
        else if (ps.xInput < 0)
        {
            //Flip player

            ps.vel = ps.baseVel * -1;
        }

        Vector2 moveDirect = new Vector2(ps.vel, 0);
        ps.rb.AddForce(moveDirect.normalized * ps.baseVel * 5f, ForceMode2D.Force);
    }

    public override void Exit()
    {
    }
}
