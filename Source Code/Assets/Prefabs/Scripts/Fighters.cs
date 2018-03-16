using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighters : MonoBehaviour {

    Rigidbody rg;
    public float speed = 50;

    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    void Update()
    {
        foreward();
    }

    public void foreward()
    {
        rg.velocity = transform.forward * speed;
    }
}
