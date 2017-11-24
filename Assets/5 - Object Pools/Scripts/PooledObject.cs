using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour {

    public ObjectPool Pool { get; set; }

    // All the prefabs will should have a reference to its respective pool
    [System.NonSerialized]
    ObjectPool poolInstanceForPrefab;

    // Tell the object to add itself back to the pool OR destroy itself
    public void ReturnToPool() {
        if (Pool) {
            Pool.AddObject(this);
        }
        else {
            Destroy(gameObject);
        }

    }

    // Get an object from the pool.
    // Each prefab Stuff asset should have a reference to its respective ObjectPool
    public T GetPooledInstance<T>() where T : PooledObject {

        // if there is no reference for the pool, get one
        if (!poolInstanceForPrefab) {
            poolInstanceForPrefab = ObjectPool.GetPool(this);
        }

        // if there is a Pool Instance reference already on the scene 
        // request and return a PooledObject from it
        return (T)poolInstanceForPrefab.GetObject();
    }
}
