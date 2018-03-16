using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foreward : MonoBehaviour {

    Rigidbody rg;
    public float speed = 50;
	// Use this for initialization
	void Start () {
        rg = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        rg.velocity = transform.right * speed;
	}
}
