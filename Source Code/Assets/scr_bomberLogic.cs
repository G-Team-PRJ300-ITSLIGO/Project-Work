using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_bomberLogic : MonoBehaviour {
	public float speed;
	private Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		rb.velocity = transform.right * speed;
	}
}
