using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling Current { get; private set; }
    [SerializeField] private GameObject pooledObject;
    [SerializeField] private int amountOfPooledObjects = 10;

    private List<GameObject> pooledObjects;

    void Awake()
    {
        Current = this;
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountOfPooledObjects; i++)
        {
            GameObject obj = Instantiate(pooledObject);
            pooledObjects.Add(obj);
            obj.SetActive(false);
        }
    }
    public GameObject GetObjectInstance()
    {
        for(int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                pooledObjects[i].SetActive(true);
                return pooledObjects[i];
            }
        }
        GameObject obj = Instantiate(pooledObject);
        pooledObjects.Add(obj);
        return obj;
    }
}
