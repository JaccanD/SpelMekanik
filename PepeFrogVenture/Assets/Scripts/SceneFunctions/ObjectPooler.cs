using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PooledObject
{
    public GameObject objectToPool;
    public int pooledAmount;
}

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler instance { get; private set; }
    [SerializeField] private List<PooledObject> DifferentObjectsToPool;
    private List<GameObject> pooledObjects;

    void Awake()
    {
        instance = this;

        pooledObjects = new List<GameObject>();
        foreach (PooledObject obj in DifferentObjectsToPool)
        {
            for (int i = 0; i < obj.pooledAmount; i++)
            {
                GameObject gObj = Instantiate(obj.objectToPool);
                gObj.SetActive(false);
                pooledObjects.Add(gObj);
            }
        }
    }

    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                Debug.Log("object returned");
                return pooledObjects[i];
            }
        }
        foreach (PooledObject item in DifferentObjectsToPool)
        {
            if (item.objectToPool.tag == tag)
            {
                GameObject obj = Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
                return obj;
            }
        }
        return null;
    }
}
