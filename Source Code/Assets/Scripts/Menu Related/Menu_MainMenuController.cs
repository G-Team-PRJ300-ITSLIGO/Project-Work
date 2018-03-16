using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_MainMenuController : MonoBehaviour {
	private scr_GameInstance instance;
	public Text txtCurrentUser;

	public Button btnStartGame;
	public Button btnCheckProfile;
	public Button btnCreateProfile;
	public Button btnEraseProfile;
	public Button btnLottery;
	public Button btnLeaderboards;
	public Button btnSettings;
	public Button btnQuitGame;

	void Start () 
	{
		GameObject GameInstancer = GameObject.FindGameObjectWithTag ("GameInstancer");

		if (GameInstancer != null) 
		{
			instance = GameInstancer.GetComponent<scr_GameInstance> ();
			if (instance.DoesProfileExist ()) 
			{
				if (instance.UserProfile != null) 
				{
					ProfileFoundLayout ();
                    instance.ReloadShips();
                    instance.ReloadEngines();
					txtCurrentUser.text = string.Format ("Hello {0}, What would you like to do?", instance.UserProfile.ProfileName);
				}	
			}
			else
			{
				NoProfileLayout ();
				txtCurrentUser.text = string.Format ("No Profile Found. Create New Profile Above.");
			}
		}

		btnStartGame.onClick.AddListener (StartGame);
		btnCheckProfile.onClick.AddListener (CheckProfile);
		btnEraseProfile.onClick.AddListener (DeleteProfileAndRestart);

		btnLottery.onClick.AddListener (Lottery);
		btnLeaderboards.onClick.AddListener (Leaderboards);
		btnSettings.onClick.AddListener (Settings);

		btnCreateProfile.onClick.AddListener (CreateProfile);
		btnQuitGame.onClick.AddListener (QuitGame);
	}
	void LateUpdate()
	{
		if (instance.DoesProfileExist ()) 
		{
			if (instance.UserProfile != null) 
			{
                instance.ReloadShips();
                instance.ReloadEngines();
                txtCurrentUser.text = string.Format ("Hello {0}, What would you like to do?", instance.UserProfile.ProfileName);
			}	
		}
		else
		{
			txtCurrentUser.text = string.Format ("No Profile Found. Create New Profile Below.");
		}
	}

	private void StartGame()
	{
		DeactivateAllButtons ();
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().MainMenu.SetActive (false);
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().ChooseShip.SetActive (true);
		GameObject.FindWithTag ("MenuCamMover").GetComponent<Menu_CameraMoving> ().GoToNextPoint ();
	}


	private void CheckProfile()
	{
		DeactivateAllButtons ();
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().MainMenu.SetActive (false);
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().DisplayProfile.SetActive (true);
	}

	private void DeleteProfileAndRestart(){
		instance.DeleteProfile ();
		NoProfileLayout ();
		//instance.LoadScene ("MainMenu");

	}

	private void Lottery()
	{
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().MainMenu.SetActive (false);
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().LotteryMenu.SetActive (true);
		GameObject.FindWithTag ("MenuCamMover").GetComponent<Menu_CameraMoving> ().GoToSpecificPoint ("GachaScreen");

	}

	private void Leaderboards()
	{
		DeactivateAllButtons ();
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().MainMenu.SetActive (false);
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().Leaderboards.SetActive (true);	}

	private void Settings()
	{
		DeactivateAllButtons ();
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().MainMenu.SetActive (false);
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().SettingsMenu.SetActive (true);
	}

	private void CreateProfile()
	{
		DeactivateAllButtons ();
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().MainMenu.SetActive (false);
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().CreateProfile.SetActive (true);
	}

	private void QuitGame(){
		instance.Quit ();
	}

	public void DeactivateAllButtons()
	{
		btnStartGame.gameObject.SetActive(false);
		btnCheckProfile.gameObject.SetActive(false);
		btnCreateProfile.gameObject.SetActive(false);
		btnEraseProfile.gameObject.SetActive(false);
		btnLottery.gameObject.SetActive(false);
		btnLeaderboards.gameObject.SetActive(false);
		btnSettings.gameObject.SetActive(false);
		btnQuitGame.gameObject.SetActive(false);
	}

	public void NoProfileLayout()
	{
		btnStartGame.gameObject.SetActive(false);
		btnCheckProfile.gameObject.SetActive(false);
		btnCreateProfile.gameObject.SetActive(true);
		btnEraseProfile.gameObject.SetActive(false);
		btnLottery.gameObject.SetActive(false);
		btnLeaderboards.gameObject.SetActive(false);
		btnSettings.gameObject.SetActive(false);
		btnQuitGame.gameObject.SetActive(true);
	}

	public void ProfileFoundLayout()
	{
		btnStartGame.gameObject.SetActive(true);
		btnCheckProfile.gameObject.SetActive(true);
		btnCreateProfile.gameObject.SetActive(false);
		btnEraseProfile.gameObject.SetActive(true);
		btnLottery.gameObject.SetActive(true);
		btnLeaderboards.gameObject.SetActive(true);
		btnSettings.gameObject.SetActive(true);
		btnQuitGame.gameObject.SetActive(true);
	}
}
