using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaselScript : MonoBehaviour
{
    public float speed, frequency, magnitude;

    public Vector3 pos;

    public bool canFunction = false;
    public bool paused = false;

    public GameObject player;
    public PlayerCombatScript pCS;

    [Header("Attacking")]
    public float attackCooldown;
    bool alreadyAttacked = false;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        pCS = player.GetComponent<PlayerCombatScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canFunction)
        {
            Fly();

            StartCoroutine(ChangePause());
        }
    }

    public void Fly()
    {
        pos -= transform.right * Time.deltaTime * speed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //play attack anim
        if (!alreadyAttacked && canFunction)
        {
            if (collision.gameObject.tag == "Player")
            {
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

    public IEnumerator ChangePause()
    {
        if (!paused)
        {
            paused = true;
        }
        else
        {
            paused = false;
        }

        yield return new WaitForSeconds(2);
    }
}
