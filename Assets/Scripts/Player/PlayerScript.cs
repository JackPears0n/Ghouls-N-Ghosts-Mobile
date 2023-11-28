using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject playerParent;

    [Header ("Components")]
    public Rigidbody2D rb;
    public CapsuleCollider2D col;

    public PlayerCombatScript pC;

    public GameObject[] sprites;
    public GameObject currentSprite;
    public Animator anim;
    public AnimationClip[] animC;

    [Header("Variables")]
    //Movement
    public float baseVel, vel;
    public int xInput, yInput;
    public Vector2 moveDirect;
    public bool crouch = false;

    //Jumping
    public LayerMask groundLayerMask;
    public float rayLength, jumpStrength;
    public bool isGrounded;

    [Header("Player status")]
    public PlayerIdle pIdle;
    public PlayerRunning pRunning;
    public PlayerJumping pJumping;
    public PlayerFalling pFalling;
    public PlayerCrouch pCrouch;

    [Header("State Mahchine")]
    public StateMachine sm;

    // Start is called before the first frame update
    void Start()
    {
        sm = gameObject.GetComponent<StateMachine>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();

        pC = GetComponent<PlayerCombatScript>();

        pIdle = new PlayerIdle(this, sm);
        pRunning = new PlayerRunning(this, sm);
        pJumping = new PlayerJumping(this, sm);
        pFalling = new PlayerFalling(this, sm);
        pCrouch = new PlayerCrouch(this, sm);

        sm.Init(pIdle);
    }

    // Update is called once per frame
    void Update()
    {
        anim = currentSprite.GetComponent<Animator>();
        SpriteScript sPS = currentSprite.GetComponent<SpriteScript>();
        animC = sPS.anims;

        isGrounded = GroundCheck();

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
    public void ChangeCrouch()
    {
        if (crouch)
        {
            crouch = false;
        }
        else
        {
            crouch = true;
        }
    }

    public bool GroundCheck()
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

    public void CheckForCrouchInput()
    {
        if (crouch)
        {
            sm.ChangeState(pCrouch);
        }
    }

    public void CheckForIdle()
    {
        if(isGrounded)
        {
            if(Input.touchCount == 0)
            {
                if (!crouch)
                {
                    xInput = 0;
                    yInput = 0;
                    sm.ChangeState(pIdle);
                }
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
