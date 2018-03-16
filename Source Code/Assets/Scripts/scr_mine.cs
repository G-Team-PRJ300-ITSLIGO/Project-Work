using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_mine : MonoBehaviour {

    public GameObject mineDebris;
    public int debrisAmount;
    private float rotation;
	public bool armed;
	private float time;
	public float armedTime;
    void Start()
    {
        rotation = 360f / debrisAmount;
    }
    void OnDestroy()
        {
        if(GetComponent<scr_enemyStats>().stats.currentHP <= 0 || armed)
        {
            for (int i = 0; i <= debrisAmount; i++)
            {
                Instantiate(mineDebris, transform.position, Quaternion.Euler(0f, rotation * i, 0f));
            }
        }
        }

	void Update()
	{
		if (!armed)
			return;
		time += Time.deltaTime;
		if (time >= armedTime) {
	
				for (int i = 0; i <= debrisAmount; i++)
				{
					Instantiate(mineDebris, transform.position, Quaternion.Euler(0f, rotation * i, 0f));
				}
			Destroy (gameObject);
		}
	}
}
