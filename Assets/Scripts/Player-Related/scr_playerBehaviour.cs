using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class scr_playerBehaviour : MonoBehaviour 
{
	public float speed;
	public float tilt;
	public Boundary boundingArea;
	public Rigidbody rb;
	public bool locked = true;

	public GameObject[] WeaponTypes;
	public Transform LeftCannonTrans;
	public Transform RightCannonTrans;

	public float fireRate;
    public int powerUpBullets;
	private float nextFire = 0.0f;


	// Use this for initialization
	void Update ()
	{
        foreach(GameObject bullet in WeaponTypes)
        {
            bullet.GetComponent<Stats>().Damage = GetComponent<Stats>().Damage;
        }
		if (locked) return;
		if(WeaponTypes != null)
		{
			if(WeaponTypes[0] != null)
			if(!Input.GetButton ("Fire2"))
				if (Input.GetButton ("Fire1") && Time.time > nextFire) 
				{
					GameObject weaponShot = WeaponTypes[0];
					nextFire = Time.time + fireRate;           
					Instantiate (weaponShot, LeftCannonTrans.position, Quaternion.Euler(0f,0f,0f));
					Instantiate (weaponShot, RightCannonTrans.position, Quaternion.Euler(0f,0f,0f));
					GetComponent<AudioSource>().Play();
				}

			if(WeaponTypes[1] != null)
			if(!Input.GetButton ("Fire1"))
				if (Input.GetButton ("Fire2") && Time.time > nextFire && powerUpBullets > 0) 
				{ 
					GameObject weaponShot = WeaponTypes [1];
					nextFire = Time.time + fireRate * 2;
					Instantiate (weaponShot, LeftCannonTrans.position, Quaternion.Euler(0f,0f,0f));
					Instantiate (weaponShot, RightCannonTrans.position, Quaternion.Euler(0f,0f,0f));
					GetComponent<AudioSource>().Play();
					GetComponent<AudioSource>().Play();
                    powerUpBullets--;
				}
		}

        if(Input.GetKeyDown(KeyCode.U))
        {
            powerUpBullets = 50;
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (locked)	return;
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
