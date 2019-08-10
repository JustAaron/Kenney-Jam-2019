using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCounter : MonoBehaviour
{
    private float maxHealth;
    private float health;
    private HealthBar healthBarScript;
    private GameManager gmScript;
    private SoundManager smScript;
    private int type; //0 is player, 1 is enemy, 2 is boss

    // Start is called before the first frame update
    void Awake()
    {
        maxHealth = 100f; //default max health
        health = maxHealth;
        healthBarScript = GetComponentInChildren<HealthBar>();
        if(healthBarScript == null)
        {
            print("healthbarscript not found");
        }
        gmScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        smScript = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }

    // NOTE: Also updates current health.
    public void setMaxHealth(float h) 
    {
        maxHealth = h;
        health = maxHealth;
        //print("health/maxhealth = " + health / maxHealth);
        updateHealthBar(health / maxHealth);
    }

    public void setType(int t)
    {
        type = t;
    }

    public void setCurrentHealth(float h)
    {
        health = h;
    }

    public float getCurrentHealth()
    {
        return health;
    }

    public void takeDamage(float d)
    {
        health -= d;
        if (checkIfDead())
        {
            //print("dead");
            kill();
        }
        else
        {
            updateHealthBar(health / maxHealth);
        }
    }

    public void instaKill()
    {
        health = 0f;
        kill();
    }

    /*private void OnDisable()
    {
        CancelInvoke();
    }*/

    private bool checkIfDead()
    {
        return (health <= 0f);
    }

    private void updateHealthBar(float percentRemaining)
    {
        healthBarScript.setSize(percentRemaining);
    }

    private void kill()
    {
        gmScript.createExplosion(transform.position);
        switch (type)
        {
            case 0: smScript.playPlayerExplosion();
                break;
            case 1: smScript.playEnemyExplosion();
                break;
            case 2: smScript.playBossExplosion();
                break;
            default: print("check for error");
                break;
        }
        gameObject.SetActive(false);
    }
}
