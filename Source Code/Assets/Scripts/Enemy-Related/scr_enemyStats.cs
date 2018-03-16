using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_enemyStats : MonoBehaviour {

    public Stats stats;
    public scr_GameController gameController;

    void Start()
    {
        stats.currentHP = stats.HP;
        FindGC();
    }

    void OnEnables()
    {
        stats.currentHP = stats.HP;
    }

    void OnDestroy()
    {
        if (stats.currentHP <= 0)
        {
            gameController.enemiesKilled++;
            gameController.AddScore(stats.ScoreValue);
        }
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
