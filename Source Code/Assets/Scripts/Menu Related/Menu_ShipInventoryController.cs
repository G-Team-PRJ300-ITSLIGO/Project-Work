using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Menu_ShipInventoryController : MonoBehaviour {
	public Button btnAnaconda, btnScarab, btnCebula;
	public Text txShipLore;
	public Text NoFactionSelectedTx;
	public Button btnEnlargeShip, btnGoBack;
	public GameObject buttonsAnaconda, buttonsScarab, buttonsCebula;
	private bool isPreviewing;

	public GameObject cvInventory;
	public GameObject cvPreview;

	private scr_GameInstance instance;

	private LinkedList<Ship> ships;
	LinkedListNode<Ship> ship;
	public List<GameObject> ShipObjs;
	public Transform PreviewSpawnPoint;
	public float RotationSpeed;
	public string selectedShipName;
	public Button btnGoBackToInv;

	private List<Lore> lores;

	// Use this for initialization
	void Start () {
        Debug.Log("Waaait a second..");
        GameObject gameInstancer = GameObject.FindGameObjectWithTag ("GameInstancer");

		if (gameInstancer != null)
		{
			instance = gameInstancer.GetComponent<scr_GameInstance> ();

			ships = new LinkedList<Ship> ();
			lores = new List<Lore> ();
			foreach (Ship s in instance.shipList.ships)
			{
				ships.AddLast (s);
			}

			foreach (Lore l in instance.loreList.Lores) {
				lores.Add (l);
			}
		}


		btnEnlargeShip.onClick.AddListener (EnlargeShip);
		btnGoBack.onClick.AddListener (GoBack);

		btnAnaconda.onClick.AddListener (AnacondaSelected);
		btnScarab.onClick.AddListener (ScarabSelected);
		btnCebula.onClick.AddListener (CebulaSelected);

		txShipLore.text = "Select a ship first!";
		NoFactionSelectedTx.text = "Select a faction below!";

		btnGoBackToInv.onClick.AddListener (GoBackToInventory);

		cvInventory.SetActive(true);
		cvPreview.SetActive(false);
		ship = new LinkedListNode<Ship>(new Ship());

	}
	
	// Update is called once per frame
	void Update () 
	{

		if (cvPreview.activeInHierarchy)
		{
			if (Input.GetKey (KeyCode.LeftArrow)) 
			{
				if(GameObject.FindWithTag ("MenuObj") != null)
				{
					GameObject.FindWithTag ("MenuObj").GetComponent<Transform>().Rotate (new Vector3 (0, 1, 0) * (RotationSpeed * Time.deltaTime));
					//Debug.Log (GameObject.FindWithTag ("MenuObj").name + " should be moving left now!");
				}
			}
			if (Input.GetKey (KeyCode.RightArrow)) 
			{
				if(GameObject.FindWithTag ("MenuObj") != null)
				{
					GameObject.FindWithTag ("MenuObj").GetComponent<Transform>().Rotate (new Vector3 (0, -1, 0) * (RotationSpeed * Time.deltaTime));
					//Debug.Log (GameObject.FindWithTag ("MenuObj").name + " should be moving right now!");

				}
			}
		}

		if (EventSystem.current.currentSelectedGameObject != null) 
		{
			if(EventSystem.current.currentSelectedGameObject.name.Contains("AnacondaShip") || EventSystem.current.currentSelectedGameObject.name.Contains("ScarabShip") || EventSystem.current.currentSelectedGameObject.name.Contains("CebulaShip"))
				{
					selectedShipName = EventSystem.current.currentSelectedGameObject.name;
						foreach (var s in ships)
						{
							if (s.ShipName == selectedShipName) 
							{
								ship.Value = s;
								//Debug.Log ("Ship: " + ship.Value.ShipName);
							}
						}
				foreach (var lore in lores) 
				{
					if (ship.Value != null) 
					{
						if (lore.shipName == ship.Value.ShipName) 
						{
							txShipLore.text = lore.loreDesc;
							break;
						}
					}
				}
				}
		}

	}

	void AnacondaSelected()
	{
		hideAllButtons ();
		buttonsAnaconda.SetActive (true);
		foreach (Button b in buttonsAnaconda.GetComponentsInChildren<Button>()) {
			foreach (Ship s in instance.shipList.ships) {
				if (s.ShipName == b.name) {
					b.interactable = s.isUnlocked;	
				}
			}
		}
	}

	void ScarabSelected()
	{
		hideAllButtons ();
		buttonsScarab.SetActive (true);
		foreach (Button b in buttonsScarab.GetComponentsInChildren<Button>()) {
			foreach (Ship s in instance.shipList.ships) {
				if (s.ShipName == b.name) {
					b.interactable = s.isUnlocked;	
				}
			}
		}
	}

	void CebulaSelected()
	{
		hideAllButtons ();
		buttonsCebula.SetActive (true);
		foreach (Button b in buttonsCebula.GetComponentsInChildren<Button>()) {
			foreach (Ship s in instance.shipList.ships) {
				if (s.ShipName == b.name) {
					b.interactable = s.isUnlocked;	
				}
			}
		}
	}
		


	void hideAllButtons()
	{
		NoFactionSelectedTx.text = "";
		buttonsAnaconda.SetActive (false);
		buttonsCebula.SetActive (false);
		buttonsScarab.SetActive (false);
	}

	void GoBackToInventory()
	{
		Destroy (GameObject.FindWithTag ("MenuObj"));
		cvInventory.SetActive(true);
		cvPreview.SetActive(false);
		GameObject.FindWithTag ("MenuCamMover").GetComponent<Menu_CameraMoving> ().GoToSpecificPoint ("InventoryScreen");

	}

	void EnlargeShip()
	{
		if (!string.IsNullOrEmpty(selectedShipName)) {
			Instantiate (ShipObjs.Find (obj => obj.name == "menu_" + ship.Value.ShipName), PreviewSpawnPoint.transform);
			//Below are quick fixes for two ships being either too small or too large
			if (selectedShipName == "CebulaShip3") {
				GameObject.Find ("InventorySpawnPoint").transform.localScale = new Vector3 (600, 600, 600);
			} 
			else if (selectedShipName == "ScarabShip3") {
				GameObject.Find ("InventorySpawnPoint").transform.localScale = new Vector3 (100, 100, 100);

			}
			else
			{
				GameObject.Find ("InventorySpawnPoint").transform.localScale = new Vector3 (150, 150, 150);

			}
			cvPreview.SetActive(true);
			cvInventory.SetActive(false);
			GameObject.FindWithTag ("MenuCamMover").GetComponent<Menu_CameraMoving> ().GoToSpecificPoint ("InventoryShowShips");

		}

	}
	void GoBack()
	{
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().DisplayProfile.SetActive (true);
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().ShipInventory.SetActive (false);
		GameObject.FindWithTag ("MenuCamMover").GetComponent<Menu_CameraMoving> ().GoToSpecificPoint ("MainMenuScreen");

	}
}
