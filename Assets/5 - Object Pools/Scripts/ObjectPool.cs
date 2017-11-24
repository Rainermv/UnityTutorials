using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Able to get and return objects from it.
public class ObjectPool : MonoBehaviour {

    private PooledObject prefab;

    private List<PooledObject> availableObjects = new List<PooledObject>();

    // Get object from the pool if available (for now only create a new object)
    public PooledObject GetObject() {

        PooledObject obj;

        int lastAvailableIndex = availableObjects.Count - 1;
        if (lastAvailableIndex >= 0) {

            // Get last object on the list and activate it
            obj = availableObjects[lastAvailableIndex];
            availableObjects.RemoveAt(lastAvailableIndex);
            obj.gameObject.SetActive(true);

        }
        else {
            obj = Instantiate<PooledObject>(prefab);

            obj.transform.SetParent(transform, false);
            obj.Pool = this;
        }

        return obj;

    }
    
    // Add an object to the pool (for now only destroys the object)
    public void AddObject(PooledObject obj) {

        // Set the object to inactive and add it to the list
        obj.gameObject.SetActive(false);
        availableObjects.Add(obj);

    }

    // Create a new Pool object and return a reference to it
    public static ObjectPool GetPool (PooledObject prefab) {

        GameObject obj;
        ObjectPool pool;

        string poolName = prefab.name + " Pool";

        if (Application.isEditor) {

            obj = GameObject.Find(poolName);

            if (obj) {
                pool = obj.GetComponent<ObjectPool>();
                if (pool) {
                    return pool;
                }
            }

        }

        obj = new GameObject(poolName);
        pool = obj.AddComponent<ObjectPool>();
        pool.prefab = prefab;
        return pool;

    }
}
