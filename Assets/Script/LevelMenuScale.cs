using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMenuScale : MonoBehaviour
{
	public GameObject[] levelMenu;
    // Start is called before the first frame update
    void Start()
    {
        if (Screen.width <= 480 && Screen.height <= 800)
		{
			EnableLevel(0);
		}
		else if (Screen.width <= 720 && Screen.height <= 1280)
		{
			EnableLevel(1);
		}
		else if (Screen.width <= 1080 && Screen.height <= 1920)
		{
			EnableLevel(2);
		}
		else if (Screen.width <= 1080 && Screen.height <= 2160)
		{
			EnableLevel(3);
		}
		else if (Screen.width <= 1440 && Screen.height <= 2560)
		{
			EnableLevel(4);
		}
		else
		{
			EnableLevel(2);
		}
    }

    // Update is called once per frame
    void Update()
    {
		
		
    }

	void EnableLevel(int i)
	{
		foreach (var item in levelMenu)
		{
			item.gameObject.SetActive(false);
		}

		levelMenu[i].gameObject.SetActive(true);
	}
}
