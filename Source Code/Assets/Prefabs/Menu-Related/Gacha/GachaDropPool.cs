using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPool", menuName = "DropPool")]
public class GachaDropPool : ScriptableObject
{
    public int Chance;
    public GachaItem[] items;
	public Rarity rarity;
}


[System.Serializable]
public enum Rarity
{
	Common,
	Uncommon,
	Legendary,
	Mythical
}