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
    float axeDuration = 5;

    void Start()
    {
        weaponType = gameObject.tag;
    }

    void Update()
    {
        if(weaponType == "axe")
        {
            axeDuration -= Time.deltaTime;

            if(axeDuration <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == enemyLayerMask)
        {
            //Checks weapon type
                //Weapon function

            if(weaponType == "spear" || weaponType == "daggar" || weaponType == "discus" || weaponType == "axe" || weaponType == "fire water")
            {
                //deals 1 damage to the enemy
            }

            if(weaponType == "sword")
            {
                //deals 2 damage to the enemy
            }

            Destroy(gameObject);

        }

        if(!(collision.gameObject.layer == enemyLayerMask || collision.gameObject.layer == playerLayerMask) && weaponType == "axe")
        {
            Destroy(gameObject);
        }

    }
}
