using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Boss1 : MonoBehaviour {
    //ToDo: Make seperate class for boss Transforms
    public GameObject boolets;
    private scr_GameController gameController;
    private Rigidbody rb;
    public Transform shotTL;
    public Transform shotBL;
    public Transform shotM;
    public Transform shotTR;
    private AudioSource[] audios;
    public Transform shotBR;
    private float nextFire;
    private float nextFireBullets;
    public GameObject explosion;
    public Stats stats;
    private float time = 0.0f;
    [SerializeField]
    private float oldRotate = 0;
    public float shotRotate;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        audios = GetComponents<AudioSource>();
        stats = GetComponent<scr_enemyStats>().stats;
        stats.currentHP = stats.HP;
        FindGC();
       
    }
	
	// Update is called once per frame
	void Update () {


        time += Time.deltaTime;
      
		if (time <= 5f) {
			Phase1 ();

		} else if (time <= 10f) {
			Phase2 (0.6f);
		} else if (time <= 13f) {
			oldRotate = 0f;
			shotRotate = 15f;
			Phase3 (-30);
		} else if (time <= 16f) {
			Phase5 ();
		}
		else if (time <= 19f) {
			Phase3 (-30);
		} else if (time <= 24f) {
			Phase4 (0.6f);
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
		rb.position += transform.right * (-2.85f * Time.deltaTime);
       
    }
    void Phase2(float speed)
    {
		rb.position += transform.right * -speed * Time.deltaTime;

        if (Time.time > nextFireBullets)
        { 
            Instantiate(boolets, shotTL.position, shotTL.rotation * Quaternion.Euler(0, this.oldRotate, 0));
        Instantiate(boolets, shotBL.position, shotBL.rotation * Quaternion.Euler(0, this.oldRotate, 0));
        nextFireBullets = Time.time + stats.fireRate;
            this.oldRotate += this.shotRotate;
            audios[0].Play();
        }
      
    }
    void Phase4(float speed)
    {
		rb.position += transform.right * -speed * Time.deltaTime;

        if (Time.time > nextFireBullets)
        {
            Instantiate(boolets, shotTR.position, shotTR.rotation * Quaternion.Euler(0, this.oldRotate, 0));
            Instantiate(boolets, shotBR.position, shotBR.rotation * Quaternion.Euler(0, this.oldRotate, 0));
            nextFireBullets = Time.time + stats.fireRate;
            this.oldRotate += this.shotRotate;
            audios[0].Play();
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
			nextFire = Time.time + stats.fireRate/2;
            audios[0].Play();
        }
	}

    void OnDestroy()
    {
        Instantiate(explosion, shotTL.position, shotTL.rotation);
        Instantiate(explosion, shotBL.position, shotBL.rotation);
        Instantiate(explosion, shotM.position, shotM.rotation);
        Instantiate(explosion, shotTR.position, shotTR.rotation);
        Instantiate(explosion, shotBR.position, shotBR.rotation);
        audios[1].Play();
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



}
