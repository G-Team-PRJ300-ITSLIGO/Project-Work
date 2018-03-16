using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{

    public float HP = 100;
    public float currentHP;
    public float Damage = 100;
    public int ScoreValue = 100;
    public float fireRate = 0.1f;
    public float speed = 0f;
    public float difficulty = 1;

    public float Difficulty()
    {
        difficulty = 1;
        if (HP > 150)
        {
            difficulty -= (float)Math.Round((HP - 150) / 25,2) * 0.05f;
        }
        else
        {
            difficulty += Mathf.Round((150 - HP) / 10) * 0.05f;
        }
        if (Damage > 100)
        {
            difficulty -= Mathf.Round((Damage - 100) / 25) * 0.05f;
        }
        else
        {
            difficulty -= Mathf.Round((100-Damage) / 25) * 0.05f;
        }
        if (speed > 8)
        {
            difficulty -= Mathf.Round((speed - 8) / 25) * 0.05f;
        }
        else
        {
            difficulty -= Mathf.Round((8 - speed) / 25) * 0.05f;
        }
        if (difficulty < 0.45f)
            difficulty = 0.45f;
        return difficulty;
    }
}
