using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManagerLine : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
    {
		int progress;
		progress = SaveSystem.LoadLevel();
		List<GameObject> childList = SaveSystem.ChildToList(gameObject);

		foreach (var item in childList)
		{
			int levelName = int.Parse(item.gameObject.name);
			if (levelName > progress + 1)
			{
				//lock
				item.gameObject.GetComponent<Image>().color = Color.red;
				item.gameObject.GetComponent<Button>().enabled = false;
			}
			else if (levelName < progress + 1)
			{
				//win
				item.gameObject.GetComponent<Image>().color = Color.green;
				item.gameObject.GetComponent<Button>().enabled = true;
			}
			else
			{
				//can do
				item.gameObject.GetComponent<Button>().enabled = true;
			}
		}
    }
}
