using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_PowerUpMethods : MonoBehaviour
{

    public RuntimeAnimatorController[] controllers;
    public void FindMethod(ref Action<string, scr_playerBehaviour> pizza, string powerup, ref Animator animator)
    {
        switch (powerup)
        {
            case "GC":
                pizza = GannonCannon;
                animator.runtimeAnimatorController = controllers[0];
                break;
            case "DDD":
                animator.runtimeAnimatorController = controllers[1];
                pizza = DefenceDemolisher;
                break;
            case "MMC":
                pizza = MatterConverter;
                animator.runtimeAnimatorController = controllers[2];
                break;
            case "KCDC":
                pizza = CheatDeathContraption;
                animator.runtimeAnimatorController = controllers[3];
                break;
            case "SS":
                pizza = SzczodrowskiStrike;
                animator.runtimeAnimatorController = controllers[4];
                break;
        }
    }




    void GannonCannon(string powerupCollected, scr_playerBehaviour player)
    {
        float increase = 0.9f;
        int hpUp = 100;
        switch (powerupCollected)
        {
            case "Blue":
                player.stats.fireRate *= increase;
                if (player.powerTimer <= 0f)
                    player.originalFireRate = player.stats.fireRate;
                if (player.specialCollect >= 4)
                {
                    player.weaponStrength++;
                    player.stats.Damage *= increase;
                    player.specialCollect = 0;
                }
                break;
            case "Red":
                player.weaponStrength += 1;
                break;
            case "Green":
                if (player.stats.currentHP < player.stats.HP)
                    player.stats.currentHP += hpUp;
                if (player.stats.currentHP > player.stats.HP)
                    player.stats.currentHP = player.stats.HP;
                break;
            case "Active":
                player.powerTimer = 5f;
                player.stats.fireRate = 0.02f;
                player.powerUpMeter = 0;
                GetComponentInParent<scr_GameController>().HUDcharacter.Play("Ultimate");
                break;
            case "Deactivate":
                Debug.Log("PowerUp Gone");
                player.stats.fireRate = player.originalFireRate;
                break;
            case "Death":
                GetComponentInParent<scr_GameController>().GameOver();
                player.locked = true;
                player.rb.useGravity = true;
                break;
            default:
                Debug.Log("Oops that's not a powerup");
                break;
        }
    }

    void DefenceDemolisher(string powerupCollected, scr_playerBehaviour player)
    {
        float increase = 1.05f;
        int hpUp = 100;
        switch (powerupCollected)
        {
            case "Blue":
                player.stats.Damage *= increase;
                player.specialCollect++;
                if (player.specialCollect >= 3)
                {
                    player.weaponStrength++;
                    player.stats.Damage /= increase;
                    player.specialCollect = 0;
                }
                break;
            case "Red":
                player.weaponStrength += 1;
                break;
            case "Green":
                if (player.stats.currentHP < player.stats.HP)
                    player.stats.currentHP += hpUp;
                if (player.stats.currentHP > player.stats.HP)
                    player.stats.currentHP = player.stats.HP;
                break;
            case "Active":
                player.powerTimer = 1f;
                if (player.powerUpMeter > 0)
                    for (int i = 0; i < 36; i++)
                    {
                        Instantiate(player.WeaponTypes[1], player.CenterCannonTrans.position, Quaternion.Euler(0f, (float)player.powerUpMeter * i, 0f));

                    }
                player.powerUpMeter -= 20;
                GetComponentInParent<scr_GameController>().HUDcharacter.Play("Ultimate");
                break;
            case "Deactivate":
                break;
            case "Death":
                GetComponentInParent<scr_GameController>().GameOver();
                player.locked = true;
                player.rb.useGravity = true;
                break;
            default:
                Debug.Log("Oops that's not a powerup");
                break;
        }
    }

    void MatterConverter(string powerupCollected, scr_playerBehaviour player)
    {
        float increaseCHAR = 1.2f;
        switch (powerupCollected)
        {
            case "Blue":
                player.increase *= increaseCHAR;
                if (player.specialCollect >= 5)
                {
                    player.weaponStrength++;
                    player.specialCollect = 0;
                }
                break;
            case "Red":
                player.powerUpMeter += 50;
                break;
            case "Green":
                player.increase *= increaseCHAR;
                break;
            case "Active":
                player.powerTimer = 5f;
                player.weaponShot = 1;
                player.powerUpMeter = 0;
                GetComponentInParent<scr_GameController>().HUDcharacter.Play("Ultimate");
                break;
            case "Deactivate":
                player.weaponShot = 0;
                break;
            case "Death":
                GetComponentInParent<scr_GameController>().GameOver();
                player.locked = true;
                player.rb.useGravity = true;
                break;
            default:
                Debug.Log("Oops that's not a powerup");
                break;
        }
    }

    void CheatDeathContraption(string powerupCollected, scr_playerBehaviour player)
    {
        float increaseDAM = 1.1f;
        switch (powerupCollected)
        {
            case "Blue":
                player.stats.Damage *= increaseDAM;
                if (player.specialCollect >= 5)
                {
                    player.weaponStrength++;
                    player.specialCollect = 0;
                }
                break;
            case "Red":
                player.weaponStrength++;
                break;
            case "Green":
                player.stats.fireRate /= increaseDAM;
                break;
            case "Active":
                break;
            case "Death":
                int random = UnityEngine.Random.Range(0, 2);
                Debug.Log(random);
                if (random == 1)
                {
                    player.stats.currentHP = 0;
                    player.powerTimer = 1f;
                    player.stats.currentHP += player.stats.HP / 3;
                    GetComponentInParent<scr_GameController>().HUDcharacter.Play("Ultimate");
                }
                else
                {
                    player.powerUpMeter = 0;
                GetComponentInParent<scr_GameController>().GameOver();
                player.locked = true;
                player.rb.useGravity = true;
                }
                break;
            default:
                Debug.Log("Oops that's not a powerup");
                break;
        }
    }
    void SzczodrowskiStrike(string powerupCollected, scr_playerBehaviour player)
    {
        float increase = 1.05f;
        int hpUp = 100;
        switch (powerupCollected)
        {
            case "Blue":
                player.stats.fireRate /= increase;
                player.specialCollect++;
                if (player.specialCollect >= 5)
                {
                    player.weaponStrength++;
                    player.stats.Damage /= increase;
                    player.specialCollect = 0;
                }
                break;
            case "Red":
                player.weaponStrength += 1;
                break;
            case "Green":
                if (player.stats.currentHP < player.stats.HP)
                    player.stats.currentHP += hpUp;
                if (player.stats.currentHP > player.stats.HP)
                    player.stats.currentHP = player.stats.HP;
                break;
            case "Active":
                player.powerTimer = 1f;
                Instantiate(player.WeaponTypes[2], new Vector3(8f, 0f, -15f), Quaternion.Euler(0f, 0f, 0f));
                Instantiate(player.WeaponTypes[2], new Vector3(-8f, 0f, -15f), Quaternion.Euler(0f, 0f, 0f));
                Instantiate(player.WeaponTypes[2], new Vector3(4f, 0f, -15f), Quaternion.Euler(0f, 0f, 0f));
                Instantiate(player.WeaponTypes[2], new Vector3(-4f, 0f, -15f), Quaternion.Euler(0f, 0f, 0f));
                Instantiate(player.WeaponTypes[2], new Vector3(2f, 0f, -15f), Quaternion.Euler(0f, 0f, 0f));
                Instantiate(player.WeaponTypes[2], new Vector3(-2f, 0f, -15f), Quaternion.Euler(0f, 0f, 0f));
                player.powerUpMeter = 0;
                GetComponentInParent<scr_GameController>().HUDcharacter.Play("Ultimate");
                break;
            case "Deactivate":
                break;
            case "Death":
                GetComponentInParent<scr_GameController>().GameOver();
                player.locked = true;
                player.rb.useGravity = true;
                break;
            default:
                Debug.Log("Oops that's not a powerup");
                break;
        }
    }
}