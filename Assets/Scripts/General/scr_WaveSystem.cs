using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_WaveSystem : MonoBehaviour {
	private int wavesAmount;
	//Values X, Y, Z that will determine where the enemies will spawn.
	//public Vector3 spawnValues;
	public float waveDelay;
	public Wave[] waves;
	private int currentWave;

	// Use this for initialization
	void Start () {
		Wave[] waves = new Wave[wavesAmount];
		currentWave = 1;
		StartCoroutine (WaveSpawner ());
		wavesAmount = waves.Length;
	}
	
	IEnumerator WaveSpawner()
	{
		for (int i=0; i < waves.Length; i++) 
		{
			//Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
			Quaternion spawnRotation = Quaternion.identity;
			int currentEnemy = 0 ;
			foreach(GameObject enemy in waves[currentWave-1].enemyTypes)
			{				
					for(int j= waves[currentWave - 1].enemiesAmounts[currentEnemy]; j > 0; j--)
					{
					Instantiate (enemy, waves[currentWave - 1].EnemySpawnPositions[currentEnemy], spawnRotation);
						//spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
						yield return new WaitForSeconds (waves [currentWave - 1].enemyDelay);
					}
				currentEnemy++;
			}
			yield return new WaitForSeconds (waveDelay);
			Debug.Log ("End of Wave " + currentWave);
			currentWave++;
		}
	}
	void Update(){
		if(currentWave > wavesAmount){
			StopCoroutine (WaveSpawner ());
			GetComponentInParent<scr_GameController> ().gameOver = true;
		}
	}
}
