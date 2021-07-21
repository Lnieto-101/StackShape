using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListShape : MonoBehaviour
{
	[HideInInspector]
	public List<Shape> shapes = new List<Shape>();
	
	public RectTransform rect;
	[HideInInspector]
	int indexPrefab = 0;

	[Header("Are buttons inactive ?")]
	public bool button = false;

	//temp prefab image
	[Header("Dependencies")]
	public GameObject[] prefabImage;
	public GameObject cursor;

	[Header("Shapes")]
	public Sprite[] shapeSpriteList;

	private void Start()
	{
		rect = gameObject.GetComponent<RectTransform>();

		if (Screen.width <= 480 && Screen.height <= 800)
		{
			indexPrefab = 0;
		}
		else if (Screen.width == 720 && Screen.height == 1280)
		{
			indexPrefab = 1;
		}
		else if (Screen.width == 1080 && Screen.height == 1920)
		{
			indexPrefab = 2;
		}
		else if (Screen.width == 1080 && Screen.height == 2160)
		{
			indexPrefab = 3;
		}
		else if (Screen.width >= 1440 && Screen.height >= 2560)
		{
			indexPrefab = 4;
		}
		else
		{
			indexPrefab = 1;
		}

		//if (button)
		//	LoadLevelFunc(1);
	}

	public void LoadLevelFunc(int levelIndex)
	{
		//clear actual list
		for (int i = 0; i < transform.childCount; i++)
		{
			Destroy(transform.GetChild(i).gameObject);
		}

		List<Shape> data = SaveSystem.LoadArray(levelIndex);

		shapes = data;
		//put each shape logo in panel on top
		foreach (var item in data)
		{
			int i = 0;

			GameObject imageFab = (GameObject)Instantiate(prefabImage[indexPrefab], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
			imageFab.transform.SetParent(transform);

			if (item.prefabShape == "Square")
			{
				if (item.x == item.y)
					i = 0;
				else
					i = 3;
			}
				
			if (item.prefabShape == "Circle")
				i = 1;
			if (item.prefabShape == "Triangle")
				i = 2;
			if (item.prefabShape == "Bar")
				i = 3;
			if (item.prefabShape == "Cross")
				i = 4;

			imageFab.GetComponent<Image>().sprite = shapeSpriteList[i];
			//resize panel horizontal layout
			rect.sizeDelta = new Vector2(rect.sizeDelta.x + 400, rect.sizeDelta.y);
			//put 0 on left
			rect.offsetMin = new Vector2(0, rect.offsetMin.y);
		}
	
	}

	public void LoadLevelTest(List<Shape> shapeL)
	{
		//clear actual list
		for (int i = 0; i < transform.childCount; i++)
		{
			Destroy(transform.GetChild(i).gameObject);
		}

		List<Shape> data = shapeL;

		shapes = data;
		//put each shape logo in panel on top
		foreach (var item in data)
		{
			int i = 0;

			GameObject imageFab = (GameObject)Instantiate(prefabImage[indexPrefab], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
			imageFab.transform.SetParent(transform);

			if (item.prefabShape == "Square")
			{
				if (item.x == item.y)
					i = 0;
				else
					i = 3;
			}
				
			if (item.prefabShape == "Circle")
				i = 1;
			if (item.prefabShape == "Triangle")
				i = 2;
			if (item.prefabShape == "Bar")
				i = 3;
			if (item.prefabShape == "Cross")
				i = 4;

			imageFab.GetComponent<Image>().sprite = shapeSpriteList[i];
			//resize panel horizontal layout
			rect.sizeDelta = new Vector2(rect.sizeDelta.x + 400, rect.sizeDelta.y);
			//put 0 on left
			rect.offsetMin = new Vector2(0, rect.offsetMin.y);
		}
	
	}

	public void RemoveFirstChild()
	{
		Destroy(transform.GetChild(0).gameObject);
	}
}
