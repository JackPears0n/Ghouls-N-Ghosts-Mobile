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
            //Flips player
            ps.playerParent.transform.rotation = new Quaternion(0, 0, 0, 0);

            //Sets the velocity to go right
            ps.vel = ps.baseVel;
        }
        else if (ps.xInput < 0)
        {
            //Flips player
            ps.playerParent.transform.rotation = new Quaternion(0, 180, 0, 0);

            //Sets the velocity to go left
            ps.vel = ps.baseVel * -1;
        }

        //Play the run animation
        string animClip = ps.animC[1].name;
        ps.anim.Play(animClip);

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
