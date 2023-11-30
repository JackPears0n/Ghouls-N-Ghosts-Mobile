using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VultureScript : MonoBehaviour
{
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
    }

    // Update is called once per frame
    void Update()
    {
        playerInFOV = false;
        // Check sight and attack range
        playerInFOV  = Physics2D.OverlapCircle(transform.position, fOV, whatIsPlayer);
        

        if (playerInFOV)
        {
            Chase();
        }
        else
        {
            anim.Play("VIdle");
        }
    }

    public void Chase()
    {
        //play run anim
        anim.Play("VChase");

        if (player.transform.position.x > transform.position.x)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //play attack anim
        if (!alreadyAttacked)
        {
            if (collision.gameObject.tag == "Player")
            {
                anim.Play("VAttack");
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
}
