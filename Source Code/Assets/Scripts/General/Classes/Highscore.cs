using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Highscore
{
	public string User;
	public int score;
	public string levelName;
		
	public Highscore()
	{

	}
}

[Serializable]
public class HighscoreList
{
	public List<Highscore> highscores;

	public HighscoreList()
	{
		highscores = new List<Highscore> ();
	}
}
