using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Engine
{
	public string CodeName;
	public string Name;
	public bool isUnlocked;
	public string SpecialAbility;
	public List<PowerUp> Powerups;
}

[System.Serializable]
public class PowerUp
{
	public string Name;
	public Colour colour;
	private Stats playerStats;

	public void RaiseHP(float amount)
	{
		playerStats.currentHP += amount;
	}
}


[System.Serializable]
public class EngineList 
{
	public List<Engine> engines;

	public EngineList()
	{
		engines = new List<Engine>();
	}

}

public enum Colour
{
	Red,
	Green,
	Blue
}