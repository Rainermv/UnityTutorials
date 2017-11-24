using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Stuff : PooledObject {

    public Rigidbody Body { get; private set; }

    private MeshRenderer[] meshRenderers;

    void Awake() {
        Body = GetComponent<Rigidbody>();
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {
       if (other.CompareTag("KILL")) {
            // return to the pool instead of destroying itself
            ReturnToPool();
        }
    }

    public void SetMaterial(Material m) {
        for (int i = 0; i < meshRenderers.Length; i++) {
            meshRenderers[i].material = m;
        }
    }
}
