using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave{
	//Which enemies will appear in this wave?
	public GameObject[] enemyTypes;
	//Amount of the enemies that will appear of each type?
	public int[] enemiesAmounts;
	//Delay that should happen between each enemy.
	public float enemyDelay;
	public Vector3[] EnemySpawnPositions;

}
