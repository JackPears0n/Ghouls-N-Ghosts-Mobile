using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header ("Components")]
    public Rigidbody2D rb;
    public Animator anim;
    public PlayerHealth pH;
    public PlayerCombat pC;

    [Header("Variables")]
    public float baseVel;
    public float vel;
    public int xInput, yInput;
    public Vector2 moveDirect;

    [Header("Player status")]
    public PlayerIdle pIdle;
    public PlayerRunning pRunning;

    [Header("State Mahchine")]
    public StateMachine sm;

    // Start is called before the first frame update
    void Start()
    {
        sm = gameObject.GetComponent<StateMachine>();
        rb = GetComponent<Rigidbody2D>();

        pIdle = new PlayerIdle(this, sm);
        pRunning = new PlayerRunning(this, sm);

        sm.Init(pIdle);
    }

    // Update is called once per frame
    void Update()
    {
        sm.CurrentState.HandleInput();
        sm.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        sm.CurrentState.PhysicsUpdate();
    }

    public void AddXInput(int input)
    {
        xInput = input;
    }

    public void AddYInput(int input)
    {
        yInput = input;
    }

    public void CheckForXInput()
    {
        if (xInput != 0)
        {
            sm.ChangeState(pRunning);
        }
    }
    public void CheckForIdle()
    {
        if(Input.touchCount == 0)
        {
            sm.ChangeState(pIdle);
        }
    }
}
