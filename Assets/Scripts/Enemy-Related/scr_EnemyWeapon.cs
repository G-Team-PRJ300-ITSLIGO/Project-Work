using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_EnemyWeapon : MonoBehaviour {
	
	private AudioSource audioSource;
	public GameObject shot;
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
}
