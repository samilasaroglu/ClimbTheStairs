using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize;
    private Queue<GameObject> pooledObjects;


    private void Awake()
    {
        pooledObjects = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.transform.SetParent(transform);
            obj.SetActive(false);

            pooledObjects.Enqueue(obj);
        }
    }

    public GameObject GetObject()
    {
        GameObject obj = pooledObjects.Dequeue();

        obj.SetActive(true);

        pooledObjects.Enqueue(obj);

        return obj;
    }
}
