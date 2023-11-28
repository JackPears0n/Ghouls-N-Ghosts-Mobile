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
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pCS = player.GetComponent<PlayerCombatScript>();

        //StartCoroutine(Spawning());
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
        anim.Play("SWalk");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //play attack anim
        if(!alreadyAttacked)
        {
            if (collision.gameObject.tag == "Player")
            {
                pCS.TakeDamage(1, gameObject);
                anim.Play("SAttack");
                alreadyAttacked = true;
            }

        }
        Invoke(nameof(resetAttack), attackCooldown);
    }

    public void resetAttack()

    {
        alreadyAttacked = false;
    }

    public IEnumerator Spawning()
    {
        //Play spawn anim

        yield return new WaitForSeconds(1);

        player = GameObject.FindGameObjectWithTag("Player");
        pCS = player.GetComponent<PlayerCombatScript>();
    }
}
