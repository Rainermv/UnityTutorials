using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffSpawner : MonoBehaviour {

    public Material stuffMaterial;

    public FloatRange RandomTimeBetweenSpawns;
    public FloatRange RandomScale;
    public FloatRange RandomVelocity;
    public FloatRange RandomAngularVelocity;

    public float baseVelocity;
    //public float spawnDistance;
    public Stuff[] stuffPrefabs;

    //private float timeSinceLastSpawn;

	// Use this for initialization
	void Start () {

        GetComponent<MeshRenderer>().material = stuffMaterial;

        StartCoroutine(Spawntimer());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        		
	}

    IEnumerator Spawntimer() {

        while (true) {

            SpawnStuff();

            float time = RandomTimeBetweenSpawns.RandomInRange;
            yield return new WaitForSeconds(time);
        }

    }

    private void SpawnStuff() {
        Stuff prefab = stuffPrefabs[Random.Range(0, stuffPrefabs.Length)];
        Stuff spawn = prefab.GetPooledInstance<Stuff>(); // get a pooled instance of an object directly from the prefab

        spawn.transform.localPosition = transform.position;
        spawn.transform.localScale = Vector3.one * RandomScale.RandomInRange;
        spawn.transform.localRotation = Random.rotation;

        spawn.Body.velocity = transform.up * baseVelocity + Random.onUnitSphere * RandomVelocity.RandomInRange;
        spawn.Body.angularVelocity = Random.onUnitSphere * RandomAngularVelocity.RandomInRange;

        spawn.SetMaterial(stuffMaterial);
    }
}
