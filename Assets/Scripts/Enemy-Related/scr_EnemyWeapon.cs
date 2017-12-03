using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_EnemyWeapon : MonoBehaviour {
	
	private AudioSource audioSource;
	public GameObject shot;
    public GameObject powerup;
    public int powerupChance;
	public Transform shotSpawn;
	public float minFireRate;
	public float maxFireRate;
	public float delay;


	void Start () {
		audioSource = GetComponent<AudioSource> ();
		InvokeRepeating ("Fire", delay, Random.Range(minFireRate,maxFireRate));
	}

	void Fire()
	{
		Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		audioSource.Play ();
	}

    void OnDestroy()
    {
        //powerupChance = Random.Range(0, 2);
        //if (powerupChance == 2)
        //{
        if(GetComponent<Stats>().currentHP <= 0)
            Instantiate(powerup, transform.position, transform.rotation);
        //}
    }
}
