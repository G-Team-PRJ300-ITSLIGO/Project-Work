using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Profile{

	public string ProfileName;
	public SavableStats svStats;
	public Stats playerStats;
	public Ship playerShip;
	public Engine playerEngine;
	public int UnlockedShips;
	public int Crystals;
    public float attempts;
    public float deaths;
	public string CurrentLevelName = "Level One";

	public Profile()
	{
		
	}
	public Profile(string name)
	{
		ProfileName = name;
        attempts = 0;
        deaths = 0;
	}

	public Profile(string name, string ShipType, Stats stats, SavableStats saveStats, Ship Ship,int crystals)
	{
		ProfileName = name;
		playerStats = stats;
		svStats = saveStats; 
		playerShip = Ship;
		playerShip.ShipName = ShipType;
        Crystals = crystals;
	}
}
