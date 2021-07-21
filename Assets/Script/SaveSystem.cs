using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEditor;
using SimpleJSON;

public static class SaveSystem
{
	public static void SaveArray(List<Shape> shapes)
	{
		string path = Application.streamingAssetsPath + "/level" + shapes[0].level.ToString() + ".json";

		//json string maker
		//open shapes
		string outputString = "{\n \"Shapes\": [";

		//get all shapes
		foreach (var item in shapes)
		{
			outputString += JsonUtility.ToJson(item) + ",";
			Debug.Log(outputString);
		}

		//close shapes
		outputString += "\n	]\n";

		//open platform
		outputString += "\"Platform\": [";

		//get all created platform
		GameObject platformContainer = GameObject.Find("PlatformContainer");
		List<GameObject> childList = ChildToList(platformContainer);

		//save transform of each platform
		foreach (var child in childList)
		{
			CustomTransform transf = new CustomTransform();

			transf.shape = child.GetComponent<PlatformInfo>().nameInt;

			transf.posX = child.transform.position.x;
			transf.posY = child.transform.position.y;
			transf.posZ = child.transform.position.z;

			transf.rotX = child.transform.rotation.eulerAngles.x;
			transf.rotY = child.transform.rotation.eulerAngles.y;
			transf.rotZ = child.transform.rotation.eulerAngles.z;

			transf.scaleX = child.transform.localScale.x;
			transf.scaleY = child.transform.localScale.y;
			transf.scaleZ = child.transform.localScale.z;

			outputString += JsonUtility.ToJson(transf) + ",";
		}

		//close platform
		outputString += "\n	]\n";

		//end json
		outputString += "}";

		//put all in a file
		File.WriteAllText(path, outputString);

		//Debug.Log(outputString);
	}

	public static void SaveLevel(int progress)
	{
		if (progress <= LoadLevel())
			return;

		PlayerInfo player = new PlayerInfo();
		player.progress = progress;
		player.totalLevel = 40;

		string path = Application.persistentDataPath + "/progress.json";

		string outputString = JsonUtility.ToJson(player);
		Debug.Log(outputString);

		//put all in a file
		File.WriteAllText(path, outputString);

		//Debug.Log(outputString);
	}

	public static void ResetProgress()
	{

		PlayerInfo player = new PlayerInfo();
		player.progress = 0;
		player.totalLevel = 40;

		string path = Application.persistentDataPath + "/progress.json";

		string outputString = JsonUtility.ToJson(player);
		Debug.Log(outputString);

		//put all in a file
		File.WriteAllText(path, outputString);

		//Debug.Log(outputString);
	}

	public static int LoadLevel()
	{
		string path = Application.persistentDataPath + "/progress.json";

		if (File.Exists(path))
		{
			string inputString = File.ReadAllText(path);

			JSONNode data = JSON.Parse(inputString);

			return data["progress"];
		}
		else
		{
			return 0;
		}
	}

	public static int GetMaxLevel()
	{
		string path = Application.persistentDataPath + "/progress.json";

		if (File.Exists(path))
		{
			string inputString = File.ReadAllText(path);

			JSONNode data = JSON.Parse(inputString);

			Debug.Log(data["totalLevel"]);

			return data["totalLevel"];
		}
		else
		{
			return 0;
		}
	}

	public static List<Transform> loadPlatform(int level)
	{
		//string path = Application.streamingAssetsPath + "/level" + level.ToString() + ".json";
		/*
				if (File.Exists(path))
				{*/
		//read our save in path to get platforms
		BetterStreamingAssets.Initialize();
		if (!BetterStreamingAssets.FileExists("level" + level.ToString() + ".json"))
		{
			Debug.LogErrorFormat("Streaming asset not found: {0}", "level" + level.ToString() + ".json");
			return null;
		}
		string inputString = BetterStreamingAssets.ReadAllText("level" + level.ToString() + ".json");//File.ReadAllText(path);

			List<Transform> platforms = new List<Transform>();

			//parse all platform in jsonNode
			JSONNode data = JSON.Parse(inputString);
			
			//save transform to create each platform later
			foreach (JSONNode item in data["Platform"])
			{
				Transform tr = new GameObject().transform;

				tr.name = item["shape"].Value;
				tr.position = new Vector3(item["posX"].AsFloat, item["posY"].AsFloat, item["posZ"].AsFloat);
				tr.rotation = Quaternion.Euler(item["rotX"].AsFloat, item["rotY"].AsFloat, item["rotZ"].AsFloat);
				tr.localScale = new Vector3(item["scaleX"].AsFloat, item["scaleY"].AsFloat, item["scaleZ"].AsFloat);

				platforms.Add(tr);
			}

			return platforms;
		/*}
		//if file doesn't exist
		else
		{
			Debug.LogError("Save file not found in " + path);
			return null;
		}*/
	}

	public static List<Shape> LoadArray(int level)
	{
		//string path = Application.streamingAssetsPath + "/level" + level.ToString() + ".json";

		/*if(File.Exists(path))
		{*/
		//read our save in path to get shapes
		BetterStreamingAssets.Initialize();

		if (!BetterStreamingAssets.FileExists("level" + level.ToString() + ".json"))
		{
			Debug.LogErrorFormat("Streaming asset not found: {0}", "level" + level.ToString() + ".json");
			return null;
		}
		string inputString = BetterStreamingAssets.ReadAllText("level" + level.ToString() + ".json");//File.ReadAllText(path);

		List<Shape> shapes = new List<Shape>();

			//parse all shapes in jsonNode
			JSONNode data = JSON.Parse(inputString);

			//save shapes info to get a list of shapes
			foreach(JSONNode item in data["Shapes"])
			{
				Shape sp = new Shape();

				sp.prefabShape = item["prefabShape"].Value;
				sp.rotationZ = item["rotationZ"].AsFloat;
				sp.x = item["x"].AsFloat;
				sp.y = item["y"].AsFloat;
				sp.level = item["level"].AsInt;
				shapes.Add(sp);
			}

			return shapes;
	/*	}
		//if file doesn't exist
		else
		{
			Debug.LogError("Save file not found in " + path);
			return null;
		}*/
	}

	//create list of all child gameobject by putting parent in this func
	public static List<GameObject> ChildToList(GameObject parent)
	{
		List<GameObject> childList = new List<GameObject>();

		for (int i = 0; i < parent.transform.childCount; i++)
		{
			childList.Add(parent.transform.GetChild(i).gameObject);
		}

		return childList;
	}
}
