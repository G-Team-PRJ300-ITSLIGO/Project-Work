using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_CreateProfileController : MonoBehaviour {
	public Button btnCreate;
	public Button btnBack;
	//public Button btnNext;

	private scr_GameInstance instance;

	private string username;


	public InputField inputUserName;

	// Use this for initialization
	void Start () {
		GameObject GameInstancer = GameObject.FindGameObjectWithTag ("GameInstancer");

		if (GameInstancer != null) 
		{
			instance = GameInstancer.GetComponent<scr_GameInstance> ();
		}
			
		inputUserName.onValueChanged.AddListener (ValidateTextBox);
		inputUserName.onEndEdit.AddListener (OnUsernameEntered);
		btnCreate.onClick.AddListener (CreateProfile);
		btnBack.onClick.AddListener (GoBack);
	}

	public void Update()
	{
		if(GameObject.Find("InputField Input Caret") != null)
		{
			GameObject.Find("InputField Input Caret").GetComponent<LayoutElement> ().ignoreLayout = false;
			GameObject.Find ("InputField Input Caret").GetComponent<LayoutElement> ().GetComponent<RectTransform> ().pivot = new Vector2(0.5f, 0.3f);
		}

	}

	public void CreateProfile()
	{
		if (!string.IsNullOrEmpty (username)) {
			instance.CreateSimpleProfile (username);
			GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().MainMenu.SetActive (true);
			GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().MainMenu.GetComponent<Menu_MainMenuController> ().ProfileFoundLayout ();
			GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().CreateProfile.SetActive (false);
		}

	}

	private void GoBack()
	{
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().MainMenu.SetActive (true);
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().MainMenu.GetComponent<Menu_MainMenuController> ().NoProfileLayout ();
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().CreateProfile.SetActive (false);
	}

	private void ValidateTextBox(string contents){
		if (!string.IsNullOrEmpty (contents)) {
			btnCreate.interactable = true;
		}
		else
		{
			btnCreate.interactable = false;
		}
	}

	private void OnUsernameEntered(string contents){
		username = contents;
	}
}
