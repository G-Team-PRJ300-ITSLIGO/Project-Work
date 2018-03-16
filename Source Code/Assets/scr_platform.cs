using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_platform : MonoBehaviour {

    float timer;
    public GameObject boss;
    public GameObject drone;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(timer != -1f)
        timer += Time.deltaTime;
        if (timer >= 4f)
        {
            Instantiate(boss, boss.transform.position, boss.transform.rotation);
            Instantiate(drone, drone.transform.position, drone.transform.rotation);
            Debug.Log("NICE");
            timer = -1f;
        }
	}
}
