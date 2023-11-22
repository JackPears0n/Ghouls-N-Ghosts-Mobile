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
    //Movement
    public float baseVel;
    public float vel;
    public int xInput, yInput;
    public Vector2 moveDirect;

    //Jumping
    public LayerMask groundLayerMask;
    public float rayLength;
    public bool isGrounded;
    public float jumpStrength;

    [Header("Player status")]
    public PlayerIdle pIdle;
    public PlayerRunning pRunning;
    public PlayerJumping pJumping;
    public PlayerFalling pFalling;

    [Header("State Mahchine")]
    public StateMachine sm;

    // Start is called before the first frame update
    void Start()
    {
        sm = gameObject.GetComponent<StateMachine>();
        rb = GetComponent<Rigidbody2D>();

        pIdle = new PlayerIdle(this, sm);
        pRunning = new PlayerRunning(this, sm);
        pJumping = new PlayerJumping(this, sm);
        pFalling = new PlayerFalling(this, sm);

        sm.Init(pIdle);
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = DoRayCollisionCheck();

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

    public bool DoRayCollisionCheck()
    {
        //cast a ray downward
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, rayLength, groundLayerMask);

        hit = Physics2D.Raycast(transform.position, -Vector2.up, rayLength, groundLayerMask);

        Debug.DrawRay(transform.position, -Vector2.up * rayLength, (hit.collider != null) ? Color.green : Color.white);

        return hit.collider != null;
    }

    #region State Checks
    public void CheckForXInput()
    {
        if (xInput != 0)
        {
            sm.ChangeState(pRunning);
        }
    }
    public void CheckForIdle()
    {
        if(isGrounded)
        {
            if(Input.touchCount == 0)
            {
                xInput = 0;
                yInput = 0;
                sm.ChangeState(pIdle);
            }
        }

    }
    public void CheckForJump()
    { 
        if (isGrounded)
        {
            sm.ChangeState(pJumping);
        }
    }
    public void CheckPlayerIsFalling()
    {
        if(rb.velocityY > 0)
        {
            if(!isGrounded)
            {
                sm.ChangeState(pFalling);
            }
        }
    }
    #endregion
}