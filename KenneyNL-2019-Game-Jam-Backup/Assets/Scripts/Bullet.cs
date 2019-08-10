using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;

    private Rigidbody2D rb;
    private BulletDestroy bdScript;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bdScript = GetComponent<BulletDestroy>();
    }

    private void OnEnable()
    {
        rb.velocity = transform.up * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bdScript.Destroy();
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            HealthCounter hc = collision.gameObject.GetComponent<HealthCounter>();
            if(hc != null)
            {
                hc.takeDamage(30f);
            }
            else
            {
                print("hc not found on collision");
            }
        }
    }
}
