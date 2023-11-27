using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public int damage;

    public Rigidbody2D rb;
    public Collider2D col;
    public string weaponType;
    public LayerMask enemyLayerMask;
    public LayerMask playerLayerMask;
    public LayerMask groundLayerMask;
    float weaponDuration = 2;

    public float rayLLength;
    public float rayRLength;
    public float rayULength;
    public float rayDLength;

    void Start()
    {
        weaponType = gameObject.tag;
    }

    void Update()
    {
        CheckForEnemy();
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

    public void CheckForEnemy()
    {
        #region RayCast
        //cast a ray to the left
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, rayLLength + 0.1F, enemyLayerMask);

        hitLeft = Physics2D.Raycast(transform.position, Vector2.left, rayLLength + 0.1F, enemyLayerMask);

        Debug.DrawRay(transform.position, Vector2.left * (rayLLength + 0.1F), (hitLeft.collider != null) ? Color.green : Color.white);

        //cast a ray to the right
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, -Vector2.left, rayRLength + 0.1F, enemyLayerMask);

        hitLeft = Physics2D.Raycast(transform.position, -Vector2.left, rayRLength + 0.1F, enemyLayerMask);

        Debug.DrawRay(transform.position, -Vector2.left * (rayRLength + 0.1F), (hitRight.collider != null) ? Color.green : Color.white);

        //cast a ray to the up
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, rayULength + 0.1F, enemyLayerMask);

        hitLeft = Physics2D.Raycast(transform.position, Vector2.up, rayULength + 0.1F, enemyLayerMask);

        Debug.DrawRay(transform.position, Vector2.up * (rayULength + 0.1F), (hitUp.collider != null) ? Color.green : Color.white);

        //cast a ray to the down
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, -Vector2.up, rayDLength + 0.1F, enemyLayerMask);

        hitLeft = Physics2D.Raycast(transform.position, -Vector2.up, rayDLength + 0.1F, enemyLayerMask);

        Debug.DrawRay(transform.position, -Vector2.up * (rayDLength + 0.1F), (hitDown.collider != null) ? Color.green : Color.white);
        #endregion

        if (hitLeft.collider)
        {
            if (weaponType == "Spear" || weaponType == "Daggar" || weaponType == "Discus" || weaponType == "Axe" || weaponType == "Fire Water")
            {
                //deals 1 damage to the enemy
                EnemyHPScript eHPS = hitLeft.collider.gameObject.GetComponent<EnemyHPScript>();
                eHPS.TakeDamage(1);
            }

            if (weaponType == "Sword")
            {
                //deals 2 damage to the enemy
                EnemyHPScript eHPS = hitLeft.collider.gameObject.GetComponent<EnemyHPScript>();
                eHPS.TakeDamage(2);
            }

            Destroy(gameObject);
        }

        if (hitRight.collider)
        {
            if (weaponType == "Spear" || weaponType == "Daggar" || weaponType == "Discus" || weaponType == "Axe" || weaponType == "Fire Water")
            {
                //deals 1 damage to the enemy
                EnemyHPScript eHPS = hitRight.collider.gameObject.GetComponent<EnemyHPScript>();
                eHPS.TakeDamage(1);
            }

            if (weaponType == "Sword")
            {
                //deals 2 damage to the enemy
                EnemyHPScript eHPS = hitRight.collider.gameObject.GetComponent<EnemyHPScript>();
                eHPS.TakeDamage(2);
            }

            Destroy(gameObject);
        }

        if (hitUp.collider)
        {
            if (weaponType == "Spear" || weaponType == "Daggar" || weaponType == "Discus" || weaponType == "Axe" || weaponType == "Fire Water")
            {
                //deals 1 damage to the enemy
                EnemyHPScript eHPS = hitUp.collider.gameObject.GetComponent<EnemyHPScript>();
                eHPS.TakeDamage(1);
            }

            if (weaponType == "Sword")
            {
                //deals 2 damage to the enemy
                EnemyHPScript eHPS = hitUp.collider.gameObject.GetComponent<EnemyHPScript>();
                eHPS.TakeDamage(2);
            }

            Destroy(gameObject);
        }

        if (hitDown.collider)
        {
            if (weaponType == "Spear" || weaponType == "Daggar" || weaponType == "Discus" || weaponType == "Axe" || weaponType == "Fire Water")
            {
                //deals 1 damage to the enemy
                EnemyHPScript eHPS = hitDown.collider.gameObject.GetComponent<EnemyHPScript>();
                eHPS.TakeDamage(1);
            }

            if (weaponType == "Sword")
            {
                //deals 2 damage to the enemy
                EnemyHPScript eHPS = hitDown.collider.gameObject.GetComponent<EnemyHPScript>();
                eHPS.TakeDamage(2);
            }

            Destroy(gameObject);
        }

    }
}
