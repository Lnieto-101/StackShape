using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCreate : MonoBehaviour
{
	[HideInInspector]
	public List<Shape> shapes;

	public void SaveLevelFunc()
	{
		int levelID;
		if (PlayerPrefs.GetInt("levelIndex") == 0)
			PlayerPrefs.SetInt("levelIndex", 1);

		SaveSystem.SaveArray(shapes);
		
		levelID = PlayerPrefs.GetInt("levelIndex");
		PlayerPrefs.SetInt("levelIndex", ++levelID);
		//SceneManager.LoadScene(2);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.K))
		{
			SceneManager.LoadScene(2);
		}
	}
}
