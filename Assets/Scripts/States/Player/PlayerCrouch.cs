using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : State
{
    public PlayerCrouch(PlayerScript player, StateMachine sm) : base(player, sm)
    {
    }
    public override void Enter()
    {
        ps.col.size = new Vector2(0.21f, 0.2f);

        string animClip = ps.animC[8].name;
        ps.anim.Play(animClip);
    }

    public override void HandleInput()
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
    }

    public override void LogicUpdate()
    {
        ps.CheckForIdle();
    }

    public override void PhysicsUpdate()
    {
       
    }

    public override void Exit()
    {
    }
}
