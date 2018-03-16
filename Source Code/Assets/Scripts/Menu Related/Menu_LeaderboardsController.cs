using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_LeaderboardsController : MonoBehaviour {
	public GameObject[] txsNames, txsLevels, txsScores;
	public int SlotsAmount;
	private scr_GameInstance instance;
	public Button btnGoBack;

	// Use this for initialization
	void Start () {
		btnGoBack.onClick.AddListener (GoBack);
		HideAllScores ();
		GameObject gameInstancer = GameObject.FindGameObjectWithTag ("GameInstancer");

		if (gameInstancer != null) {
			instance = gameInstancer.GetComponent<scr_GameInstance> ();
		}
		instance.LoadHighscores ();

		SlotsAmount = txsNames.Length;

			for (int i = 0; i < SlotsAmount; i++) 
			{
			
			if (instance.Highscores.highscores[i] != null & i < SlotsAmount)
				{
					txsNames [i].SetActive (true);
					txsNames [i].GetComponentInChildren<Text> ().text = instance.Highscores.highscores[i].User;

					txsLevels [i].SetActive (true);
					txsLevels [i].GetComponentInChildren<Text> ().text =instance.Highscores.highscores[i].levelName;

					txsScores [i].SetActive (true);
					txsScores [i].GetComponentInChildren<Text> ().text = instance.Highscores.highscores[i].score.ToString();
				}
			}

	}

	void GoBack()
	{
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().MainMenu.SetActive (true);
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().MainMenu.GetComponent<Menu_MainMenuController> ().ProfileFoundLayout ();
		GameObject.FindWithTag ("MenuController").GetComponent<Menu_GeneralMenuController> ().Leaderboards.SetActive (false);
	}

	void HideAllScores()
	{
		foreach (var tx in txsNames)
		{
			tx.SetActive (false);
		}
		foreach (var tx in txsLevels)
		{
			tx.SetActive (false);
		}
		foreach (var tx in txsScores)
		{
			tx.SetActive (false);
		}
	}
}
