using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public float timeBetweenSpawns;
    public float spawnDistance;
    public Nucleon[] nucleonPrefabs;

    private float timeSinceLastSpawn;

	// Use this for initialization
	void Start () {
        StartCoroutine(Spawntimer());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        /*
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= timeBetweenSpawns) {
            timeSinceLastSpawn -=
        }
        */
		
	}

    IEnumerator Spawntimer() {

        while (true) {

            SpawnNucleon();

            yield return new WaitForSeconds(timeBetweenSpawns);
        }

    }

    private void SpawnNucleon() {
        Nucleon prefab = nucleonPrefabs[Random.Range(0, nucleonPrefabs.Length)];
        Nucleon spawn = Instantiate<Nucleon>(prefab);

        spawn.transform.localPosition = Random.onUnitSphere * spawnDistance;
    }
}
