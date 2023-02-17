using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    public static ObjectPool SharedInstance;
    public List<GameObject> objectToPool;
    public int amountToPool;
    public List<GameObject> pooledObjects;
    public int poolCounter = -1;

    private void Awake()
    {
        SharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        BuildPool();
        //Debug.Log(pooledObjects.Count);
    }

    // Pool list of random objects
    private void BuildPool()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool[Random.Range(0, objectToPool.Count)]);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    //private void BuildPool(int min, int max)
    //{
    //    pooledObjects = new List<GameObject>();
    //    GameObject tmp;
    //    for (int i = 0; i < amountToPool; i++)
    //    {
    //        tmp = Instantiate(objectToPool[Random.Range(min,max)]);
    //        tmp.SetActive(false);
    //        pooledObjects.Add(tmp);
    //    }
    //}

    // Return pooled object if it is not in the active hierarchy
    public GameObject GetPooledObject()
    {
        poolCounter++;
        if (poolCounter>= amountToPool)
        {
            poolCounter = -1;
        }
        if (!pooledObjects[poolCounter].activeInHierarchy&&poolCounter<amountToPool)
        {
            return pooledObjects[poolCounter];
        }
        
        return pooledObjects[poolCounter];   
    }
}
