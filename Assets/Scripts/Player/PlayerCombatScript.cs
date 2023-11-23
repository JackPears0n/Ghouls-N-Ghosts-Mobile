using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerCombatScript : MonoBehaviour
{
    [Header ("Variables")]
    public int maxHP, currentHP;
    public int maxMagicCharge, currentMagicCharge;
    public string currentWeapon;
    public LayerMask enemyLayerMask;

    [Header("Components")]
    public GameObject playerParent;
    private PlayerScript ps;

    [Header("Game Objects")]
    public Button castMagic;
    public Slider magicC;
    public Slider hp;

    public Button[] attackButtons;

    public GameObject[] weapons;

    public GameObject weaponSpawn;

    // Start is called before the first frame update
    void Start()
    {
        ps = gameObject.GetComponent<PlayerScript>();

        currentMagicCharge = 0;
        currentHP = 2;
    }

    // Update is called once per frame
    void Update()
    {
        CheckHP();
        ShowMagicUI();
    }

    //Makes the player take damage
    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;
    }

    //Shows the magic UI
    public void ShowMagicUI()
    {
        if (currentHP == 3)
        {
            castMagic.gameObject.SetActive(true);
            magicC.gameObject.SetActive(true);
        }
        else
        {
            castMagic.gameObject.SetActive(false);
            magicC.gameObject.SetActive(false);
        }
    }
    //Uses weapon magic
    public void UseMagic()
    {
        //lance
        //sword
        //super sword
        //big axe
        //fire water
        //discus
        //dagger
    }

    //Attacks with the current weapon horisontally
    public void UseWeaponHorisontal(int x)
    {
        Vector2 direction = new Vector2(x, 0);

        //spear
        if (currentWeapon == "spear")
        {
            //Instanciate weapon with a velocity
            GameObject weapon;
            weapon = Instantiate(weapons[4]);

            // get the rigidbody component
            Rigidbody2D rb = weapon.GetComponent<Rigidbody2D>();

            // set the position
            rb.transform.position = new Vector2(weaponSpawn.transform.position.x, weaponSpawn.transform.position.y);

            // set the velocity
            rb.AddForce(direction.normalized * 1.5F * 10f, ForceMode2D.Force);
            
        }
        //sword
        if (currentWeapon == "sword")
        {
            //shoot raycast at enemy layer
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 2, enemyLayerMask);

            hit = Physics2D.Raycast(transform.position, direction, 2, enemyLayerMask);
            //select 1st enemy
            //play animation
            //damage enemy
        }

        //axe
        if (currentWeapon == "axe")
        {
            //shoot raycast at enemy layer
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 5, enemyLayerMask);

            hit = Physics2D.Raycast(transform.position, direction, 5, enemyLayerMask);
            //select 1st enemy
            //play animation
            //damage enemy
        }

        //fire water
        if (currentWeapon == "fire water")
        {
            //shoot raycast at enemy layer
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 4, enemyLayerMask);

            hit = Physics2D.Raycast(transform.position, direction, 4, enemyLayerMask);
            //select 1st enemy
            //play animation
            //damage enemy
        }

        //discus
        if (currentWeapon == "discus")
        {
            //shoot raycast at enemy layer
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 10, enemyLayerMask);

            hit = Physics2D.Raycast(transform.position, direction, 10, enemyLayerMask);
            //select 1st enemy
            //play animation
            //damage enemy
        }

        //dagger
        if (currentWeapon == "dagger")
        {
            //shoot raycast at enemy layer
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 10, enemyLayerMask);

            hit = Physics2D.Raycast(transform.position, direction, 10, enemyLayerMask);
            //select 1st enemy
            //play animation
            //damage enemy
        }
    }

    //Checks the player's HP
    public void CheckHP()
    {
        hp.value = currentHP;

        if (currentHP <= 0)
        {
            StartCoroutine(Die());
        }

        if (currentHP == 1)
        {
            ps.currentSprite = ps.sprites[0];
        }

        if (currentHP == 2)
        {
            ps.currentSprite = ps.sprites[1];
        }

        if (currentHP == 3)
        {
            ps.currentSprite = ps.sprites[2];
        }

        if (currentHP > 3)
        {
            currentHP = 3;
        }

        UpdateSprites();
    }

    //End the game
    public IEnumerator Die()
    {
        yield return new WaitForSeconds(1F); 
    }

    //Updates the sprite to represent the player's status
    public void UpdateSprites()
    {
        foreach (var sprite in ps.sprites)
        {
            if (sprite.gameObject != ps.currentSprite)
            {
                sprite.gameObject.SetActive(false);
            }
            else
            {
                sprite.gameObject.SetActive(true);
            }
        }
    }
}
