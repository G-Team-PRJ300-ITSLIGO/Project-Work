using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Menu_ChooseShipController : MonoBehaviour {
	private scr_GameInstance instance;

	public string ShipType;

	public Button btnNext;
	public Button btnGoBack;

	public GameObject ShipSpawnPoint;
    
	public LinkedList<Ship> ships;
	LinkedListNode<Ship> ship;
	public List<GameObject> ShipObjs;


	public GameObject[] atkEnergies;
	//int to calculate per how much of "x" statistic (ex. per how much hp) how many energy units to display.
	public int atkMod;
	public GameObject[] frEnergies;
	public float frMod;
	public GameObject[] spdEnergies;
	public int spdMod;
	public GameObject[] hpEnergies;
	public int hpMod;
    public Text diffMod;

	void Start () {
		GameObject gameInstancer = GameObject.FindGameObjectWithTag ("GameInstancer");

		if (gameInstancer != null) 
		{
			instance = gameInstancer.GetComponent<scr_GameInstance> ();

			ships = new LinkedList<Ship> ();

			foreach(Ship s in instance.shipList.ships)
			{
				if(s.isUnlocked)
				{
					ships.AddLast (s);
                    Debug.Log("ADDING THIS ONE");
				}
			}
			ship = ships.First;

			Instantiate (ShipObjs.Find(s => s.name == "menu_" + ship.Value.ShipName).gameObject, ShipSpawnPoint.transform);
			UpdateStats ();

			btnGoBack.onClick.AddListener (GoBack);
		}
		else 
		{
			Debug.Log("Game Instancer has not been added to the Scene! Fix that first!");
		}

		btnNext.onClick.AddListener (CreateProfile);
		btnGoBack.onClick.AddListener (GoBack);
	}
	void Update()
	{


		if(GameObject.FindWithTag("MenuController").GetComponent<Menu_GeneralMenuController>().ChooseShip.activeInHierarchy)
		{
			if (ship.Value.ShipName == "CebulaShip3") {
				GameObject.Find ("ShipSpawn").transform.localScale = new Vector3 (600, 600, 600);
			}
			else {
				GameObject.Find ("ShipSpawn").transform.localScale = new Vector3 (150, 150, 150);

			}

			ShipType = ship.Value.ShipName;
            if (instance != null)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {

                    Destroy(GameObject.Find("menu_" + ship.Value.ShipName + "(Clone)"));
                    ScrollLeft();
                    if (ship != null)
                    {
                        Instantiate(ShipObjs.Find(obj => obj.name == "menu_" + ship.Value.ShipName), ShipSpawnPoint.transform);

                    }
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    Destroy(GameObject.Find("menu_" + ship.Value.ShipName + "(Clone)"));
                    ScrollRight();
                    if (ship != null)
                    {
                        Instantiate(ShipObjs.Find(obj => obj.name == "menu_" + ship.Value.ShipName), ShipSpawnPoint.transform);
                    }

                }
            }

			if (!string.IsNullOrEmpty("menu_" + ship.Value.ShipName + "(Clone)")) 
			{
				if(GameObject.Find("menu_" + ship.Value.ShipName + "(Clone)") != null)
				{
					GameObject.Find ("menu_" + ship.Value.ShipName + "(Clone)").GetComponent<Transform>().Rotate (new Vector3 (0, 15, 0) * (5 * Time.deltaTime));
				}
			}
		}
	}
		

	void ScrollLeft(){
		if(ship.Previous != null)
		{
			ship = ship.Previous;

			UpdateStats ();
		}
	}

	void ScrollRight(){
		if(ship.Next != null)
		{
			ship = ship.Next;
			UpdateStats ();
		}
	}

	void UpdateStats()
	{
		HideAllUnits ();

        Ship StatsShip = ship.Value;

		int atkUnits, spdUnits, hpUnits;
		int frUnits, finalFrUnits;
		atkUnits = (int)StatsShip.statistics.Damage / atkMod;
		Debug.Log ("Atk units: " + atkUnits);
		frUnits = (int)(StatsShip.statistics.fireRate / frMod);
		Debug.Log ("FR units: " + frUnits);
		spdUnits = (int)StatsShip.statistics.speed / spdMod;
		Debug.Log ("Spd units: " + spdUnits);
		hpUnits = (int)StatsShip.statistics.HP / hpMod;
		Debug.Log ("HP units: " + hpUnits);
        diffMod.text = StatsShip.statistics.Difficulty().ToString();


		for (int i = 0; i < atkUnits; i++)
		{
			atkEnergies [i].SetActive (true);
		}
		if (frUnits > frEnergies.Length) {
			finalFrUnits = frEnergies.Length;
		}
		else
		{
			finalFrUnits = frEnergies.Length - frUnits;
		}
		for (int i = 1; i <= finalFrUnits; i ++)
		{
			frEnergies [i - 1].SetActive (true);
		}
		for (int i = 0; i < spdUnits; i++)
		{
			spdEnergies [i].SetActive (true);
		}

		for (int i = 0; i < hpUnits; i++)
		{
			hpEnergies [i].SetActive (true);
		}
	}

	void HideAllUnits()
	{
		foreach(GameObject go in atkEnergies)
		{
			go.SetActive (false);
		}
		foreach(GameObject go in frEnergies)
		{
			go.SetActive (false);
		}
		foreach(GameObject go in spdEnergies)
		{
			go.SetActive (false);
		}
		foreach(GameObject go in hpEnergies)
		{
			go.SetActive (false);
		}
	}


	private void GoBack(){
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().MainMenu.SetActive (true);
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().MainMenu.GetComponent<Menu_MainMenuController> ().ProfileFoundLayout ();
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().ChooseShip.SetActive (false);
		GameObject.FindWithTag ("MenuCamMover").GetComponent<Menu_CameraMoving> ().GoToPreviousPoint ();

	}


	private void CreateProfile()
	{
		instance.CreateProfile (instance.UserProfile.ProfileName, ship.Value.ShipName, ship.Value.statistics, ship.Value.svStats, ship.Value);
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().ChooseShip.SetActive (false);
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().ChoosePowerUp.SetActive (true);
		GameObject.FindWithTag ("MenuCamMover").GetComponent<Menu_CameraMoving> ().GoToNextPoint ();

	}
}
