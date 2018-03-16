using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_DisplayProfileController : MonoBehaviour {
	public Text txPlayerName;
	public Text txShipsUnlocked;
	public Text txTotalScore;
	public Text txCrystalsAmount;
	public Text txCurrentLevel;
	public Text txLastShip;
	public Text txCDRatio;

	private scr_GameInstance instance;

	public Button btnGoBack ;
	public Button btnEditProfileName;
	public Button btnInventory;

	public InputField PlayerNameInput;
	public Button btnConfirmEdit;

	string username;

	// Use this for initialization
	void Start () {
		GameObject gameInstancer = GameObject.FindGameObjectWithTag ("GameInstancer");

		if (gameInstancer != null)
		{
			instance = gameInstancer.GetComponent<scr_GameInstance> ();
		}
		DefaultProfileView ();

		PlayerNameInput.onValueChanged.AddListener (ValidateTextBox);
		PlayerNameInput.onEndEdit.AddListener (OnUsernameEntered);

		btnGoBack.onClick.AddListener (GoBack);
		btnEditProfileName.onClick.AddListener (EditProfileName);
		btnInventory.onClick.AddListener (Inventory);
		btnConfirmEdit.onClick.AddListener (ConfirmEdit);

		LoadValues ();
	}

	void Update()
	{
		if(GameObject.Find("Input_PlayerName Input Caret") != null)
		{
			GameObject.Find("Input_PlayerName Input Caret").GetComponent<LayoutElement> ().ignoreLayout = false;
			GameObject.Find ("Input_PlayerName Input Caret").GetComponent<LayoutElement> ().GetComponent<RectTransform> ().pivot = new Vector2(0.5f, 0.3f);
		}
	}
	
	void GoBack()
	{
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().MainMenu.SetActive (true);
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().MainMenu.GetComponent<Menu_MainMenuController> ().ProfileFoundLayout ();
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().DisplayProfile.SetActive (false);
	}

	void ConfirmEdit()
	{
		if (!string.IsNullOrEmpty (username)) 
		{
			DefaultProfileView ();
			instance.UserProfile.ProfileName = PlayerNameInput.text;
			instance.SaveUserProfile ();
			LoadValues ();
		}
	}


	void EditProfileName()
	{
		EditProfileView ();
	}

	void Inventory()
	{
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().ShipInventory.SetActive (true);
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().DisplayProfile.SetActive (false);
		GameObject.FindWithTag ("MenuCamMover").GetComponent<Menu_CameraMoving> ().GoToSpecificPoint ("InventoryScreen");

	}

	void LoadValues()
	{
		
		txPlayerName.text = instance.UserProfile.ProfileName;
		txShipsUnlocked.text = string.Format("{0}/{1}", instance.UserProfile.UnlockedShips.ToString(), instance.ShipsAmount.ToString());
		txTotalScore.text = instance.UserProfile.svStats.Score.ToString();
		txCrystalsAmount.text = instance.UserProfile.Crystals.ToString();
		txCurrentLevel.text = instance.UserProfile.CurrentLevelName;
		if (instance.UserProfile.playerShip.ShipName != null) {
			txLastShip.text = instance.UserProfile.playerShip.ShipName;
		} else {
			txLastShip.text = "None.";
		}
            instance.UserProfile.deaths = 1;
        txCDRatio.text = string.Format((instance.UserProfile.attempts + " : "+ instance.UserProfile.deaths).ToString());
    }

	void DefaultProfileView()
	{
		btnConfirmEdit.gameObject.SetActive (false);
		PlayerNameInput.gameObject.SetActive (false);
		txPlayerName.gameObject.SetActive (true);
		btnEditProfileName.gameObject.SetActive (true);
		btnGoBack.gameObject.SetActive (true);
		btnInventory.gameObject.SetActive (true);

	}

	void EditProfileView()
	{
		btnConfirmEdit.gameObject.SetActive (true);
		PlayerNameInput.gameObject.SetActive (true);
		txPlayerName.gameObject.SetActive (false);
		btnEditProfileName.gameObject.SetActive (false);
		btnGoBack.gameObject.SetActive (false);
		btnInventory.gameObject.SetActive (false);
	}

	private void ValidateTextBox(string contents){
		if (!string.IsNullOrEmpty (contents)) {
			btnConfirmEdit.interactable = true;
		}
		else
		{
			btnConfirmEdit.interactable = false;
		}
	}

	private void OnUsernameEntered(string contents){
		username = contents;
	}
}
