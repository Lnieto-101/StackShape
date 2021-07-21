using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundGestion : MonoBehaviour
{
	public AudioSource[] sound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

	private void OnEnable()
	{
		if (PlayerPrefs.GetInt("Sound", 1) == 1)
		{
			foreach (var item in sound)
			{
				item.enabled = true;
			}
		}
		else
		{
			foreach (var item in sound)
			{
				item.enabled = false;
			}
		}
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
