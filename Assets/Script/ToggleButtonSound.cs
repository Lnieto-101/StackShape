using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButtonSound : MonoBehaviour
{
	public GameObject soundOn;
	public GameObject soundOff;
	public GameObject MusicOn;
	public GameObject MusicOff;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Music", 1) == 1)
		{
			MusicOn.SetActive(true);
			MusicOff.SetActive(false);
		}
		else
		{
			MusicOn.SetActive(false);
			MusicOff.SetActive(true);
		}

		if (PlayerPrefs.GetInt("Sound", 1) == 1)
		{
			soundOn.SetActive(true);
			soundOff.SetActive(false);
		}
		else
		{
			soundOn.SetActive(false);
			soundOff.SetActive(true);
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
