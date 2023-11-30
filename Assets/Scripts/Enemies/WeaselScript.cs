using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaselScript : MonoBehaviour
{
    private Vector3 pos;
    [SerializeField] private float frequency = 3F;
    [SerializeField] private float magnitude = 3F;
    [SerializeField] private float offset = 0F;

    public bool canFunction;
    public Rigidbody2D rb;

    public bool pause = false;

    public EnemyHPScript eHPS;

    public GameObject enemy;

    public GameObject player;
    public PlayerCombatScript pCS;

    public float speed;
    public Animator anim;

    public float fOV;
    public bool playerInFOV = false;
    public LayerMask whatIsPlayer;

    [Header("Attacking")]
    public float attackCooldown;
    bool alreadyAttacked = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pCS = player.GetComponent<PlayerCombatScript>();
        eHPS = GetComponent<EnemyHPScript>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canFunction)
        {
            playerInFOV = false;
            // Check sight and attack range
            playerInFOV = Physics2D.OverlapCircle(transform.position, fOV, whatIsPlayer);

            if (playerInFOV && !pause)
            {
                eHPS.invulnerable = true;
                Move();
            }
            else if (pause)
            {
                rb.velocity = new Vector2(0, 0);
                eHPS.invulnerable = false;
            }
            else
            {
                eHPS.invulnerable = true;
            }
        }

    }

    public void Move()
    {
        //play run anim

        if (player.transform.position.x > transform.position.x)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        transform.rotation = Quaternion.FromToRotation(transform.position, -player.transform.position);
        pos -= transform.right * Time.deltaTime * speed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }

    public IEnumerator Pause()
    {
        if (eHPS.invulnerable)
        {
            eHPS.invulnerable = false;
        }
        else
        {
            eHPS.invulnerable = true;
        }

        yield return new WaitForSeconds(3);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (!alreadyAttacked)
        {
            if (collision.gameObject.tag == "Player")
            {
                 //play attack anim
                pCS.TakeDamage(1, gameObject);
                alreadyAttacked = true;
            }

        }
        Invoke(nameof(resetAttack), attackCooldown);
    }

    public void resetAttack()

    {
        alreadyAttacked = false;
    }

    public IEnumerator Attack()
    {
        yield return new WaitForSeconds(1);
    }
}
