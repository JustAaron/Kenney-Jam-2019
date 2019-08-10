using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    //public GameObject bullet;
    //public int pooledAmount;
    //public float fireTime;
    //public bool willGrow;
    //public GameObject bulletSpawn;

    private Rigidbody2D rb;
    //private BoxCollider2D bc;
    //private List<GameObject> bullets;
    //private float lastFireTime;
    //private ObjectPooler opScript;
    private Tank tankScript;
    private HealthCounter healthCounterScript;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //bc = GetComponent<BoxCollider2D>();
        //opScript = GetComponent<ObjectPooler>();
        tankScript = GetComponent<Tank>();
        tankScript.setFireTime(.25f);
        tankScript.setPooledAmount(5);
        tankScript.setWillGrow(false);
        healthCounterScript = GetComponent<HealthCounter>();
        healthCounterScript.setMaxHealth(200f);
        healthCounterScript.setType(0);
        /*bullets = new List<GameObject>();
        for(int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = Instantiate(bullet);
            obj.SetActive(false);
            bullets.Add(obj);
        }*/
        //lastFireTime = Time.unscaledTime;
    }

    // Update is called once per frame
    void Update()
    {
        //float translation = Input.GetAxis("Vertical") * speed;
        //float rotation = Input.GetAxis("Horizontal") * rotationSpeed * -1;

        //translation *= Time.deltaTime;
        //rotation *= Time.deltaTime;

        //transform.Translate(0, translation, 0);
        //transform.Rotate(0, 0, rotation);
        float movementControl = Input.GetAxis("Vertical");
        float rotationControl = Input.GetAxis("Horizontal") * -1f;

        rb.velocity = transform.up * speed * movementControl;
        //print(rb.rotation);
        rb.SetRotation(rb.rotation + (rotationControl * rotationSpeed));
        //if (Input.GetButton("Fire1") && Time.unscaledTime - lastFireTime >= fireTime)
        //{
        //    //fire();
        //    tankScript.fire();
        //    lastFireTime = Time.unscaledTime;
        //    //print("player angle is " + transform.eulerAngles);
        //}
        if (Input.GetButton("Fire1") && tankScript.checkFireTime())
        {
            tankScript.fire();
        }
    }

    private void FixedUpdate()
    {
        /* float movementControl = Input.GetAxis("Vertical");
        float rotationControl = Input.GetAxis("Horizontal");

        rb.velocity = transform.forward * speed * movementControl;
        transform.Rotate(Vector3.up * rotationSpeed * rotationControl * Time.deltaTime);
        *(/
        /*if (Input.GetAxis("Vertical") > 0) // Up
        {
            //print("up");
            upDir = 1f;
            rb.AddRelativeForce(transform.up * speed);
        }
        else if (Input.GetAxis("Vertical") < 0) // Down
        {
            //print("down");
            upDir = -1f;
            rb.AddRelativeForce(transform.up * speed);
        }
        else if (Input.GetAxis("Vertical") == 0) // Not Up or Down
        {
            upDir = 0f;
            rb.velocity = new Vector3(0f, 0f, 0f);
        }
        if (Input.GetAxis("Horizontal") > 0) // Right
        {
            //print("right");
            rightDir = 1f;
            rb.AddTorque(rightDir * rotationSpeed * Time.fixedDeltaTime);
        }
        else if (Input.GetAxis("Horizontal") < 0) // Left
        {
            //print("left");
            rightDir = -1f;
            rb.AddTorque(rightDir * rotationSpeed * Time.fixedDeltaTime);
        }
        else if (Input.GetAxis("Horizontal") == 0) // Not left or right
        {
            rightDir = 0f;
            rb.rotation = 0f;
        }*/
    }

    // Fires bullets;
    /*private void fire()
    {
        /*for(int i = 0; i < opScript.pooledObjects.Count; i++)
        {
            if (!bullets[i].activeInHierarchy)
            {
                bullets[i].transform.position = bulletSpawn.transform.position;
                bullets[i].transform.rotation = transform.rotation;
                bullets[i].SetActive(true);
                break;
            }
        }*/
    /*GameObject bullet = opScript.getPooledObject();
    if(bullet != null)
    {
        bullet.transform.position = bulletSpawn.transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.SetActive(true);
    }
}*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            healthCounterScript.takeDamage(20f);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            healthCounterScript.instaKill();
        }
    }
}
