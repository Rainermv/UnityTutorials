using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Nucleon : MonoBehaviour {

    public float attractionForce;

    Rigidbody body;

    void Awake() {
        body = GetComponent<Rigidbody>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        body.AddForce(transform.localPosition * -attractionForce);
	}
}
