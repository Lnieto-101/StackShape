using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Advertisements;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.Events;
using System;

public class SpawnObject : MonoBehaviour
{
	[Header("Dependencies")]
	public GameObject cursor;
	public ListShape listShape;
	public LevelCreate level;
	public GameObject pause;
	public GameObject continueUi;
	public GameObject pauseMenu;
	public GameObject winParticle;
	public GameObject levelText;
	public GameObject menuButton;
	public GameObject retryButton;
	
	
	[Header("This is create mode ?")]
	public bool create = false;

	//temp name and var
	[Header("Platforms and Shapes")]
	public GameObject[] shapesPrefab;
	public GameObject[] prefabPlatforms;
	public AudioSource spawnSong;

	[HideInInspector]
	public List<Shape> shapes = new List<Shape>();
	[HideInInspector]
	public List<Transform> platforms = new List<Transform>();

	//platformContainer
	public GameObject pc;
	public GameObject sc;

	float countdownWin = 2;
	float timeMove = 0;
	int index = 0;

	[Header("Shape info")]
	public bool oneTimeScale = false;
	public float zRot = 0.25f;
	public float xScale = 0.4f;
	public float yScale = 0.4f;

	[Header("Level index (start from 1)")]
	public int levelIndex = 1;

	[Header("UI ref")]
	public GameObject winMenu;

	[Header("AD google")]
	public UnityEvent OnAdLoadedEvent;

	public void ResetValue()
	{
		countdownWin = 2;
		index = 0;
		win = false;

		if (oneTimeScale)
		{
			zRot = 0.25f;
			xScale = 0.4f;
			yScale = 0.4f;
		}
	}

	#region HELPER METHODS

	private AdRequest CreateAdRequest()
	{
		return new AdRequest.Builder()
			.AddTestDevice(AdRequest.TestDeviceSimulator)
			.AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
			.AddKeyword("unity-admob-sample")
			.TagForChildDirectedTreatment(false)
			.AddExtra("color_bg", "9B30FF")
			.Build();
	}

	#endregion

	private InterstitialAd interstitial;

	public void ShowInterstitialAd()
	{

        string adUnitId = "ca-app-pub-2807814307137078/3167740816";

		// Clean up interstitial before using it
		if (interstitial != null)
		{
			interstitial.Destroy();
		}

		interstitial = new InterstitialAd(adUnitId);

		interstitial.OnAdLoaded += (sender, args) => OnAdLoadedEvent.Invoke();

		interstitial.LoadAd(CreateAdRequest());
		
	}

	private void Start()
	{
		// Initialize the Google Mobile Ads SDK.
		//MobileAds.Initialize(initStatus => { });
		List<String> deviceIds = new List<String>() { AdRequest.TestDeviceSimulator };

		deviceIds.Add("75EF8D155528C04DACBBA6F36F433035");

		RequestConfiguration requestConfiguration =
			new RequestConfiguration.Builder()
			.SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.True)
			.SetTestDeviceIds(deviceIds).build();

		MobileAds.SetRequestConfiguration(requestConfiguration);
		MobileAds.Initialize(HandleInitCompleteAction);

		

		sc = GameObject.Find("ShapeContainer");
		if (!create)
			RemoveAllShape();

		pc = GameObject.Find("PlatformContainer");
		if (!create)
			RemoveAllPlatform();

		cursor.GetComponent<Cursor>().zRotCursor = zRot;

		levelIndex = PlayerPrefs.GetInt("levelIndex");


		//NOT IN CREATE MODE
		/*if (!create)
		{
			platforms = SaveSystem.loadPlatform(levelIndex);

			//create each platform of this level
			foreach (var item in platforms)
			{
				GameObject actualPlat = Instantiate(prefabPlatforms[int.Parse(item.name)], (Vector3)item.position, item.rotation);
				actualPlat.transform.SetParent(pc.transform);
				actualPlat.transform.localScale = item.localScale;
				Destroy(item.gameObject);
			}
		}*/
	}

	private void HandleInitCompleteAction(InitializationStatus initstatus)
	{
		// Callbacks from GoogleMobileAds are not guaranteed to be called on
		// main thread.
		// In this example we use MobileAdsEventExecutor to schedule these calls on
		// the next Update() loop.
		MobileAdsEventExecutor.ExecuteInUpdate(() => {
			Debug.Log("AdRun");
		});
	}

	public void RemoveAllShape()
	{
		//clear shapes
		for (int i = 0; i < sc.transform.childCount; i++)
		{
			Destroy(sc.transform.GetChild(i).gameObject);
		}
		
	}

	public void RemoveAllPlatform()
	{
		//clear platforms
		for (int i = 0; i < pc.transform.childCount; i++)
		{
			Destroy(pc.transform.GetChild(i).gameObject);
		}
		
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

	public bool win = false;

	public void SpawnPlatforms(int lvlIndex)
	{
		levelIndex = lvlIndex;
		pause.SetActive(true);
		retryButton.SetActive(true);
		levelText.GetComponent<Text>().text = "LEVEL " + levelIndex.ToString();
		menuButton.SetActive(false);
		

		win = false;
		platforms = SaveSystem.loadPlatform(levelIndex);

		RemoveAllShape();
		RemoveAllPlatform();

		//create each platform of this level
		foreach (var item in platforms)
		{
			GameObject actualPlat = Instantiate(prefabPlatforms[int.Parse(item.name)], (Vector3)item.position, item.rotation);
			actualPlat.transform.SetParent(pc.transform);
			actualPlat.transform.localScale = item.localScale;
			Destroy(item.gameObject);
		}
		
	}

	public int WinFunc()
	{
		//NOT IN CREATE MODE
		//WIN
		if (!create && index >= listShape.shapes.Count && !win)
		{
			pause.SetActive(false);
			pauseMenu.SetActive(false);
			continueUi.SetActive(false);
			bool oneMoving = false;
			cursor.SetActive(false);
			List<GameObject> shapeInWorld = SaveSystem.ChildToList(sc);

			foreach (var item in shapeInWorld)
			{
				if (item.GetComponent<ShapeColorControl>().moving)
					oneMoving = true;
			}

			if (oneMoving)
				timeMove = 3;
			else
				timeMove = 0;

			if (countdownWin + timeMove > 0)
			{
				countdownWin -= Time.deltaTime;
			}
			else
			{
				ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "/Screen.png");

				GameObject winPart = Instantiate(winParticle, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
				Destroy(winPart, 3);

				//winMenu.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(true);
				winMenu.SetActive(true);
				win = true;
				//RemoveAllShape();
				SaveSystem.SaveLevel(levelIndex);
			}
			return 1;
		}
		return 0;
	}

	public void NextLevel()
	{
		int nextLevelInt = levelIndex + 1;
	}

	/* channel = new AndroidNotificationChannel()
	{
		Id = "channel_id",
		Name = "Default Channel",
		Importance = Importance.Default,
		Description = "Generic notifications",
	};
	AndroidNotificationCenter.RegisterNotificationChannel(channel);*/

	float bonusOffset;
	public bool showAd = false;

	public void ShowMyIntersitial()
	{
		showAd = true;
		/*if (interstitial != null)
		{
			if (interstitial.IsLoaded())
			{
				interstitial.Show();
				PlayerPrefs.SetInt("countAds", 0);
			}
			else
			{
				Debug.Log("not loaded");
			}
		}
		else
		{
			Debug.Log("testtest");
		}*/
	}

	public void AdGestion()
	{

		//ShowInterstitialAd();
		if (PlayerPrefs.GetInt("countAds", 0) == 5)
		{
			ShowInterstitialAd();
			PlayerPrefs.SetInt("countAds", 0);
			//this.interstitial.Destroy();
		}
		else
		{
			PlayerPrefs.SetInt("countAds", PlayerPrefs.GetInt("countAds", 0) + 1);
		}

		if (showAd)
		{
			if (interstitial != null)
			{
				if (interstitial.IsLoaded())
				{
					interstitial.Show();
					PlayerPrefs.SetInt("countAds", 0);
					showAd = false;
				}
				else
				{
					Debug.Log("not loaded");
				}
			}
			else
			{
				Debug.Log("testtest");
			}
		}
	}

	// Update is called once per frame
	void Update()
    {
		
		//NOT IN CREATE MODE
		if (!create)
		{
			if (listShape.shapes.Capacity <= 0)
				return;
			if (WinFunc() == 1)
				return;
		}
		//NOT IN CREATE MODE
		if (!create && !win && index < listShape.shapes.Count)
		{
			int shapeIndex = ShapeSelect(listShape.shapes[index].prefabShape);

			cursor.transform.rotation = Quaternion.Euler(0, 0, listShape.shapes[index].rotationZ);
			cursor.GetComponent<Cursor>().index_ = shapeIndex;
			//cursor.SetActive(true);
		}

		if (!create && index < listShape.shapes.Count)
		{
			cursor.GetComponent<Cursor>().zRotCursor = listShape.shapes[index].rotationZ;
			cursor.GetComponent<Cursor>().xScaleCursor = listShape.shapes[index].x;
			cursor.GetComponent<Cursor>().yScaleCursor = listShape.shapes[index].y;
		}

		if (Input.touchCount >= 1)
		{
			if (Input.GetTouch(0).phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(0) && !win && !pauseMenu.activeSelf)
			{
				Vector3 posObject = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - cursor.GetComponent<Cursor>().xScaleCursor, Camera.main.ScreenToWorldPoint(Input.mousePosition).y + cursor.GetComponent<Cursor>().yScaleCursor, 0);
				cursor.transform.position = posObject;
				transform.rotation = Quaternion.Euler(0, 0, cursor.GetComponent<Cursor>().zRotCursor);
				transform.localScale = new Vector3(cursor.GetComponent<Cursor>().xScaleCursor, cursor.GetComponent<Cursor>().yScaleCursor, 0.4f);
				cursor.SetActive(true);
			}

			if (Input.GetTouch(0).phase == TouchPhase.Ended && !cursor.GetComponent<Cursor>().isTriggered && !win && cursor.gameObject.activeSelf && !pauseMenu.activeSelf && index < listShape.shapes.Count && !EventSystem.current.IsPointerOverGameObject(0))
			{
				Vector3 mousPos = Input.mousePosition;
				Vector3 posObject = new Vector3(Camera.main.ScreenToWorldPoint(mousPos).x, Camera.main.ScreenToWorldPoint(mousPos).y, 0);

				//NOT IN CREATE MODE
				if (!create)
				{
					int shapeIndex = ShapeSelect(listShape.shapes[index].prefabShape);

					//spawn first child in world
					GameObject shapeObj = Instantiate(shapesPrefab[shapeIndex], cursor.transform.position, Quaternion.Euler(0, 0, listShape.shapes[index].rotationZ));
					shapeObj.transform.SetParent(sc.transform);
					shapeObj.transform.localScale = new Vector3(listShape.shapes[index].x, listShape.shapes[index].y, 0.4f);
					shapeObj.GetComponent<Rigidbody2D>().mass = listShape.shapes[index].x * listShape.shapes[index].y;
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

					//remove first child from UI list
					listShape.RemoveFirstChild();
					cursor.SetActive(false);
				}

				//CREATE MODE
				/*if (create && !EventSystem.current.IsPointerOverGameObject())
				{
					//spawn new object

					if (yScale < 0.5f)
						bonusOffset = 0.3f;

					float yOffset = yScale + bonusOffset;

					if (yOffset > 1)
						yOffset = 1;
					posObject = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y + yOffset * 2, 0);
					GameObject newObj = Instantiate(shapesPrefab[cursor.GetComponent<Cursor>().index_], posObject, Quaternion.Euler(0, 0, zRot));
					newObj.transform.localScale = new Vector3(xScale, yScale, 0.4f);
					newObj.GetComponent<Rigidbody2D>().mass = xScale * yScale;

					//add new shape to list
					Shape shape = new Shape();

					shape.prefabShape = shapesPrefab[cursor.GetComponent<Cursor>().index_].name;
					shape.rotationZ = zRot;
					shape.level = levelIndex;
					shape.x = xScale;
					shape.y = yScale;

					shapes.Add(shape);

					level.shapes = shapes;
					ResetValue();
				}*/

				index++;
			}

			if (Input.GetTouch(0).phase == TouchPhase.Ended && !win && !create)
			{
				cursor.SetActive(false);
			}
		}

		//ONLY CREATE

		//rotate right
		if (Input.GetKeyDown(KeyCode.E) && create)
		{
			zRot -= 5;
			cursor.GetComponent<Cursor>().zRotCursor = zRot;
		}

		if (create)
		{
			cursor.GetComponent<Cursor>().zRotCursor = zRot;
			cursor.GetComponent<Cursor>().xScaleCursor = xScale;
			cursor.GetComponent<Cursor>().yScaleCursor = yScale;
		}

		//rotate left
		if (Input.GetKeyDown(KeyCode.Q) && create)
		{
			zRot += 5;
			cursor.GetComponent<Cursor>().zRotCursor = zRot;
		}

		if (Input.GetKeyDown(KeyCode.S) && create)
		{
			PlayerPrefs.SetInt("levelIndex", levelIndex);
		}

		//load platforms
		if (Input.GetKeyDown(KeyCode.Z) && create)
		{
			RemoveAllPlatform();

			platforms = SaveSystem.loadPlatform(levelIndex);

			//create each platform of this level
			foreach (var item in platforms)
			{
				GameObject actualPlat = Instantiate(prefabPlatforms[int.Parse(item.name)], (Vector3)item.position, item.rotation);
				actualPlat.transform.SetParent(pc.transform);
				actualPlat.transform.localScale = item.localScale;
				Destroy(item.gameObject);
			}
		}

		if (Input.GetKeyDown(KeyCode.F))
		{
			SaveSystem.ResetProgress();
			Debug.Log("nice");
		}

		if (Input.GetKeyDown(KeyCode.J))
		{
			SaveSystem.SaveLevel(PlayerPrefs.GetInt("levels"));
			Debug.Log("nice");
		}

		if (Input.GetKeyDown(KeyCode.G))
		{
			SaveSystem.LoadLevel();
		}

		if (Input.GetKeyDown(KeyCode.M))
		{
			PlayerPrefs.SetInt("alreadyPlay", 0);
		}
    }
}
