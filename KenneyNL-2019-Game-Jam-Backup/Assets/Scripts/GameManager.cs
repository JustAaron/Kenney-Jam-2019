using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int textureIndex;
    public GameObject[] textures;
    public GameObject player;
    public GameObject[] playerSpawn;
    //public GameObject[] enemyTypes;
    public GameObject[] enemySpawns;
    public int level; //set to -1 if in menu.

    private ObjectPooler[] poolers; //0 is enemies, 1 is explosions

    // Start is called before the first frame update
    void Start()
    {
        poolers = GetComponents<ObjectPooler>();
        poolers[0].setPooledObject(transform.GetChild(0).gameObject);
        poolers[1].setPooledObject(transform.GetChild(1).gameObject);
        foreach (ObjectPooler op in poolers)
        {
            op.setPooledAmount(5);
            op.setWillGrow(true);
            op.create();
        }
        setUpBackground();
        setUpPlayer();
        setUpEnemies();
    }

    private void setUpBackground()
    {
        for (int i = -10; i < 10; i++)
        {
            for (int j = -10; j < 10; j++)
            {
                GameObject tempSand = Instantiate(textures[textureIndex], new Vector3(i, j, 0), Quaternion.identity);
                tempSand.GetComponent<SpriteRenderer>().sortingOrder = 0;
            }
        }
    }

    private void setUpPlayer()
    {
        if(level >= 0)
        {
            player.transform.position = playerSpawn[level].transform.position;
        }
    }

    private void setUpEnemies()
    {
        if (level >= 0)
        {
            switch (level)
            {
                case 0: // level 01
                    for (int i = 0; i < enemySpawns.Length; i++)
                    {
                        GameObject enemy = poolers[0].getPooledObject();
                        enemy.transform.position = enemySpawns[i].transform.position;
                        enemy.SetActive(true);
                    }
                    break;
            }
        }
    }

    public void createExplosion(Vector3 pos)
    {
        GameObject explosion = poolers[1].getPooledObject();
        explosion.transform.position = pos;
        explosion.SetActive(true);
    }
}
