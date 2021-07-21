using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Generator : MonoBehaviour
{
	public GameObject[] platforms;
	public GameObject parentPlatform;
	public Text platformNumbers;

	public GameObject GM_Menu;
	public GameObject GameMaster;
	public LevelCreate levelCreate;

	public Text levelNumber;
	public Text levelNumberEnter;

	public GameObject[] shapes;
	public GameObject parentShapes;
	public Text shapesNumbers;
	public List<Shape> shapesList = new List<Shape>();

	public ListShape ls;

	float[] rotations = { -180f, -135.5f, -90.5f, -45.5f, 0f, 45.5f, 90.5f, 135.5f };

	private void Start()
	{
		levelNumber.text = PlayerPrefs.GetInt("levelIndex", 1).ToString();
	}

	public void ReloadLevelNumber()
	{
		if (levelNumberEnter.text != "")
			PlayerPrefs.SetInt("levelIndex", int.Parse(levelNumberEnter.text));
		levelNumber.text = PlayerPrefs.GetInt("levelIndex", 1).ToString();
	}

	public void PlatformGenerator()
	{
		for (int i = 0; i < int.Parse(platformNumbers.text); i++)
		{
			GameObject platform = platforms[(int)Random.Range(0, platforms.Length)];

			float posPlatx = Random.Range(-2f, 2f);
			float posPlaty = Random.Range(-4f, 2f);

			float scale = Random.Range(0.2f, 1.5f);

			float rot = rotations[Random.Range(0, 8)];

			GameObject newPlat = Instantiate(platform, new Vector3(posPlatx, posPlaty, 1), new Quaternion(0, 0, rot, 0));
			newPlat.transform.parent = parentPlatform.transform;
			newPlat.transform.localScale = new Vector3(scale, scale, 1);
		}
	}

	public void ShapesGenerator()
	{
		List<Shape> myShapes = new List<Shape>();
		for (int i = 0; i < int.Parse(shapesNumbers.text); i++)
		{
			//add new shape to list
			Shape shape = new Shape();

			shape.prefabShape = shapes[Random.Range(0, shapes.Length)].name;
			shape.rotationZ = rotations[Random.Range(0, 8)];
			shape.level = int.Parse(levelNumber.text);

			float scale = Random.Range(0.2f, 1.5f);

			shape.x = scale;
			shape.y = scale;

			myShapes.Add(shape);
		}

		shapesList = myShapes;
		ReloadLevel();
	}

	public void AddOneShape()
	{
		Shape shape = new Shape();

		shape.prefabShape = shapes[Random.Range(0, shapes.Length)].name;
		shape.rotationZ = rotations[Random.Range(0, 8)];
		shape.level = int.Parse(levelNumber.text);

		float scale = Random.Range(0.2f, 1.5f);

		shape.x = scale;
		shape.y = scale;

		shapesList.Add(shape);
	}

	public void SaveMyLevel()
	{
		levelCreate.shapes = shapesList;
		levelCreate.SaveLevelFunc();
		SceneManager.LoadScene(3);
	}

	public void ReloadLevel()
	{
		GM_Menu.SetActive(false);
		ls.LoadLevelTest(shapesList);
		ReloadLevelNumber();
		GameMaster.SetActive(true);
		GameMaster.GetComponent<SpawnObject>().ResetValue();
		GameMaster.GetComponent<SpawnObject>().RemoveAllShape();
	}
}
