using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class Menu_ChoosePowerupController : MonoBehaviour {
	private scr_GameInstance instance;

	private EngineList Engines;
	private Engine engine;

	//Engine Declarations
	public Button btnGC;
	public Button btnDDD;
	public Button btnKCDC;
	public Button btnMMC;
	public Button btnSS;
	//Text To Control text under engines
	public Text txEngineSelected;
	private string EngineSelected;

	//PowerUps declarations
	public Button btnRedPowerup;
	public Button btnBluePowerup;
	public Button btnGreenPowerup;

	public GameObject bluePowerUpSel;
	public GameObject redPowerUpSel;
	public GameObject greenPowerUpSel;

	public Text txPowerupSelected;
	public Text txSpecialAbility;
	private int PowerUpColorSelected = 0;

	public Button btnGoBack;
	public Button btnStartGame;

	// Use this for initialization
	void Start () {

		GameObject gameInstancer = GameObject.FindGameObjectWithTag ("GameInstancer");
		Engines = new EngineList();
		

		hideAllEffects ();
		if (gameInstancer != null) 
		{
			instance = gameInstancer.GetComponent<scr_GameInstance> ();
			Engines = gameInstancer.GetComponent<scr_GameInstance> ().engineList;
		}
		else 
		{
			Debug.Log("Game Instancer has not been added to the Scene! Fix that first!");
		}


		foreach (var e in Engines.engines) {
			if(e.CodeName == "GC")
			{
		        btnGC.interactable = e.isUnlocked;
		        btnGC.onClick.AddListener (GannonCannon);
			}
			else if(e.CodeName == "DDD")
			{
				btnDDD.interactable = e.isUnlocked;
				btnDDD.onClick.AddListener(DArcyDefenseDemolisher);
			}
			else if(e.CodeName == "KCDC")
			{
				btnKCDC.interactable = e.isUnlocked;
				btnKCDC.onClick.AddListener(KonradCheatDeathContraption);
			}
			else if(e.CodeName == "MMC")
			{
				btnMMC.interactable = e.isUnlocked;
				btnMMC.onClick.AddListener(MahadyMatterConverter);
			}
			else if(e.CodeName == "SS")
			{
				btnSS.interactable = e.isUnlocked;
				btnSS.onClick.AddListener(SzczodrowskiStrike);
			}	
		}				

		btnRedPowerup.onClick.AddListener (RedPowerupSelected);
		btnBluePowerup.onClick.AddListener (BluePowerupSelected);
		btnGreenPowerup.onClick.AddListener (GreenPowerupSelected);

		btnGoBack.onClick.AddListener (GoBack);
		btnStartGame.onClick.AddListener (StartGame);

		txEngineSelected.text = "";
		txPowerupSelected.text = "";
		txSpecialAbility.text = "";

	}

	public void GannonCannon()
	{
		foreach (var e in Engines.engines) {
			if(e.CodeName == "GC")
			{
				txEngineSelected.text = e.Name;
				txSpecialAbility.text = e.SpecialAbility;
				engine = e;
			
				txPowerupSelected.text = "Please Select Power-Up above.";

				break;
			}	
		}
	}
	public void DArcyDefenseDemolisher()
	{
		foreach (var e in Engines.engines) {
			if(e.CodeName == "DDD")
			{
				txEngineSelected.text = e.Name;
				txSpecialAbility.text = e.SpecialAbility;
				engine = e;
				txPowerupSelected.text = "Please Select Power-Up above.";

				break;
			}	
		}
	}
	public void KonradCheatDeathContraption()
	{
		foreach (var e in Engines.engines) {
			if(e.CodeName == "KCDC")
			{
				txEngineSelected.text = e.Name;
				txSpecialAbility.text = e.SpecialAbility;
				engine = e;

				txPowerupSelected.text = "Please Select Power-Up above.";

				break;
			}	
		}
	}
	public void MahadyMatterConverter()
	{
		foreach (var e in Engines.engines) {
			if(e.CodeName == "MMC")
			{
				txEngineSelected.text = e.Name;
				txSpecialAbility.text = e.SpecialAbility;
				engine = e;

				txPowerupSelected.text = "Please Select Power-Up above.";

				break;
			}	
		}
	}
	public void SzczodrowskiStrike()
	{
		foreach (var e in Engines.engines) {
			if(e.CodeName == "SS")
			{
				txEngineSelected.text = e.Name;
				txSpecialAbility.text = e.SpecialAbility;
				engine = e;

				txPowerupSelected.text = "Please Select Power-Up above.";

				break;
			}	
		}
	}
	public void RedPowerupSelected()
	{
		if(engine.Powerups != null)
		{
			PowerUp p = engine.Powerups.Find (pup => pup.colour == Colour.Red);
			if(p != null)
			{
				txPowerupSelected.text = p.Name;
				hideAllEffects ();
				redPowerUpSel.SetActive (true);
			}
		}
		else
		{
			txPowerupSelected.text = "Select Engine First.";

		}

	}
	public void BluePowerupSelected()
	{
		if(engine.Powerups != null)
		{
			PowerUp p = engine.Powerups.Find (pup => pup.colour == Colour.Blue);
			if(p != null)
			{
				txPowerupSelected.text = p.Name;
				hideAllEffects ();
				bluePowerUpSel.SetActive (true);

			}
		}
		else
		{
			txPowerupSelected.text = "Select Engine First.";

		}

	}
	public void GreenPowerupSelected()
	{
		if(engine.Powerups != null)
		{
			PowerUp p = engine.Powerups.Find (pup => pup.colour == Colour.Green);
			if(p != null)
			{
				txPowerupSelected.text = p.Name;
				hideAllEffects ();

				greenPowerUpSel.SetActive (true);

			}
		}
		else
		{
			txPowerupSelected.text = "Select Engine First.";

		}

	}

	void hideAllEffects()
	{
		redPowerUpSel.SetActive (false);
		bluePowerUpSel.SetActive (false);
		greenPowerUpSel.SetActive (false);

	}

	public void StartGame()
	{
		if(engine != null)
		{
			instance.GetComponent<scr_GameInstance> ().UserProfile.playerEngine = engine;
            instance.GetComponent<scr_GameInstance>().UserProfile.attempts++;
            instance.GetComponent<scr_GameInstance> ().SaveUserProfile ();
			SceneManager.LoadScene ("FirstLevel", LoadSceneMode.Single);
		}
		else
		{
			txEngineSelected.text = "Select an Engine first!";
		}
	}

	public void GoBack()
	{
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().ChooseShip.SetActive (true);
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().ChoosePowerUp.SetActive (false);
		GameObject.FindWithTag ("MenuCamMover").GetComponent<Menu_CameraMoving> ().GoToPreviousPoint ();

	}
}
