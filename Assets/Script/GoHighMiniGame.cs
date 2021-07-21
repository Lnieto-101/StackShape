using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;
using Random = UnityEngine.Random;

public class GoHighMiniGame : MonoBehaviour
{
	GameObject ads;

	public GameObject[] shapes;
	public GameObject parentShapes;
	public GameObject parentPlatform;
	public List<Shape> shapesList = new List<Shape>();
	public Sprite[] shapeSpriteList;
	public GameObject tuto;

	public Image actualShape;
	public Image nextShape;


	public GameObject cursor;

	public AudioSource spawnSong;

	public float countdownToFreeze;
	private float countdown;


	public void ShapesGenerator()
	{
		List<Shape> myShapes = new List<Shape>();
		for (int i = 0; i < 2; i++)
		{
			//add new shape to list
			Shape shape = new Shape();

			shape.prefabShape = shapes[Random.Range(0, shapes.Length)].name;
			shape.rotationZ = 0;
			shape.level = 0;

			float scale = Random.Range(0.4f, 0.6f);

			shape.x = scale;
			shape.y = scale;

			myShapes.Add(shape);
		}

		shapesList = myShapes;
		actualShape.GetComponent<Image>().sprite = shapeSpriteList[ShapeSelect(shapesList[0].prefabShape)];
		nextShape.GetComponent<Image>().sprite = shapeSpriteList[ShapeSelect(shapesList[1].prefabShape)];
	}

	public void NextShape()
	{
		Shape shape = new Shape();

		shape.prefabShape = shapes[Random.Range(0, shapes.Length)].name;
		shape.rotationZ = 0;
		shape.level = 0;

		float scale = Random.Range(0.4f, 0.6f);

		shape.x = scale;
		shape.y = scale;

		shapesList.Add(shape);

		//nextShape.GetComponent<Image>().sprite = shapeSpriteList[ShapeSelect(shape.prefabShape)];
	}

	int ShapeSelect(string shape)
	{
		int shapeIndex = 0;

		//select shape
		if (shape == "Square")
			shapeIndex = 0;
		if (shape == "Circle")
			shapeIndex = 1;
		if (shape == "Triangle")
			shapeIndex = 2;
		if (shape == "Bar")
			shapeIndex = 3;
		if (shape == "Cross")
			shapeIndex = 4;

		return shapeIndex;
	}

	private void Start()
	{
		ads = GameObject.Find("NewAds");
		ShapesGenerator();

	}

	public void adgestion()
	{
		ads.GetComponent<launchAds>().AdGestion();
	}

	int index = 0;

	float starty = 0;

	int idfing;

	private void Update()
	{ 
		int shapeIndex = ShapeSelect(shapesList[index].prefabShape);
		cursor.GetComponent<Cursor>().index_ = shapeIndex;

		cursor.GetComponent<Cursor>().xScaleCursor = shapesList[index].x;
		cursor.GetComponent<Cursor>().yScaleCursor = shapesList[index].y;

		if (Input.touchCount >= 1)
		{
			tuto.SetActive(false);
			if (Input.GetTouch(0).phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(0))
			{
				//show cursor
				//Debug.Log("omg");
				Vector3 posObject = new Vector3(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).x - cursor.GetComponent<Cursor>().xScaleCursor, Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).y + cursor.GetComponent<Cursor>().yScaleCursor, 0);
				cursor.transform.position = posObject;
				transform.rotation = Quaternion.Euler(0, 0, cursor.GetComponent<Cursor>().zRotCursor);
				transform.localScale = new Vector3(cursor.GetComponent<Cursor>().xScaleCursor, cursor.GetComponent<Cursor>().yScaleCursor, 0.4f);
				cursor.SetActive(true);
				idfing = Input.GetTouch(0).fingerId;
			}

			if (Input.GetTouch(0).phase == TouchPhase.Ended && idfing == Input.GetTouch(0).fingerId && !cursor.GetComponent<Cursor>().isTriggered && cursor.activeSelf)
			{
				//Debug.Log("what");
				countdown = countdownToFreeze;

				//spawn first child in world
				GameObject shapeObj = Instantiate(shapes[shapeIndex], cursor.transform.position, Quaternion.Euler(0, 0, cursor.GetComponent<Cursor>().zRotCursor));
				shapeObj.transform.SetParent(parentShapes.transform);
				shapeObj.GetComponent<ShapeColorControl>().toFreeze = true;
				shapeObj.GetComponent<ShapeColorControl>().platformParent = parentPlatform;
				shapeObj.transform.localScale = new Vector3(shapesList[index].x, shapesList[index].y, 0.4f);
				shapeObj.GetComponent<Rigidbody2D>().mass = shapesList[index].x * shapesList[index].y;

				if (shapeObj.GetComponent<Rigidbody2D>().mass < 0.15)
				{
					spawnSong.pitch = 2;
				}
				else if (shapeObj.GetComponent<Rigidbody2D>().mass > 1.3)
				{
					spawnSong.pitch = 0.4f;
				}
				else
				{
					spawnSong.pitch = 1;
				}
				if (PlayerPrefs.GetInt("Sound", 1) == 1)
					spawnSong.Play();

				cursor.SetActive(false);
				index++;
				NextShape();

				actualShape.GetComponent<Image>().sprite = shapeSpriteList[ShapeSelect(shapesList[index].prefabShape)];
				nextShape.GetComponent<Image>().sprite = shapeSpriteList[ShapeSelect(shapesList[index + 1].prefabShape)];

			}

			if (Input.touchCount >= 2)
			{
				if (Input.GetTouch(1).phase == TouchPhase.Began)
				{
					starty = Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position).y;
				}

				if (Input.GetTouch(1).phase == TouchPhase.Moved)
				{
					
					float ynow = Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position).y;
					float myRotz = cursor.GetComponent<Cursor>().zRotCursor;
					if (starty > ynow)
						cursor.GetComponent<Cursor>().zRotCursor = myRotz + (-(ynow - starty) * 15);
					else if (starty < ynow)
						cursor.GetComponent<Cursor>().zRotCursor = myRotz + (-(ynow - starty) * 15);
					//Debug.Log(cursor.transform.eulerAngles);
					starty = Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position).y;
				}
			}

			if (Input.GetTouch(0).phase == TouchPhase.Ended && idfing == Input.GetTouch(0).fingerId)
			{
				cursor.SetActive(false);
			}
		}
		else
		{
			return;
		}
	}
}
