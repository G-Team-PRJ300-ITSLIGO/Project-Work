using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Menu_SettingsController : MonoBehaviour {
	public Toggle fullscreenMode;
	public Toggle vSyncEnabled;
	public Toggle hqTerrains;
	public Button btnBack;

	public AudioMixer audioMix;

	// Use this for initialization
	void Start () {
		fullscreenMode.onValueChanged.AddListener(delegate {
			ChangeScreenMode();
		});
		vSyncEnabled.onValueChanged.AddListener (delegate {
			SetVSync();
		});
		hqTerrains.onValueChanged.AddListener (delegate{
			SetHQTerrains();
		});

		btnBack.onClick.AddListener (GoBack);
	}
	 void ChangeScreenMode()
	{
		Screen.fullScreen = fullscreenMode.isOn;
	}

	 void SetVSync ()
	{
		if (vSyncEnabled.isOn) {
			QualitySettings.vSyncCount = 1;
		}
		else {
			QualitySettings.vSyncCount = 0;
		}
	}

	 void SetHQTerrains()
	{
		QualitySettings.softVegetation = hqTerrains.isOn;
	}

	public void SetVolume (float volume)
	{
		audioMix.SetFloat ("volume", volume);
	}
	
	public void SetGraphicsQuality(int QualityIndex)
	{
		QualitySettings.SetQualityLevel (QualityIndex);
	}

	public void GoBack()
	{
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().MainMenu.SetActive (true);
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().MainMenu.GetComponent<Menu_MainMenuController> ().ProfileFoundLayout ();
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().SettingsMenu.SetActive (false);
	}
}
