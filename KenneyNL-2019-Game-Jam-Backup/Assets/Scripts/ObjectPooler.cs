using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    //public static ObjectPooler current;
    private GameObject pooledObject;
    private int pooledAmount;
    private bool willGrow;

    private List<GameObject> pooledObjects;

    private void Awake()
    {
        //current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        pooledObjects = new List<GameObject>();
        for(int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject getPooledObject()
    {
        for(int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        if (willGrow)
        {
            GameObject obj = Instantiate(pooledObject);
            pooledObjects.Add(obj);
            pooledAmount++;
            return obj;
        }
        return null;
    }

    public void setPooledObject(GameObject po)
    {
        pooledObject = po;
    }

    public void setWillGrow(bool wg)
    {
        willGrow = wg;
    }

    public void setPooledAmount(int pa)
    {
        pooledAmount = pa;
    }

    public int getPooledAmount()
    {
        return pooledAmount;
    }

    public void create()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }
}
