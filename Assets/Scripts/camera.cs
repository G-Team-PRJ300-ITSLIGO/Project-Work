using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {

	public Transform obj;
	public Transform end;
	private scr_GameController gameController;
	private scr_WaveSystem waves;
	private scr_playerBehaviour player;
	private float time;
	private float step;
	// Use this for initialization
	void Start () {
		step = 4 * Time.deltaTime;
		FindGC();
		FindPlayer();
	}
	
	// Update is called once per frame
	void Update () {
		step = 4 * Time.deltaTime;
		time += Time.deltaTime;
		this.GetComponent<Camera> ().orthographicSize = Mathf.Lerp (0.1f, 10f, time/10);
		if (time <= 4f) {
			transform.RotateAround (obj.position, Vector3.up, 90 * Time.deltaTime);
		}
		else if (time <= 7f) {

			transform.RotateAround (obj.position, Vector3.right, 30 * Time.deltaTime);
		}

		else if (time <= 10f) {
            transform.rotation = Quaternion.Euler(90f, 0f, 0f);
            transform.position = Vector3.MoveTowards (transform.position, end.position, step);
		}
		else if (time >= 10f) {

			player.locked = false;
			gameController.enabled = true;
			waves.active = true;
			Destroy (gameObject);
		}

	}

	void FindGC()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) 
		{
			gameController = gameControllerObject.GetComponent<scr_GameController> ();
			waves = gameControllerObject.GetComponent<scr_WaveSystem> ();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot Find 'GameController' Script");
		}
	}

	void FindPlayer()
	{
		GameObject playerObject = GameObject.FindWithTag ("Player");
		if (playerObject != null) 
		{
			player = playerObject.GetComponent<scr_playerBehaviour>();
		}
		if (player == null)
		{
			Debug.Log ("Cannot Find 'GameController' Script");
		}
	}



}
