using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_shield : MonoBehaviour {

    public GameObject[] shieldPylons = new GameObject[5];
    public int PylonsActive;
    public bool alive = true;
	// Use this for initialization
	void Start () {
		
	}
	void OnEnable()
    {
        alive = true;
        foreach(GameObject p in shieldPylons)
        {
            p.SetActive(true);
            p.GetComponent<scr_enemyStats>().stats.currentHP = p.GetComponent<scr_enemyStats>().stats.HP;
        }
    }
	// Update is called once per frame
	void Update () {
        PylonsActive = 0;
        foreach (GameObject p in shieldPylons)
        {
            if(p.active)
            {
                PylonsActive++;
            }
        }
        if (PylonsActive == 0)
        {
            alive = false;
        }

        if(shieldPylons[0].GetComponent<scr_enemyStats>().stats.currentHP <= 0)
        { 
            shieldPylons[0].SetActive(false);
        }
        if (shieldPylons[1].GetComponent<scr_enemyStats>().stats.currentHP <= 0)
        {
            shieldPylons[1].SetActive(false);
        }
        if (shieldPylons[2].GetComponent<scr_enemyStats>().stats.currentHP <= 0)
        {
            shieldPylons[2].SetActive(false);
        }
        if (shieldPylons[3].GetComponent<scr_enemyStats>().stats.currentHP <= 0)
        {
            shieldPylons[3].SetActive(false);
        }
        if (shieldPylons[4].GetComponent<scr_enemyStats>().stats.currentHP <= 0)
        {
            shieldPylons[4].SetActive(false);
        }
    }
}
