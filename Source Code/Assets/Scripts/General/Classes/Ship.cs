using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ship 
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       {
	public string ShipName;
	public Stats statistics;
	public SavableStats svStats;
	public bool isUnlocked;
	public Faction faction;

	public Ship()
	{
		svStats = new SavableStats ();
	}

}

[System.Serializable]
public class ShipList 
{
	public List<Ship> ships;

	public ShipList()
	{
		ships = new List<Ship>();
	}

}
[System.Serializable]
public enum Faction
{
	AnacondaSquadron,
	PathOfScarab,
	OrderOfCebula,
	Other
}

