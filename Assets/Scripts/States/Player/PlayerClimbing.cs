using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbing : State
{
    public PlayerClimbing(PlayerScript player, StateMachine sm) : base(player, sm)
    {
    }
    public override void Enter()
    {
        ps.anim.Play(ps.animC[0].name);
    }

    public override void HandleInput()
    {
    }

    public override void LogicUpdate()
    {
        if (!ps.isClimbing)
        {
            ps.CheckPlayerIsFalling();
        }

        ps.CheckForIdle();
    }

    public override void PhysicsUpdate()
    {
        if (ps.isClimbing)
        {
            ps.rb.gravityScale = 0;
            ps.rb.velocity = new Vector2 (ps.rb.velocity.x, ps.vertical * ps.vel);
        }
        else
        {
            ps.rb.gravityScale = 1;
        }

        /*
        Vector2 moveDirect = new Vector2(1, 0);
        ps.rb.AddForce(moveDirect.normalized * ps.baseVel * 10f, ForceMode2D.Force);*/
    }

    public override void Exit()
    {
    }
}
