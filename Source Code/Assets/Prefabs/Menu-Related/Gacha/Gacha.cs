using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Gacha : MonoBehaviour {
	private scr_GameInstance instance;
    private EngineList eList;
    private ShipList sList;


    static Random r;
    public List<GachaDropPool> dropPools;
    [SerializeField]
    private GachaDropPool dropPool;
    [SerializeField]
    private GachaItem SelectedItem;
    [SerializeField]
    private string fuck;
    bool poolSelected = false;
    public int Things;
	public Transform PortalSpawnPoint;
    public Transform ObjSpawnPoint;
    int range;
    int poolRange;

	public Rarity ItemRarity;

    void Start()
    {
        r = new Random();
        foreach(GachaDropPool g in dropPools)
        {
            range += g.Chance;
        }

		GameObject GameInstancer = GameObject.FindGameObjectWithTag ("GameInstancer");

		if (GameInstancer != null) 
		{
			instance = GameInstancer.GetComponent<scr_GameInstance> ();
            eList = instance.engineList;
            sList = instance.shipList;
		}
    }

    void Update()
    {
      
    }
    public void SingleRollPool()
    {
        int roll = Random.Range(0, range);
        int rollChance = 0;
        Debug.Log("dropPool roll was " + roll.ToString());
       

        foreach (GachaDropPool g in dropPools)
        {
                rollChance += g.Chance;
                ItemRarity = g.rarity;
                if (rollChance > roll)
                {
                    dropPool = g;
                    Debug.Log("dropPool was " + g.name);
                    SelectItem();
                    break;
                }
        }
        //if(dropPool == null)
        //{
        //    dropPool = dropPools.Find(pool => pool.name == "Common");
        //    SelectItem();
        //}
    }

	public void TripleRollPool()
	{
        foreach (GachaDropPool g in dropPools)
        {
            if (g.rarity == Rarity.Mythical)
            {

                dropPool = g;
                ItemRarity = g.rarity;
                Debug.Log("dropPool was " + g.name);
                SelectItem();
                break;
            }
        }

    }

	//Single Roll
    public void SelectItem()
    {
        poolRange = 0;
        foreach(GachaItem gi in dropPool.items)
        {
            poolRange += gi.dropChance;
        }
        int roll = Random.Range(0, poolRange);
        int rollChance = 0;
        Debug.Log("drop roll was " + roll.ToString());
        foreach (GachaItem g in dropPool.items)
        {
            rollChance += g.dropChance;
            if (rollChance > roll)
            {
                Debug.Log(g.name);
                SelectedItem = g;
                fuck = SelectedItem.type;
                Instantiate(SelectedItem.thing, ObjSpawnPoint.transform.position, ObjSpawnPoint.transform.rotation);
                break;
            }
        }
            if (SelectedItem.type == "Ship")
            {
                Debug.Log("it's a ship");
                foreach (Ship ship in instance.shipList.ships)
                {
                    if (SelectedItem.name == ship.ShipName)
                    {
                        ship.isUnlocked = true;
                    if(ship.isUnlocked)
                    {
                        Debug.Log("FUCKIGN FINALLY " + ship.ShipName);
                    }
                        instance.ReloadShips();
                        break;
                    }
                }
            }
            else if (SelectedItem.type == "Engine")
            {
                Debug.Log("drop roll was ");
                foreach (Engine engine in instance.engineList.engines)
                {
                    if (SelectedItem.name == engine.CodeName)
                    {
                        Debug.Log("drop roll was ");
                        engine.isUnlocked = true;
                        instance.ReloadEngines();
                        break;
                    }
                }
            }
        }
    }
