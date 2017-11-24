using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffSpawnerRing : MonoBehaviour {

    public Material[] stuffMaterials;
    public int numberOfSpawners;

    public float radius;
    public float tiltAngle;

    public StuffSpawner spawnerPrefab;

    void Awake() {
        for (int i = 0; i < numberOfSpawners; i++) {

            CreateSpawner(i);

        }    
    }

   void CreateSpawner(int index) {

        Transform rotater = new GameObject("Rotater").transform;
        rotater.SetParent(transform, false);

        rotater.localRotation = Quaternion.Euler(0f, ((float)index / (float)numberOfSpawners) * 360f, 0f);

        
        StuffSpawner spawner = Instantiate<StuffSpawner>(spawnerPrefab);

        Transform spawner_transform = spawner.transform;
        spawner_transform.SetParent(rotater, false);
        spawner_transform.localPosition = new Vector3(0f, 0f, radius);
        spawner_transform.localRotation = Quaternion.Euler(tiltAngle, 0f, 0f);

        spawner.stuffMaterial = stuffMaterials[index % stuffMaterials.Length];
        


    }
}
