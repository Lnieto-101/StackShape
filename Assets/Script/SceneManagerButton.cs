using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerButton : MonoBehaviour
{
	private void Start()
	{
		PlayerPrefs.SetInt("levels", 148);

		PlayerPrefs.GetInt("Sound", 1);
		PlayerPrefs.GetInt("Music", 1);
	}

	public void MusicToggle(bool music)
	{
		if (music)
			PlayerPrefs.SetInt("Music", 1);
		else
			PlayerPrefs.SetInt("Music", 0);
	}

	public void SoundToggle(bool sound)
	{
		if (sound)
			PlayerPrefs.SetInt("Sound", 1);
		else
			PlayerPrefs.SetInt("Sound", 0);
	}

	public void OpenLevelSelector()
	{
		if (PlayerPrefs.GetInt("alreadyPlay", 0) == 0)
			SceneManager.LoadScene(2);
		else
			SceneManager.LoadScene(1);
	}

	public void OpenTuto()
	{
		SceneManager.LoadScene(2);
	}

	public void OpenMiniGame()
	{
		SceneManager.LoadScene(3);
	}

	public void OpenMainMenu()
	{
		SceneManager.LoadScene(0);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
