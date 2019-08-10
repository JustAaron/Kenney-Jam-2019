using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    //public float speed;
    //public float rotationSpeed;
    private float fireTime;
    private GameObject bulletSpawn;
    private GameObject bullet;
    private float lastFireTime;
    private ObjectPooler bullets;
    private SoundManager soundManager;

    // Start is called before the first frame update
    void Awake()
    {
        bullets = GetComponent<ObjectPooler>();
        bullet = transform.GetChild(1).gameObject;
        //bullets.setPooledObject(bullet);
        lastFireTime = Time.unscaledTime;
        fireTime = 0.25f; //default time
        bullets.setPooledObject(transform.GetChild(1).gameObject);
        bullets.setPooledAmount(5); //default pooled amount
        bullets.setWillGrow(false); //default will grow 
        bullets.create();
        bulletSpawn = Instantiate(new GameObject(), transform);
        bulletSpawn.transform.position = transform.position + (Vector3.up * 1f);
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }

    public bool checkFireTime()
    {
        return (Time.unscaledTime - lastFireTime >= fireTime);
    }

    public void fire()
    {
        GameObject bullet = bullets.getPooledObject();
        if (bullet != null)
        {
            print("fired");
            soundManager.playTankShot();
            bullet.transform.position = bulletSpawn.transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
            lastFireTime = Time.unscaledTime;
        }
        else
        {
            //print("bullet unavailable");
        }
    }

    public void setFireTime(float ft)
    {
        fireTime = ft;
    }

    public void setWillGrow(bool wg)
    {
        bullets.setWillGrow(wg);
    }

    public void setPooledAmount(int pa)
    {
        bullets.setPooledAmount(pa);
    }
}
