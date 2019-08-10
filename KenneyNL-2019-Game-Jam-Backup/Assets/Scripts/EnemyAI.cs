using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    private Transform target;
    private float speed = 200f;
    private float nextWaypointDistance = 3f;
    private Path path;
    private int currentWaypoint = 0;
    //private bool reachedEndOfPath = false;
    private Seeker seeker;
    private Rigidbody2D rb;

    private float rotationSpeed;
    private float fireTime;
    private HealthCounter hc;

    private void Start()
    {
        //print("target is " + target);
        target = GameObject.FindGameObjectWithTag("Player").transform;
        //print("target is now " + target);
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        hc = GetComponent<HealthCounter>();
        hc.setMaxHealth(100f);
        hc.setType(1);
    }

    private void OnEnable()
    {
        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void fire()
    {

    }

    private void FixedUpdate()
    {
        if(path == null)
        {
            return;
        }
        if(currentWaypoint >= path.vectorPath.Count || Vector2.Distance(rb.position, target.position) <= 7.5f)
        {
            //reachedEndOfPath = true;
            return;
        }
        else
        {
            //reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.fixedDeltaTime;
        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
}
