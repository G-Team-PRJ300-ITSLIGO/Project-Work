using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_EnemyWeapon : MonoBehaviour {
	
	private AudioSource audioSource;
	public GameObject shot;
    public GameObject powerup;
	public Transform shotSpawn;
	public float delay;


	void Start () {
        audioSource = GetComponent<AudioSource> ();
		InvokeRepeating ("Fire", delay, Random.Range(GetComponent<scr_enemyStats>().stats.fireRate, GetComponent<scr_enemyStats>().stats.fireRate*2));
	}

	void Fire()
	{
		Instantiate (shot, shotSpawn.position, Quaternion.Euler(0f,180f,0f));
		audioSource.Play ();
	}

    void OnDestroy()
    {
        if (powerup != null)
        {
            if (GetComponent<scr_enemyStats>().stats.currentHP <= 0)
                Instantiate(powerup, transform.position, transform.rotation);
        }
        
    }
}
