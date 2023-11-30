using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPScript : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public bool invulnerable;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (!invulnerable)
        {
            currentHealth -= damage;
        }

        // Play hurt animation

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        print("Enemy died");
        // Death animation

        Invoke(nameof (DestroyEnemyOBJ), 0.1F);

        // Disable the enemy
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

    }

    public void DestroyEnemyOBJ()
    {
        Destroy(gameObject);
    }
}
