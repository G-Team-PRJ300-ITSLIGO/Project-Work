using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class scr_playerBehaviour : MonoBehaviour 
{
	public float speed;
	public float tilt;
	public Boundary boundingArea;
	public Rigidbody rb;

	public GameObject[] WeaponTypes;
	public Transform weaponSpawnerTrans;

	public float fireRate;
	private float nextFire = 0.0f;


	// Use this for initialization
	void Update ()
	{
		if(WeaponTypes != null)
		{
			if(WeaponTypes[0] != null)
			if(!Input.GetButton ("Fire2"))
				if (Input.GetButton ("Fire1") && Time.time > nextFire) 
				{
					GameObject weaponShot = WeaponTypes[0];
					nextFire = Time.time + fireRate;           
					Instantiate (weaponShot, weaponSpawnerTrans.position, weaponSpawnerTrans.rotation);
					GetComponent<AudioSource>().Play();
				}

			if(WeaponTypes[1] != null)
			if(!Input.GetButton ("Fire1"))
				if (Input.GetButton ("Fire2") && Time.time > nextFire) 
				{ 
					GameObject weaponShot = WeaponTypes [1];
					nextFire = Time.time + fireRate;
					Instantiate (weaponShot, weaponSpawnerTrans.position, weaponSpawnerTrans.rotation);
					GetComponent<AudioSource>().Play();
				}
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		rb = GetComponent<Rigidbody> ();

		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.velocity = movement * speed;

		rb.position = new Vector3 
			(
				Mathf.Clamp(rb.position.x, boundingArea.xMin, boundingArea.xMax),
				0.0f,
				Mathf.Clamp(rb.position.z, boundingArea.zMin, boundingArea.zMax)
			);

		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);

	}
}
