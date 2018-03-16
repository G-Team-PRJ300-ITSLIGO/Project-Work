using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_TransitionBetweenLevels : MonoBehaviour {
	public GameObject TransitionCam;
	GameObject MainCam;
	GameObject Controller;
	GameObject Player;
	float timer;

	// Use this for initialization
	void Start () {
		MainCam = GameObject.FindWithTag ("MainCamera");
		Controller = GameObject.FindWithTag ("GameController");
		Player = GameObject.FindWithTag ("Player");
		timer = -999f;
	}
	
	// Update is called once per frame
	void Update () {
		if(MainCam == null)
		{
			MainCam = GameObject.FindWithTag ("MainCamera");
		}
		if(Controller == null)
		{
			Controller = GameObject.FindWithTag ("GameController");
		}
		if(Player == null)
		{
			Player = GameObject.FindWithTag ("Player");
		}

		if(timer != -999f)
		{
			timer -= Time.deltaTime;
		}
		if(Controller.GetComponent<scr_GameController>().bossKill)
		{
			
			Controller.GetComponent<scr_GameController>().enabled = false;
			Controller.GetComponent<scr_GameController>().player.locked = true;
			Controller.GetComponent<scr_WaveSystem>().enabled = false;

			TransitionCam.transform.position = MainCam.transform.position;
			timer = 1;

			Vector3 TargetPos = new Vector3 (transform.position.x, Player.transform.position.y - 1.0f, Player.transform.position.z);

			Vector3 TargetRot = new Vector3 (-25f, 225f, 0f);

			if(transform.position.x != TargetPos.x  & timer <= 0)//First move to X pos.
			{
				transform.position = Vector3.Lerp (transform.position, TargetPos, 0.05f);
				timer = 2;
				if(transform.position.x == TargetPos.x  & timer <= 0)//Once reached X start moving y.
				{
					TargetPos = new Vector3 (Player.transform.position.x + 1.5f, transform.position.y, transform.position.z);

					transform.position = Vector3.Lerp (transform.position, TargetPos, 0.05f);
					timer = 2;

					if(transform.position.y == TargetPos.y  & timer <= 0) //Finally once y is reached move z
					{
						TargetPos = new Vector3 (transform.position.x, transform.position.y, Player.transform.position.z + 2.35f);
						transform.position = Vector3.Lerp (transform.position, TargetPos, 0.05f);

						timer = 2;

						if(transform.position.z == TargetPos.z)
						{
							TargetPos = new Vector3 (Player.transform.position.x + 1.5f,Player.transform.position.y +1.0f, Player.transform.position.z + 2.35f);

							if(transform.position == TargetPos  & transform.rotation.eulerAngles != TargetRot)
							{
								transform.Rotate (TargetRot * Time.deltaTime);

							}
						}

					}
				}
			}

		}
	}



}
