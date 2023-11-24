using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D col;
    public string weaponType;
    public LayerMask enemyLayerMask;
    public LayerMask playerLayerMask;
    public LayerMask groundLayerMask;
    float weaponDuration = 2;

    public float rayLLength;
    public float rayRLength;
    public float rayDLength;

    void Start()
    {
        weaponType = gameObject.tag;
    }

    void Update()
    {
        weaponDuration -= Time.deltaTime;

        if(weaponDuration <= 0)
        {
            Destroy(gameObject);
        }

        if (GroundCheck())
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {


        if(collision.gameObject.layer == enemyLayerMask)
        {
            //Checks weapon type
                //Weapon function

            if(weaponType == "Spear" || weaponType == "Daggar" || weaponType == "Discus" || weaponType == "Axe" || weaponType == "Fire Water")
            {
                //deals 1 damage to the enemy
            }

            if(weaponType == "Sword")
            {
                //deals 2 damage to the enemy
            }

            Destroy(gameObject);

        }

        if(!(collision.gameObject.layer == enemyLayerMask || collision.gameObject.layer == playerLayerMask) && weaponType == "Axe")
        {
            Destroy(gameObject);
        }

    }

    public bool GroundCheck()
    {
        //cast a ray to the left
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, rayLLength, groundLayerMask);

        hitLeft = Physics2D.Raycast(transform.position, Vector2.left, rayLLength, groundLayerMask);

        Debug.DrawRay(transform.position, Vector2.left * rayLLength, (hitLeft.collider != null) ? Color.green : Color.white);

        //cast a ray to the right
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.left, rayRLength, groundLayerMask);

        hitLeft = Physics2D.Raycast(transform.position, -Vector2.left, rayRLength, groundLayerMask);

        Debug.DrawRay(transform.position, -Vector2.left * rayRLength, (hitRight.collider != null) ? Color.green : Color.white);

        //cast a ray to the down
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, -Vector2.up, rayDLength, groundLayerMask);

        hitLeft = Physics2D.Raycast(transform.position, -Vector2.up, rayDLength, groundLayerMask);

        Debug.DrawRay(transform.position, -Vector2.up * rayDLength, (hitDown.collider != null) ? Color.green : Color.white);

        if(hitLeft.collider != null || hitRight.collider != null || hitDown.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }


    }
}
