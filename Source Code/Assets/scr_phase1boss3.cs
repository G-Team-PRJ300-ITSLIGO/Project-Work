using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_phase1boss3 : MonoBehaviour {

    Stats stats;
	// Use this for initialization
	void Start () {
        stats = GetComponent<scr_enemyStats>().stats;

    }
	
	// Update is called once per frame
	void Update () {
		if(stats.currentHP <= 0)
        {
            GetComponentInParent<Animator>().Play("Phase2");
            Destroy(gameObject);
        }
	}
    void OnDestroy()
    {
        GetComponentInParent<Animator>().Play("Phase2");
        Destroy(gameObject);
    }
}
