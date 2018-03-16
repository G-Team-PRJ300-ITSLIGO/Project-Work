using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_randomshipInBg : MonoBehaviour {
	public List<GameObject> Ships;
	public float spawnPosX;
	public float SpawnPosYmin;
	public float SpawnPosYmax;
	public float SpawnPosZ;
	public float SpawnWait;

	// Use this for initialization
	void Start () {
		StartCoroutine (SpawnShips());
	}

	IEnumerator SpawnShips()
	{
		yield return new WaitForSeconds (SpawnWait);
			while(true)
			{
				for (int i=0;i < Ships.Count; i++) 
					{
					GameObject ship = Ships[Random.Range (0, Ships.Count - 1)]; 
				Vector3 spawnPosition = new Vector3 (spawnPosX, Random.Range(SpawnPosYmin,SpawnPosYmax) , SpawnPosZ);
					Quaternion spawnRotation = ship.GetComponent<Rigidbody>().transform.rotation;
					Instantiate (ship, spawnPosition,spawnRotation);
					yield return new WaitForSeconds (SpawnWait);
				}
			}
		}
}
