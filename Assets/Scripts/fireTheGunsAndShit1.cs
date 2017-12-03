using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Logic : MonoBehaviour {

    public GameObject boolets;
    private Rigidbody rb;
    public Transform shot1;
    public Transform shot2;
    public Transform shot3;
    public Transform shot4;
    public Transform shot5;
    public float fireRate;
    public float nextFire;
    private float nextFireBullets;
    public GameObject missile;
    public int shots;
    private float time = 0.0f;
    [SerializeField]
    private float oldRotate = 0;
    public float shotRotate;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();

       
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
      
		if (time <= 5f && shots == 1) {
			Phase1 ();

		} else if (time <= 10f) {
			Phase2 (0.01f);
			shots = 2;
		} else if (time <= 13f) {
			oldRotate = 0f;
			shotRotate = 15f;
			//StartCoroutine(FireMissiles());
			shots = 3;
			Phase3 (-30);
		} else if (time <= 16f) {
			Phase5 ();
		}
		else if (time <= 19f) {
			Phase3 (-30);
		} else if (time <= 24f) {
			Phase4 (0.01f);
		} else if (time <= 27f) {
			oldRotate = 0f;
			shotRotate = 15f;
			Phase3 (30);
		}
		else if (time < 30f) {
			Phase5();
		}
        else if (time <= 33f)
        {
            Phase3(30);
        }
        else if (time > 33f)
        {
            time = 5f;
        }

        if (oldRotate > 60)
        {
            oldRotate = 60;
            shotRotate *= -1;
        }
        if(oldRotate < -60)
        {
            oldRotate = -60;
            shotRotate *= -1;
        }


    }
    void Phase1()
    {
        rb.position += transform.right * 0.025f;
       
    }
    void Phase2(float speed)
    {
        rb.position += transform.right * speed;

        if (Time.time > nextFireBullets)
        { 
            Instantiate(boolets, shot1.position, shot1.rotation * Quaternion.Euler(0, this.oldRotate, 0));
        Instantiate(boolets, shot2.position, shot2.rotation * Quaternion.Euler(0, this.oldRotate, 0));
        nextFireBullets = Time.time + fireRate;
            this.oldRotate += this.shotRotate;
        }
      
    }
    void Phase4(float speed)
    {
        rb.position += transform.right * speed;

        if (Time.time > nextFireBullets)
        {
            Instantiate(boolets, shot4.position, shot4.rotation * Quaternion.Euler(0, this.oldRotate, 0));
            Instantiate(boolets, shot5.position, shot5.rotation * Quaternion.Euler(0, this.oldRotate, 0));
            nextFireBullets = Time.time + fireRate;
            this.oldRotate += this.shotRotate;
        }

    }
    void Phase3(int rotation)
    {
        rb.transform.Rotate(0, rotation * Time.deltaTime, 0);
    }
	void Phase5()
	{
		if (Time.time >= nextFire)
		{
			Instantiate(boolets, shot3.position, Quaternion.Euler(0, 2.5f, 0));
			Instantiate(boolets, shot3.position, Quaternion.Euler(0, -2.5f, 0));

			Instantiate(boolets, shot3.position, Quaternion.Euler(0, 20f, 0));
			Instantiate(boolets, shot3.position, Quaternion.Euler(0, -20f, 0));

			Instantiate(boolets, shot3.position, Quaternion.Euler(0, 35f, 0));
			Instantiate(boolets, shot3.position, Quaternion.Euler(0, -35f, 0));

			Instantiate(boolets, shot3.position, Quaternion.Euler(0, 50f, 0));
			Instantiate(boolets, shot3.position, Quaternion.Euler(0, -50f, 0));

			Instantiate(boolets, shot3.position, Quaternion.Euler(0, 65f, 0));
			Instantiate(boolets, shot3.position, Quaternion.Euler(0, -65f, 0));
			nextFire = Time.time + fireRate/2;
		}
	}
    //IEnumerator FireMissiles()
    //{

    //        Instantiate(missile, shot3.position, shot3.rotation);
    //        yield return new WaitForSeconds(1);
    //}
}
