using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_mine : MonoBehaviour {

    public GameObject mineDebris;
    public int debrisAmount;
    public float rotation;
    void Start()
    {
        rotation = 360f / debrisAmount;
    }
    void OnDestroy()
        {
        if(GetComponent<Stats>().currentHP <= 0)
        {
            for (int i = 0; i <= debrisAmount; i++)
            {
                Instantiate(mineDebris, transform.position, Quaternion.Euler(0f, rotation * i, 0f));
            }
        }
        }
}
