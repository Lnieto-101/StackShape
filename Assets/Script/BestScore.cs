using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScore : MonoBehaviour
{
	public Text bestScoreText;
	public GameObject newHighScore;
	float scoreDefault;

	private void Start()
	{
		scoreDefault = PlayerPrefs.GetFloat("record", 0);
	}

	//Change the player best score
	void Update()
    {
		bestScoreText.text = PlayerPrefs.GetFloat("record", 0).ToString("F0") + " M";
		if (scoreDefault != PlayerPrefs.GetFloat("record", 0))
		{
			newHighScore.SetActive(true);
		}
    }
}
