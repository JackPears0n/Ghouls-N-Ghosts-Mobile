using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESpawnPointScript : MonoBehaviour
{
    public GameObject enemyPrefab;
    GameObject enemy;

    public GameObject spawnLocation;

    public int currentEnemies;
    public int spawnLimit;

    public int spawnInterval;

    public bool canSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {

        if (enemy = null)
        {
            currentEnemies = 0;
        }

        if (currentEnemies < spawnLimit)
        {
            canSpawn = true;
        }
        else
        {
            canSpawn = false;
        }

        if (canSpawn)
        {    
            enemy = Instantiate(enemyPrefab);

            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();

            // set the position
            rb.transform.position = new Vector2(spawnLocation.transform.position.x, spawnLocation.transform.position.y);

            currentEnemies++;
        }

        yield return new WaitForSeconds(spawnInterval);
    }
}
