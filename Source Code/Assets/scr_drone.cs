using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_drone : MonoBehaviour {

    float timer;
    public GameObject otherDrone;

    private AudioSource audioSource;
    public GameObject shot;
    scr_GameController gameController;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Fire", 1f, Random.Range(GetComponent<scr_enemyStats>().stats.fireRate, GetComponent<scr_enemyStats>().stats.fireRate * 2));
    }

    void Fire()
    {
        if(tag == "Left")
        Instantiate(shot, transform.position,Quaternion.Euler(0f,90f,0f));
        else
        Instantiate(shot, transform.position, Quaternion.Euler(0f, -90f, 0f));

        audioSource.Play();
    }
    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
        if(timer >= 5f)
        {
            Instantiate(otherDrone, otherDrone.transform.position,otherDrone.transform.rotation);
            Destroy(gameObject);
        }
        if (gameController.bossKill)
            Destroy(gameObject);
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
