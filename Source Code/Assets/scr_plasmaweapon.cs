using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_plasmaweapon : MonoBehaviour
{


    private AudioSource audioSource;
    public GameObject shot;
    public GameObject boss;
    public Transform shotSpawn;
    public Transform shotSpawn2;
    public Transform shotSpawn3;
    public Transform shotSpawn4;
    public float delay;
    private scr_GameController gameController;


    void Start()
    {
        FindGC();
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Fire", delay, Random.Range(GetComponent<scr_enemyStats>().stats.fireRate, GetComponent<scr_enemyStats>().stats.fireRate * 2));
    }

    void Fire()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        Instantiate(shot, shotSpawn2.position, shotSpawn2.rotation);
        Instantiate(shot, shotSpawn3.position, shotSpawn3.rotation);
        Instantiate(shot, shotSpawn4.position, shotSpawn4.rotation);
        audioSource.Play();
    }

    void Update()
    {
        if (GetComponent<scr_enemyStats>().stats.currentHP <= 0)
        {
            gameController.bossKill = true;
            Destroy(boss);
        }
    }
    
    void OnDestroy()
    {
        gameController.bossKill = true;
        Destroy(boss);
    }

    void FindGC()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<scr_GameController>();
        }
    }

}
