using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_WaveSystem : MonoBehaviour {
	private int wavesAmount = 1;
	//Values X, Y, Z that will determine where the enemies will spawn.
	//public Vector3 spawnValues;
	public float waveDelay;
	public Wave[] waves;
	public bool active = false;
	private int currentWave;
    public Vector3 bossPosition;
    public Vector3 bossRotation;
    public int enemiesInStage;
    public GameObject boss;
    private bool bossCondition = true;

	// Use this for initialization
	void Start () {
        Wave[] waves = new Wave[wavesAmount];
        wavesAmount = waves.Length;
        currentWave = 1;

		
	}
	
	public IEnumerator WaveSpawner()
	{
		for (int i=0; i < waves.Length; i++) 
		{
			//Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
			Quaternion spawnRotation = Quaternion.identity;
			int currentEnemy = 0 ;
            Debug.Log("We're on wave " + currentWave + " of " + waves.Length);
			foreach(GameObject enemy in waves[currentWave-1].enemyTypes)
			{				
					for(int j= waves[currentWave - 1].enemiesAmounts[currentEnemy]; j > 0; j--)
					{
					Instantiate (enemy, waves[currentWave - 1].EnemySpawnPositions[currentEnemy], spawnRotation);
                    enemiesInStage++;
					Debug.Log ("Hey we got a dude here apparently");
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
		if (active) {
            StartCoroutine (WaveSpawner());
			active = false;
		}

        if (currentWave > waves.Length)
        {
            StopCoroutine(WaveSpawner());
            if (bossCondition)
            {
                SpawnBoss();
                GetComponentInParent<scr_GameController>().HUDcharacter.Play("bossSpawn");
                bossCondition = false;
            }

        }
    }

    void SpawnBoss()
    {
        Instantiate(boss, bossPosition, Quaternion.Euler(bossRotation));
        
    }

}
