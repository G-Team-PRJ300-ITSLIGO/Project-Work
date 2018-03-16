using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newItem",menuName ="Item")]
public class GachaItem : ScriptableObject
{
    public int dropChance;
    public GameObject thing;
    public new string name;
    public string type;
}
