using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
	public GameObject cursor;
	GameObject GameMaster;
	public ListShape list;
	public GameObject gameOverUI;
	public GameObject winUi;
	public GameObject pauseUi;
	public GameObject continueUi;
	public GameObject PauseMenu;
	public bool minigame;

	private void Start()
	{
		GameMaster = GameObject.Find("GameMaster");
	}

	/*private void OnCollisionEnter2D(Collision2D collision)
	{
		if (!GameMaster.GetComponent<SpawnObject>().win)
		{
			GameMaster.SetActive(false);
			cursor.SetActive(false);
			gameOverUI.SetActive(true);
			continueUi.SetActive(false);
			pauseUi.SetActive(false);
			PauseMenu.SetActive(false);
		}
	}*/

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!minigame)
		{
			if (!GameMaster.GetComponent<SpawnObject>().win)
			{
				GameMaster.SetActive(false);
				cursor.SetActive(false);
				gameOverUI.SetActive(true);
				continueUi.SetActive(false);
				pauseUi.SetActive(false);
				PauseMenu.SetActive(false);
				Destroy(collision.gameObject);
			}
		}
		else
		{
			if (collision.GetComponent<Rigidbody2D>())
			{
				cursor.SetActive(false);
				gameOverUI.SetActive(true);
				continueUi.SetActive(false);
				pauseUi.SetActive(false);
				PauseMenu.SetActive(false);
				Destroy(collision.gameObject);
			}
			else
			{
				Destroy(collision.gameObject);
			}
		}


	}

	public void Reload()
	{
		int level = GameMaster.GetComponent<SpawnObject>().levelIndex;
		gameOverUI.SetActive(false);
		winUi.SetActive(false);
		continueUi.SetActive(false);
		pauseUi.SetActive(true);
		PauseMenu.SetActive(false);
		list.LoadLevelFunc(level);
		GameMaster.SetActive(true);
		GameMaster.GetComponent<SpawnObject>().ResetValue();
		GameMaster.GetComponent<SpawnObject>().RemoveAllShape();
	}
}
