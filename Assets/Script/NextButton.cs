using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextButton : MonoBehaviour
{
	public ListShape listShape;
	public GameObject parentGO;
	public SpawnObject s_o;

	private void OnEnable()
	{
		int level = s_o.levelIndex + 1;
		if (level > PlayerPrefs.GetInt("levels"))
		{
			gameObject.SetActive(false);
			return;
		}
		s_o.ResetValue();
		GetComponent<Button>().onClick.AddListener(delegate { listShape.LoadLevelFunc(level); });
		GetComponent<Button>().onClick.AddListener(delegate { s_o.SpawnPlatforms(level); });
	}

	public void HideMenu()
	{
		parentGO.SetActive(false);
	}
}
