using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Boss2Logic : MonoBehaviour {

    public float rotateSpeed;
    private scr_GameController gameController;
    public GameObject shield;
    public GameObject bullet;
	public Transform ShotSpawn;
	public Transform ShotSpawn2;
    public float MaxShieldDownTime;
    private float timer;
    private enum bossPhases { ShieldMode = 1, ShieldDown, AssaultMode, ShieldUp }
    private int bossPhase = 1;
    private float fireRate;
    private float nextFire = 0f;
    // Use this for initialization
    void Start()
    {
        FindGC();
        fireRate = GetComponent<scr_enemyStats>().stats.fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        switch (bossPhase)
        {
            case (int)bossPhases.ShieldMode:
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
			shield.transform.Rotate(transform.up * (rotateSpeed/2) * Time.deltaTime, Space.World);
                break;
            case (int)bossPhases.ShieldDown:
                Debug.Log(transform.rotation.eulerAngles.x);
                transform.Rotate(transform.right * (rotateSpeed / 12) * Time.deltaTime, Space.World);
                if (transform.rotation.eulerAngles.x >= 88f)
                {
                    Debug.Log("Yeah baby");
                    transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                    bossPhase = (int)bossPhases.AssaultMode;
                }
                break;
            case (int)bossPhases.AssaultMode:
                timer += Time.deltaTime;
                if (nextFire < Time.time)
                {
				Instantiate(bullet, ShotSpawn.position, ShotSpawn.rotation);
				Instantiate(bullet, ShotSpawn2.position,  ShotSpawn2.rotation);
                    GetComponent<AudioSource>().Play();
                    nextFire = Time.time + fireRate;
                }
                break;
            case (int)bossPhases.ShieldUp:
                timer = 0f;
                transform.Rotate(transform.right * -(rotateSpeed / 12) * Time.deltaTime, Space.World);
                if (transform.rotation.eulerAngles.x <= 2)
                {
                    Debug.Log("Yeah baby");
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    shield.SetActive(true);
                }
                break;

        }
    }

    void LateUpdate()
    {
        if (timer >= MaxShieldDownTime)
            bossPhase = (int)bossPhases.ShieldUp;
        if (!shield.GetComponent<scr_shield>().alive && shield.active)
        {
            shield.SetActive(false);
            bossPhase = (int)bossPhases.ShieldDown;
        }
        else if (shield.GetComponent<scr_shield>().alive)
            bossPhase = (int)bossPhases.ShieldMode;
    }

    void OnDestroy()
    {
        Destroy(shield);
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
