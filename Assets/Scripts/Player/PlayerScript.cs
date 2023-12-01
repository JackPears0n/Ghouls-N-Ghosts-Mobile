using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public GameObject playerParent;
    public Button jumpB;
    public GameObject jumpBHolder;

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

    //Ladders
    public float vertical;
    public bool isLadder;
    public bool isClimbing;

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
    public PlayerClimbing pClimbing;

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
        pClimbing = new PlayerClimbing(this, sm);

        sm.Init(pIdle);
    }

    // Update is called once per frame
    void Update()
    {
        anim = currentSprite.GetComponent<Animator>();
        SpriteScript sPS = currentSprite.GetComponent<SpriteScript>();
        animC = sPS.anims;

        isGrounded = GroundCheck();

        if (isLadder && Mathf.Abs(yInput) > 0F)
        {
            isClimbing = true;
        }
        if (!isClimbing)
        {
            rb.gravityScale = 1;
        }

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
        if (!crouch && GroundCheck())
        {
            crouch = true;         
        }
        else
        {
            crouch = false;
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            print("Touching ladder");
            isLadder = true;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
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
            jumpBHolder.SetActive(false);
            sm.ChangeState(pCrouch);
        }
        else
        {
            jumpBHolder.SetActive(true);
            sm.ChangeState(sm.CurrentState);
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
                    jumpBHolder.SetActive(true); xInput = 0;
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

    public void CheckForClimbInput()
    {
        if (isClimbing)
        {
            sm.ChangeState(pClimbing);
        }
    }
    #endregion
}
