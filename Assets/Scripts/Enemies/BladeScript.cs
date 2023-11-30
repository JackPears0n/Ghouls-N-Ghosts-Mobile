using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeScript : MonoBehaviour
{
    public GameObject blade;
    public GameObject topPos, botPos;
    public Rigidbody2D rb;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y == topPos.transform.position.y || transform.position.y <= topPos.transform.position.y && transform.position.y > botPos.transform.position.y)
        {
            transform.position = Vector2.MoveTowards(transform.position, botPos.transform.position, 1 * Time.deltaTime);
        }

        else if (transform.position.y == botPos.transform.position.y)
        {
            blade.transform.position = botPos.transform.position;
        }

        /*
        else if (transform.position.y <= topPos.transform.position.y && transform.position.y > botPos.transform.position.y)
        {
            transform.position = Vector2.MoveTowards(transform.position, botPos.transform.position, 1 * Time.deltaTime);
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerCombatScript>().TakeDamage(1, gameObject);
        }
    }
}
