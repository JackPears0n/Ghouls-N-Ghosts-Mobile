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
    float axeDuration = 5;

    void Start()
    {
        weaponType = gameObject.tag;
    }

    void Update()
    {
        if(weaponType == "Axe")
        {
            axeDuration -= Time.deltaTime;

            if(axeDuration <= 0)
            {
                Destroy(gameObject);
            }
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
        //cast a ray downward
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.15F, groundLayerMask);

        hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.15F, groundLayerMask);

        Debug.DrawRay(transform.position, -Vector2.up * 0.15F, (hit.collider != null) ? Color.green : Color.white);

        return hit.collider != null;
    }
}
