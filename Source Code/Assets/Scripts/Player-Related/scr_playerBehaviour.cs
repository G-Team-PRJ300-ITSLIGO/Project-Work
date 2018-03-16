using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class scr_playerBehaviour : MonoBehaviour 
{
	public float tilt;
	public Boundary boundingArea;
	public Rigidbody rb;
	public bool locked = true;
    public Stats stats;
    private float countdown = 2.5f;
    public GameObject explosion;
    public bool dead;

    public Action<string, scr_playerBehaviour> powerupAction;
    private scr_GameController gameController;
    public float increase;
    public GameObject[] WeaponTypes;
	public Transform LeftCannonTrans;
	public Transform RightCannonTrans;
	public Transform CenterCannonTrans;
	public int weaponStrength;
    public int specialCollect;
    public GameObject winCamera;
    public int weaponShot;
    public float powerUpMeter = 0f;
    private bool powerActive;
    public float powerTimer = -1f;
    //fireRateThings
	private float nextFire = 0.0f;
    public float originalFireRate;
    public float bonusFireRate;
    //for laser
    LineRenderer laserLine;



    void Start()
    {
        weaponShot = 0;
        increase = 0.5f;
        stats.currentHP = stats.HP;
        originalFireRate = stats.fireRate;
       
        laserLine = GetComponent<LineRenderer>();
        laserLine.startWidth = .5f;
        laserLine.endWidth = .5f;
        laserLine.enabled = false;

        FindGC();
    }
	// Use this for initialization
	void Update ()
	{
        foreach(GameObject bullet in WeaponTypes)
        {
            if(bullet.tag != "SS")
            bullet.GetComponent<BoltCollision>().stats.Damage = stats.Damage;   
        }

        if (rb.useGravity)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0f)
            {
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
            transform.Rotate(transform.right, 30 * Time.deltaTime);
            transform.Rotate(transform.up, 60 * Time.deltaTime);
        }
		if (locked)
			return;

            if (Input.GetButton("Fire1") && Time.time > nextFire)
            {
			FireWeapon ();
                nextFire = Time.time + stats.fireRate;
                if (powerUpMeter < 100 && !powerActive && powerTimer == -1f)
                {
                    powerUpMeter += increase;
                }

            }

            if (Input.GetButton("Fire2") && powerUpMeter > 0 && powerActive)
            {
            powerupAction("Active", this);


        }
            if ((powerTimer <= 0 && powerTimer > -1))
            {
            powerUpMeter = 0;
            powerActive = false;
            powerupAction("Deactivate", this);
        }
            if(powerUpMeter == 100) powerActive = true;

        if (powerUpMeter > 100)
            {
                powerUpMeter = 100;

            }

        if(powerTimer > -1f)
        {
            powerTimer -= Time.deltaTime;
        }
        else
        {
            powerTimer = -1f;
        }

        if (weaponStrength > 5)
        {
            weaponStrength = 5;
        }
        //Check if player's supposed to be dead
        if (dead)
        {
            gameController.GameOver();
            locked = true;
            rb.useGravity = true;
        }

    }
    //
    void FindGC()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<scr_GameController>();
            if (gameControllerObject.GetComponent<scr_GameController>().powerupAction != null)
                powerupAction = gameControllerObject.GetComponent<scr_GameController>().powerupAction;
        }
    }

    // Update is called once per frame
    void FixedUpdate () 
	{
        if (!locked)
        {
            rb = GetComponent<Rigidbody>();

            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            rb.velocity = movement * stats.speed;

            rb.position = new Vector3
                (
                    Mathf.Clamp(rb.position.x, boundingArea.xMin, boundingArea.xMax),
                    0.0f,
                    Mathf.Clamp(rb.position.z, boundingArea.zMin, boundingArea.zMax)
                );

            rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
        }
	}
    void OnDestroy()
    {
        gameController.HUDcharacter.Play("death");
        gameController.gameOver = true;
        Destroy(this.laserLine.material);
    }



	void FireWeapon()
	{
        StopCoroutine("FireLaser");
        laserLine.enabled = false;
        switch (weaponShot)
        {
            case 0:
                #region normalBullet
                switch (weaponStrength)
                {
                    case 1:
                        Instantiate(WeaponTypes[0], CenterCannonTrans.position, Quaternion.Euler(0f, 0f, 0f));
                        GetComponent<AudioSource>().Play();
                        break;
                    case 2:
                        Instantiate(WeaponTypes[0], LeftCannonTrans.position, Quaternion.Euler(0f, 0f, 0f));
                        Instantiate(WeaponTypes[0], RightCannonTrans.position, Quaternion.Euler(0f, 0f, 0f));
                        GetComponent<AudioSource>().Play();
                        break;
                    case 3:
                        Instantiate(WeaponTypes[0], LeftCannonTrans.position, Quaternion.Euler(0f, 0f, 0f));
                        Instantiate(WeaponTypes[0], CenterCannonTrans.position, Quaternion.Euler(0f, 0f, 0f));
                        Instantiate(WeaponTypes[0], RightCannonTrans.position, Quaternion.Euler(0f, 0f, 0f));
                        GetComponent<AudioSource>().Play();
                        break;
                    case 4:
                        Instantiate(WeaponTypes[0], LeftCannonTrans.position, Quaternion.Euler(0f, 0f, 0f));
                        Instantiate(WeaponTypes[0], RightCannonTrans.position, Quaternion.Euler(0f, 0f, 0f));
                        Instantiate(WeaponTypes[0], LeftCannonTrans.position, Quaternion.Euler(0f, -1.5f, 0f));
                        Instantiate(WeaponTypes[0], RightCannonTrans.position, Quaternion.Euler(0f, 1.5f, 0f));
                        GetComponent<AudioSource>().Play();
                        break;
                    case 5:
                        Instantiate(WeaponTypes[0], LeftCannonTrans.position, Quaternion.Euler(0f, -15f, 0f));
                        Instantiate(WeaponTypes[0], RightCannonTrans.position, Quaternion.Euler(0f, 15f, 0f));
                        Instantiate(WeaponTypes[0], LeftCannonTrans.position, Quaternion.Euler(0f, 0f, 0f));
                        Instantiate(WeaponTypes[0], RightCannonTrans.position, Quaternion.Euler(0f, 0f, 0f));
                        Instantiate(WeaponTypes[0], LeftCannonTrans.position, Quaternion.Euler(0f, -5f, 0f));
                        Instantiate(WeaponTypes[0], RightCannonTrans.position, Quaternion.Euler(0f, 5f, 0f));
                        GetComponent<AudioSource>().Play();
                        break;
                    default:
                        break;

                }
                #endregion
                break;
            case 1:
                StartCoroutine("FireLaser");
                break;
        }

    }
    IEnumerator FireLaser()
    {
        laserLine.enabled = true;
        while (Input.GetButton("Fire1"))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            laserLine.SetPosition(0, ray.origin);
            if (Physics.Raycast(ray, out hit, 30))
            {
                if (hit.collider.tag.Contains("Enemy"))
                {
                    laserLine.SetPosition(1, hit.point);
                    Instantiate(WeaponTypes[0], hit.point, Quaternion.Euler(0f, 0f, 0f));
                    stats.currentHP += stats.Damage / 10;
                }
            }
            else
            {
                laserLine.SetPosition(1, ray.GetPoint(30));
            }
            yield return null;

            //if (Physics.Raycast(transform.position, transform.forward, out hit))
            //{
            //    if (hit.collider)
            //    {
            //        laserLine.SetPosition(1, hit.point);
            //    }
            //}
            //else laserLine.SetPosition(1, transform.forward * 5000);
        }
        laserLine.enabled = false;
    }


}
