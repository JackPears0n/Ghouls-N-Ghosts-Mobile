using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected PlayerScript ps;

    protected StateMachine sm;
    
    protected State (PlayerScript ps, StateMachine sm)
    {
        this.ps = ps;
        this.sm = sm;
    }

    #region Common Methods
    //Activates when state is entered
    public virtual void Enter()
    {
        Debug.Log("This is base.enter");
    }

    //Handles all the inputs
    public virtual void HandleInput()
    {
    }

    //Updates the current logic, i.e. CurrentState, ect.
    public virtual void LogicUpdate()
    {
    }

    //Does any physics based updates
    public virtual void PhysicsUpdate()
    {
    }

    //Activates when state is exited
    public virtual void Exit()
    {
    }
    #endregion
}
