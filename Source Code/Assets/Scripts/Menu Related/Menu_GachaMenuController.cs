using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_GachaMenuController : MonoBehaviour {
	private scr_GameInstance instance;
    private Gacha gacha;
	public GameObject GachaCanvas;
	public Button btnAddCrystals;
	public Button btnGoBack,btnNext, btnSinglePull,btnTriplePull; 

	public bool SinglePull, TriplePull; 

	public GameObject GeneratorDoor;
	public Transform LeftRod, RightRod;
	public GameObject CrystalSpawn;
	public GameObject[] Crystals;

	public GameObject sfx_Elec1, sfx_Elec2, sfx_Elec3;
	public GameObject sfxDimensionPortal;
	public GameObject[] RarityPortals;
	private Transform CrystalSpawnStart;
	public Text txCrystalsAmount;
	private bool GetRidOfCrystal;

	public bool isRolling;
	private bool TimerSet;
	public float Timer;
    int rollsRemaining;

	// Use this for initialization
	void Start () {
		GameObject GameInstancer = GameObject.FindGameObjectWithTag ("GameInstancer");
        gacha = GetComponent<Gacha>();

		if (GameInstancer != null) 
		{
			instance = GameInstancer.GetComponent<scr_GameInstance> ();
		}
		Timer = -999f;
		CrystalSpawnStart = CrystalSpawn.transform;
		CrystalSpawn.SetActive (false);
		btnGoBack.onClick.AddListener (GoBack);
		btnNext.onClick.AddListener (Next);
		btnSinglePull.onClick.AddListener (SingleRoll);
		btnTriplePull.onClick.AddListener (TripleRoll);
		btnAddCrystals.onClick.AddListener (AddCrystalsDebug);
	}

	void AddCrystalsDebug()
	{
		instance.UserProfile.Crystals += 90001;
		instance.SaveUserProfile ();
	}

	void GoBack()
	{
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().MainMenu.SetActive (true);
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().LotteryMenu.SetActive (false);
		GameObject.FindWithTag ("MenuCamMover").GetComponent<Menu_CameraMoving> ().GoToSpecificPoint ("MainMenuScreen");

	}

	void Next()
	{
		if (instance.UserProfile.Crystals != 0) 
		{
            if (SinglePull)
            {
                if (instance.UserProfile.Crystals >= 500)
                {
                    GameObject.FindWithTag("MenuCamMover").GetComponent<Menu_CameraMoving>().GoToSpecificPoint("GachaPull");
                    GachaCanvas.SetActive(false);

                    instance.UserProfile.Crystals -= 500;
                    instance.SaveUserProfile();
                }
            }
                 if (TriplePull)
                {
                    if (instance.UserProfile.Crystals >= 1500)
                    {
                        GameObject.FindWithTag("MenuCamMover").GetComponent<Menu_CameraMoving>().GoToSpecificPoint("GachaPull");
                        GachaCanvas.SetActive(false);

                        instance.UserProfile.Crystals -= 1500;
                        instance.SaveUserProfile();
                    }
                }
            GetComponent<Animator>().Play("closeDoor");
            SetupRoll();


		}
	}

    void SetupRoll()
    {
        GetRidOfCrystal = true;
        Instantiate(Crystals[Random.Range(0, Crystals.Length - 1)], CrystalSpawnStart);
        isRolling = true;
        CrystalSpawn.SetActive(true);
        if(rollsRemaining < 3)
        SinglePull = true;
        Debug.Log(rollsRemaining.ToString());
   
    }

	void SingleRoll(){
		SinglePull = true;
		TriplePull = false;
        rollsRemaining = 1;
	}

	void TripleRoll()
	{
		SinglePull = false;
		TriplePull = true;
        rollsRemaining = 3;
	}

	// Update is called once per frame
	void Update () {
		txCrystalsAmount.text = instance.UserProfile.Crystals.ToString();

		if (isRolling) {
			if (GetRidOfCrystal) {
				CrystalSpawn.transform.SetPositionAndRotation(CrystalSpawnStart.position,CrystalSpawnStart.rotation); 
				CrystalSpawn.SetActive(false);
				int childs = CrystalSpawn.transform.childCount;
				for (int i = childs - 1; i > 0; i--) {
					GameObject.Destroy (CrystalSpawn.transform.GetChild (i).gameObject);
				}
				GetRidOfCrystal = false;
				CreateElec2 ();
                if (Timer == -999 & !TimerSet)
                {
                    Timer = 1f;
                    TimerSet = true;
                }

            }
			if (Timer != -999) {
				Timer += Time.deltaTime;
			}
			if (Timer > 3f) 
			{
				if (GameObject.Find ("Electricity_1(Clone)") == null) {
					CreateElec1 ();
				}
			}
			if (Timer > 4f) 
			{
				if (GameObject.Find ("Electricity_3(Clone)") == null) {
					CreateElec3 ();
				}
			}
			if (Timer > 5f) {
				//if (GameObject.FindWithTag("DimensionalGate") == null) {
				//	CreateDimensionPortal ();
				//}
			}
			if (Timer > 7f) {
                if (SinglePull)
                {
                    GetComponent<Gacha>().SingleRollPool();
                    rollsRemaining--;
                    SinglePull = false;
                }
                if (TriplePull)
                {
                    GetComponent<Gacha>().TripleRollPool();
                    rollsRemaining--;
                    TriplePull = false;
                }
                if (GameObject.FindWithTag("PortalEpic") == null | GameObject.FindWithTag("PortalLegendary") == null | GameObject.FindWithTag("PortalMythical") == null |  GameObject.FindWithTag("PortalRare") == null)
					CreateRarityPortal ();
			}
			if (Timer > 10f) {
                if (rollsRemaining <= 0)
                {
                    Timer = -999;
                    GameObject.FindWithTag("MenuCamMover").GetComponent<Menu_CameraMoving>().GoToSpecificPoint("GachaScreen");
                    GachaCanvas.SetActive(true);
                    TimerSet = false;
                    isRolling = false;
                }
                else
                {
                    Timer = -999;
                    TimerSet = false;
                    SetupRoll();
                }
            }

			int childsPortal = GetComponent<Gacha>().ObjSpawnPoint.transform.childCount;
			for (int i = childsPortal - 1; i > 0; i--) {
				if (childsPortal > 1) {
					GameObject.Destroy (GetComponent<Gacha>().ObjSpawnPoint.transform.GetChild (i).gameObject);

				}
			}
		}

	}
	void CreateElec1()
	{
		Instantiate (sfx_Elec1, LeftRod);
		Instantiate (sfx_Elec1, RightRod);	
	}

	void CreateElec2()
	{
		if (GameObject.Find ("Electricity_2(Clone)") == null) {
			Instantiate (sfx_Elec2, LeftRod);
			Instantiate (sfx_Elec2, RightRod);
		}

	}

	void CreateElec3()
	{
		Instantiate (sfx_Elec3, LeftRod);
		Instantiate (sfx_Elec3, RightRod);
	}

	void CreateDimensionPortal()
	{
		Instantiate (sfxDimensionPortal, GetComponent<Gacha> ().ObjSpawnPoint.transform);
	}

	void CreateRarityPortal()
	{
		
		if (GetComponent<Gacha> ().ItemRarity == Rarity.Common) {
			if(GameObject.FindWithTag("PortalRare") == null)
			Instantiate (RarityPortals [0], GetComponent<Gacha> ().PortalSpawnPoint.transform);
		}
        else if (GetComponent<Gacha> ().ItemRarity == Rarity.Uncommon) {
			if( GameObject.FindWithTag("PortalEpic") == null)
			Instantiate (RarityPortals [1], GetComponent<Gacha> ().PortalSpawnPoint.transform);
		}
        else if (GetComponent<Gacha> ().ItemRarity == Rarity.Legendary) {
			if(GameObject.FindWithTag("PortalLegendary") == null)
			Instantiate (RarityPortals [2], GetComponent<Gacha> ().PortalSpawnPoint.transform);
		}
        else if (GetComponent<Gacha> ().ItemRarity == Rarity.Mythical) {
			if(GameObject.FindWithTag("PortalMythical") == null)
			Instantiate (RarityPortals [3], GetComponent<Gacha> ().PortalSpawnPoint.transform);
		}
        else {
			if(GameObject.FindWithTag("PortalEpic") == null)
			Instantiate (RarityPortals [0], GetComponent<Gacha> ().PortalSpawnPoint.transform);

		}
	}
}
