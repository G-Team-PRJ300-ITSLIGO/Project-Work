using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_GeneralMenuController : MonoBehaviour 
{
	public GameObject MainMenu;
	public GameObject DisplayProfile;
	public GameObject ShipInventory;
	public GameObject ChooseShip;
	public GameObject ChoosePowerUp;
	public GameObject CreateProfile;
	public GameObject SettingsMenu;
	public GameObject Leaderboards;
	public GameObject LotteryMenu;

	private scr_GameInstance instance;


	void Awake()
	{
		DeactivateAllMenus ();
	}

	// Use this for initialization
	void Start () {
		GameObject GameInstancer = GameObject.FindGameObjectWithTag ("GameInstancer");

		if (GameInstancer != null) 
		{
			instance = GameInstancer.GetComponent<scr_GameInstance> ();
		}

		MainMenu.SetActive (true);

	}


	void DeactivateAllMenus()
	{
		MainMenu.SetActive (false);
		DisplayProfile.SetActive (false);
		ShipInventory.SetActive (false);
		ChooseShip.SetActive (false);
		ChoosePowerUp.SetActive (false);
		CreateProfile.SetActive (false);
		SettingsMenu.SetActive (false);
		Leaderboards.SetActive (false);
		LotteryMenu.SetActive (false);
	}
}
