using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Linq;

public class scr_GameInstance : MonoBehaviour {
	public Profile UserProfile;
	private string savePath;

	public ShipList shipList = new ShipList ();
	public List<GameObject> ShipObjects = new List<GameObject> ();
	public string ShipListPath;
	public int ShipsAmount;

	public EngineList engineList;
	public string EngineListPath;

	public string LoreFilePath;
	public LoreList loreList;

	public string HighscoresPath;
	public HighscoreList Highscores;

	void Start () 
	{
		savePath = Application.persistentDataPath + "\\profile.prjg";
		ShipListPath = Application.persistentDataPath + "\\ships.json";
		EngineListPath = Application.persistentDataPath + "\\engines.json";
		LoreFilePath = Application.dataPath + "\\StreamingAssets" + "\\Data\\Lore.json";
		HighscoresPath = Application.persistentDataPath + "\\scores.json";


        #region Ships Loading Handling
        if (!File.Exists(ShipListPath))
        {
            CreateShipFile(ShipObjects);
        }
        else
        {
            LoadShipList();
        }
 

		if(DoesProfileExist()){
			LoadUserProfile();
		}

		GetUnlockedShips();
        #endregion

        #region Engines Loading Handling
        if (!File.Exists(EngineListPath))
        {
            CreateEnginesFile();
        }
		
		LoadEngineList();
		#endregion
        foreach(Engine e in engineList.engines)
        {
            Debug.Log(e.isUnlocked);
        }
		LoadLoreList ();

		if (Load<HighscoreList> (HighscoresPath) == null)
		{
			CreateHighscoresFile ();
		}
        foreach (Ship s in shipList.ships)
        {
            if (s.isUnlocked)
            {
                Debug.Log(s.ShipName + " is unlocked");
            }
        }
        DontDestroyOnLoad (this);

	}

	public void LoadShipList(){
		var ships = Load<ShipList> (ShipListPath);
		if(ships != null){
			shipList = ships;
		}
	}
	public void GetUnlockedShips()
	{
        UserProfile.UnlockedShips = 0;
        ShipsAmount = shipList.ships.Count;
		foreach(var s in shipList.ships)
		{
			if(s.isUnlocked)
			{
				UserProfile.UnlockedShips++;
			}
		}
	}

	public void LoadLoreList(){
		var lores = Load<LoreList> (LoreFilePath);
		if(lores != null){
			loreList = lores;
		}
	}

	public void LoadEngineList(){
		var engines = Load<EngineList> (EngineListPath);
		if(engines != null){
			engineList = engines;
		}
	}
	public void LoadHighscores(){
		var highscores = Load<HighscoreList> (HighscoresPath);
		if(highscores != null){
			Highscores = highscores;
			Highscores.highscores.OrderByDescending (h => h.score);
		}
	}
	public void Save(object objToSave, string path)
	{
		using (StreamWriter sw = new StreamWriter (path)) 
		{
			string json = JsonUtility.ToJson (objToSave, true);
			sw.Write (json);
		}
	}

	//Generic Load Method
	public T Load<T>(string path)
	{
		if (File.Exists (path)) 
		{
			using (StreamReader sr = new StreamReader (path)) 
			{
				string json = sr.ReadToEnd ();
				return JsonUtility.FromJson<T> (json);
			}
		}
		else 
		{
			return default(T);
		}
	}

	public void CreateProfile(string username, string ShipType, Stats stats, SavableStats saveStats, Ship ship)
	{
		UserProfile = new Profile (username, ShipType,stats, saveStats, ship,UserProfile.Crystals);
		SaveUserProfile ();
	}
	public void CreateSimpleProfile(string username)
	{
		UserProfile = new Profile (username);
        Debug.Log(Application.persistentDataPath);
        //CreateEnginesFile();
        //CreateShipFile(ShipObjects);
        SaveUserProfile ();
	}

	public void SaveUserProfile()
	{
        Save(UserProfile, savePath);
	}

	public void LoadUserProfile(){
		UserProfile = Load<Profile>(savePath);
	}

	public void DeleteProfile()
	{
        if (DoesProfileExist())
        {
            if(File.Exists(EngineListPath))
            File.Delete(EngineListPath);
            if(File.Exists(ShipListPath))
            File.Delete(ShipListPath);
            File.Delete(savePath);
        }
	}

	public bool DoesFileExit(string path)
	{
		return File.Exists (path);
	}

	public bool DoesProfileExist()
	{
		return File.Exists (savePath);
	}

	public void LoadScene(string name)
	{
		SceneManager.LoadScene (name, LoadSceneMode.Single);
	}

	public void Quit()
	{
		SaveUserProfile();
		Application.Quit ();
	}
		

	public void CreateShipFile(List<GameObject> ships)
	{
		Ship temp = new Ship ();

		foreach(var ship in ships)
		{
			temp = new Ship () 
			{ 
				ShipName = ship.name,
				svStats = new SavableStats()
				{
					Health = ship.GetComponent<scr_playerBehaviour>().stats.HP,
					Speed = ship.GetComponent<scr_playerBehaviour> ().stats.speed,
					Damage = ship.GetComponent<scr_playerBehaviour>().stats.Damage,
					FireRate = ship.GetComponent<scr_playerBehaviour>().stats.fireRate,
					Score = ship.GetComponent<scr_playerBehaviour>().stats.ScoreValue
				},
				statistics = ship.GetComponent<scr_playerBehaviour>().stats

			};
			//if(ship.name == "CebulaShip" || ship.name == "AnacondaShip" || ship.name == "ScarabShip" || ship.name == "CebulaShip2" || ship.name == "AnacondaShip2" || ship.name == "ScarabShip2" || ship.name == "CebulaShip3" || ship.name == "AnacondaShip3" || ship.name == "ScarabShip3"  )
			if(ship.name == "AnacondaShip" || ship.name == "ScarabShip")
			{
				temp.isUnlocked = true;
			}

			if(ship.name.ToUpper().Contains("CEBULA"))
			{
				temp.faction = Faction.OrderOfCebula;
			}
			else if(ship.name.ToUpper().Contains("ANACONDA"))
			{
				temp.faction = Faction.AnacondaSquadron;
			}
			else if(ship.name.ToUpper().Contains("SCARAB"))
			{
				temp.faction = Faction.PathOfScarab;
			}
			else
			{
				temp.faction = Faction.Other;
			}
			shipList.ships.Add (temp);
		}

		using (StreamWriter sw = new StreamWriter (ShipListPath)) 
		{
			string json = JsonUtility.ToJson (shipList, true);
			sw.Write (json);
		}


	}


	public void ReloadShips()
	{
		GetUnlockedShips ();
		SaveUserProfile ();

        using (StreamWriter sw = new StreamWriter(ShipListPath))
        {
            string json = JsonUtility.ToJson(shipList, true);
            sw.Write(json);
        }
    }

	public void ReloadEngines()
	{
		SaveUserProfile ();

        using (StreamWriter sw = new StreamWriter(EngineListPath))
        {
            string json = JsonUtility.ToJson(engineList, true);
            sw.Write(json);
        }


    }

    public List<Engine> CreateEngines ()
	{
		return new List<Engine>
		{
			new Engine ()
			{
				CodeName = "GC",
				Name = "Gannon Cannon",
				Powerups = new List<PowerUp>()
				{
					new PowerUp()
					{
						Name = "FireRate Up!",
						colour = Colour.Blue
					},
					new PowerUp()
					{
						Name = "FireRate Up!",
						colour = Colour.Green
					},
					new PowerUp()
					{
						Name= "Shield Up!",
						colour = Colour.Red
					}
				},
				isUnlocked = false,
				SpecialAbility = "Beam of Bullets."
			},
			new Engine ()
			{
				CodeName = "MMC",
				Name = "Mahady Matter Converter",
				Powerups = new List<PowerUp>()
				{
					new PowerUp()
					{
						Name = "Damage Up!",
						colour = Colour.Blue
					},
					new PowerUp()
					{
						Name = "Speed Up!",
						colour = Colour.Green
					},
					new PowerUp()
					{
						Name= "Special Damage Up!",
						colour = Colour.Red
					}
				},
				isUnlocked = false,
				SpecialAbility = "Life Steal Beam."
			},
			new Engine ()
			{
				CodeName = "KCDC",
				Name = "Konrad Cheat-Death Contraption",
				Powerups = new List<PowerUp>()
				{
					new PowerUp()
					{
						Name = "Attack Up!",
						colour = Colour.Blue
					},
					new PowerUp()
					{
						Name = "Attack Down!",
						colour = Colour.Green
					},
					new PowerUp()
					{
						Name= "HP Restore!",
						colour = Colour.Red
					}
				},
				isUnlocked = false,
				SpecialAbility = "On Game Over, Chance to re-spawn."
			},
			new Engine ()
			{
				CodeName = "DDD",
				Name = "DArcy Defense Demolisher",
				Powerups = new List<PowerUp>()
				{
					new PowerUp()
					{
						Name = "HP Up!",
						colour = Colour.Blue
					},
					new PowerUp()
					{
						Name = "FireRate Up!",
						colour = Colour.Green
					},
					new PowerUp()
					{
						Name= "Speed Up!",
						colour = Colour.Red
					}
				},
				isUnlocked = true,
				SpecialAbility = "Send out a wave which brings HP of all visible enemies to 1 HP."
			},
			new Engine ()
			{
				CodeName = "SS",
				Name = "Szczodrowski Strike",
				Powerups = new List<PowerUp>()
				{
					new PowerUp()
					{
						Name = "Attack Up!",
						colour = Colour.Blue
					},
					new PowerUp()
					{
						Name = "Speed Up!",
						colour = Colour.Green
					},
					new PowerUp()
					{
						Name= "Huge Attack Up!",
						colour = Colour.Red
					}
				},
				isUnlocked = false,
				SpecialAbility = "Send out a screen covering wave of onions to destroy enemies on contact."
			},
		};
	}

	public void CreateLoreFile(List<GameObject> ships)
	{
		Lore temp = new Lore ();

		foreach(var s in ships)
		{
			temp = new Lore()
			{
				shipName = s.name,
				loreDesc = "blablah"
			};
			loreList.Lores.Add (temp);
		}

		using (StreamWriter sw = new StreamWriter (LoreFilePath)) 
		{
			string json = JsonUtility.ToJson (loreList, true);
			sw.Write (json);
		}


	}

	public void CreateHighscoresFile()
	{
		Highscores = new HighscoreList ();

		using (StreamWriter sw = new StreamWriter (HighscoresPath)) 
		{
			string json = JsonUtility.ToJson (Highscores, true);
			sw.Write (json);
		}


	}

	public void UpdateHighscoresFile(string user, string levelname, int scr)
	{
		
		HighscoreList hsList = Load<HighscoreList> (HighscoresPath);
		Highscore temp = new Highscore ();

		hsList.highscores.Add (new Highscore () {
			User = user,
			levelName = levelname,
			score = scr
		});
	

		hsList.highscores.OrderByDescending (u => u.score);

		Highscores = hsList;
		using (StreamWriter sw = new StreamWriter (HighscoresPath)) 
		{
			string json = JsonUtility.ToJson (Highscores, true);
			sw.Write (json);
		}
	}

	public void CreateEnginesFile ()
	{
		List<Engine> engines = CreateEngines ();
		Engine temp = new Engine ();

		foreach(var e in engines)
		{
			temp = new Engine () 
			{ 
				CodeName = e.CodeName,
				Name = e.Name,
				Powerups = e.Powerups,
				SpecialAbility = e.SpecialAbility,
				isUnlocked = e.isUnlocked //For Testing Purposes unlock every engine.
			};
			engineList.engines.Add (temp);
		}

		using (StreamWriter sw = new StreamWriter (EngineListPath)) 
		{
			string json = JsonUtility.ToJson (engineList, true);
			sw.Write (json);
		}



	}
}

