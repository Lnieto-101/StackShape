using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreGestion : MonoBehaviour
{
	Text score;
	public GameObject cam;
	AudioSource scoreSound;
	string oldtext = "";
    // Start is called before the first frame update
    void Start()
    {
		score = gameObject.GetComponent<Text>();
		scoreSound = GetComponent<AudioSource>();
		oldtext = "0 M";
    }

	

    // Update is called once per frame
    void Update()
    {
		
		score.text = (cam.transform.position.y * 10).ToString("F0") + " M";
		if (score.text != oldtext && PlayerPrefs.GetInt("Sound", 1) == 1)
			scoreSound.Play();
		
		oldtext = score.text;

		if (PlayerPrefs.GetFloat("record", 0) < cam.transform.position.y * 10)
		{
			PlayerPrefs.SetFloat("record", cam.transform.position.y * 10);
		}
	}
}
