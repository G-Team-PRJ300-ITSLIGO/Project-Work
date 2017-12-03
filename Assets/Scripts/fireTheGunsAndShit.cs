using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireTheGunsAndShit : MonoBehaviour {

    public GameObject boolets;
    public Canvas canvas;
    private scr_GameController gameController;
    private Rigidbody rb;
    public Transform shotTL;
    public Transform shotBL;
    public Transform shotM;
    public Transform shotTR;
    public Transform shotBR;
    public float fireRate;
    public float nextFire;
    private float nextFireBullets;
    public GameObject missile;
    public GameObject explosion;
    public int shots;
    private float time = 0.0f;
    [SerializeField]
    private float oldRotate = 0;
    public float shotRotate;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        FindGC();
       
    }
	
	// Update is called once per frame
	void Update () {


        time += Time.deltaTime;
      
		if (time <= 5f) {
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
        rb.position += transform.right * -0.0475f;
       
    }
    void Phase2(float speed)
    {
        rb.position += transform.right * -speed;

        if (Time.time > nextFireBullets)
        { 
            Instantiate(boolets, shotTL.position, shotTL.rotation * Quaternion.Euler(0, this.oldRotate, 0));
        Instantiate(boolets, shotBL.position, shotBL.rotation * Quaternion.Euler(0, this.oldRotate, 0));
        nextFireBullets = Time.time + fireRate;
            this.oldRotate += this.shotRotate;
            GetComponent<AudioSource>().Play();
        }
      
    }
    void Phase4(float speed)
    {
        rb.position += transform.right * -speed;

        if (Time.time > nextFireBullets)
        {
            Instantiate(boolets, shotTR.position, shotTR.rotation * Quaternion.Euler(0, this.oldRotate, 0));
            Instantiate(boolets, shotBR.position, shotBR.rotation * Quaternion.Euler(0, this.oldRotate, 0));
            nextFireBullets = Time.time + fireRate;
            this.oldRotate += this.shotRotate;
            GetComponent<AudioSource>().Play();
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
			Instantiate(boolets, shotM.position, shotM.rotation * Quaternion.Euler(0, 2.5f, 0));
			Instantiate(boolets, shotM.position, shotM.rotation * Quaternion.Euler(0, -2.5f, 0));

			Instantiate(boolets, shotM.position, shotM.rotation * Quaternion.Euler(0, 20f, 0));
			Instantiate(boolets, shotM.position, shotM.rotation * Quaternion.Euler(0, -20f, 0));

			Instantiate(boolets, shotM.position, shotM.rotation * Quaternion.Euler(0, 35f, 0));
			Instantiate(boolets, shotM.position, shotM.rotation * Quaternion.Euler(0, -35f, 0));

			Instantiate(boolets, shotM.position, shotM.rotation * Quaternion.Euler(0, 50f, 0));
			Instantiate(boolets, shotM.position, shotM.rotation * Quaternion.Euler(0, -50f, 0));

			Instantiate(boolets, shotM.position, shotM.rotation * Quaternion.Euler(0, 65f, 0));
			Instantiate(boolets, shotM.position, shotM.rotation * Quaternion.Euler(0, -65f, 0));
			nextFire = Time.time + fireRate/2;
            GetComponent<AudioSource>().Play();
        }
	}

    void OnDestroy()
    {
        Instantiate(explosion, shotTL.position, shotTL.rotation);
        Instantiate(explosion, shotBL.position, shotBL.rotation);
        Instantiate(explosion, shotM.position, shotM.rotation);
        Instantiate(explosion, shotTR.position, shotTR.rotation);
        Instantiate(explosion, shotBR.position, shotBR.rotation);
        gameController.bossKill = true;
    }

    void FindGC()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<scr_GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot Find 'GameController' Script");
        }
    }



    //IEnumerator FireMissiles()
    //{

    //        Instantiate(missile, shotM.position, shotM.rotation);
    //        yield return new WaitForSeconds(1);
    //}
}
