using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Boundary
{
	public float xMin,xMax,zMin,zMax;

}

public class PlayerController : MonoBehaviour 
{
	public float speed;
	public float tilt;
	public Boundary boundary;
	public Rigidbody rb;

	public GameObject shot;
	public Transform shotSpawn;
    public int shotType = 1;
    public int shotPower = 1;

	public float fireRate = 0.25f;
	private float nextFire = 0.0f;


	void Update()
	{
		if (Input.GetButton ("Fire1") && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
            Shoot(shotType, shotPower);
			GetComponent<AudioSource>().Play();
		}
        if(Input.GetKey(KeyCode.Alpha1))
        {
            shotPower = 1;
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            shotPower = 2;
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            shotPower = 3;
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            shotPower = 4;
        }
    }

	void FixedUpdate()
	{
		rb = GetComponent<Rigidbody> ();

		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.velocity = movement * speed;

		rb.position = new Vector3 
		(
				Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
				0.0f,
				Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
		);

		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}

    void Shoot(int shotType, int shotPower)
    {
        switch(shotType)
        {
            case 1:
                switch(shotPower)
                {
                    case 1:
                        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                        break;
                    case 2:
                        Instantiate(shot, shotSpawn.position, Quaternion.Euler(0,2.5f,0));
                        Instantiate(shot, shotSpawn.position, Quaternion.Euler(0, -2.5f, 0));

                        break;
                    case 3:
                        Instantiate(shot, shotSpawn.position, Quaternion.Euler(0, 5f, 0));
                        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                        Instantiate(shot, shotSpawn.position, Quaternion.Euler(0, -5f, 0));
                        break;
                    case 4:
                        Instantiate(shot, shotSpawn.position, Quaternion.Euler(0, 10f, 0));
                        Instantiate(shot, shotSpawn.position, Quaternion.Euler(0, 5f, 0));
                        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                        Instantiate(shot, shotSpawn.position, Quaternion.Euler(0, -5f, 0));
                        Instantiate(shot, shotSpawn.position, Quaternion.Euler(0, -10f, 0));
                        break;
                    default:
                        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                        break;
                }
                break;
        }
    }



}
