using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumping : State
{
    public PlayerJumping(PlayerScript player, StateMachine sm) : base(player, sm)
    {
    }
    public override void Enter()
    {
        //Play jump animation

        Vector2 moveDirect = new Vector2(0, 1);
        ps.rb.AddForce(moveDirect.normalized * ps.jumpStrength * 10f, ForceMode2D.Force);
    }

    public override void HandleInput()
    {
    }

    public override void LogicUpdate()
    {
        ps.CheckPlayerIsFalling();
    }

    public override void PhysicsUpdate()
    {

    }

    public override void Exit()
    {
    }
}
