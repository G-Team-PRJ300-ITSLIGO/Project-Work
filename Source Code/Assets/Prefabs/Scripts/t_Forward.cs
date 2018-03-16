using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t_Forward : MonoBehaviour {

    public bool destroyable;

    Rigidbody rg;
    public float speed = 50;
    public int direction;

    public float timeToMove;
    public float timeToDestroy;
    public float time;


    // Use this for initialization
    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        time += Time.deltaTime;
        if (time > timeToMove)
        {
            switch (direction)
        {
            case 1:
                rg.velocity = transform.forward * speed;
                break;
            case 2:
                rg.velocity = transform.right * speed;
                break;
            default:
                rg.velocity = transform.right * speed;
                break;
        }
        }

        if (time >= timeToDestroy && destroyable == true)
        {
            Destroy(gameObject);
        }
    }
}
