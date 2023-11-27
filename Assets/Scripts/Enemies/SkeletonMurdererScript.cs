using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMurdererScript : MonoBehaviour
{

    public GameObject enemy;

    public GameObject player;
    public PlayerCombatScript pCS;

    public float speed;
    public Animator anim;

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
        Chase();
    }

    public void Chase()
    {
        //play run anim
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //play attack anim
        if(!alreadyAttacked)
        {
            if (collision.gameObject.tag == "Player")
            pCS.TakeDamage(1, gameObject);
            alreadyAttacked = true;
        }
        Invoke(nameof(resetAttack), attackCooldown);
    }

    public void resetAttack()

    {
        alreadyAttacked = false;
    }
}
