using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Lore
{
	public string shipName;
	public string loreDesc;
}

[System.Serializable]
public class LoreList
{
	public List<Lore> Lores;
}
