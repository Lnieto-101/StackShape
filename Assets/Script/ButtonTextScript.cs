using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTextScript : MonoBehaviour
{
	public ListShape listShape;
	GameObject parentGO;
	public SpawnObject s_o;
    // Create level buttons
    void Start()
    {
		
		parentGO = transform.parent.transform.parent.transform.parent.gameObject;
		Debug.Log(parentGO);
		transform.GetChild(0).GetComponent<Text>().text = gameObject.name;
		GetComponent<Button>().onClick.AddListener(delegate { listShape.LoadLevelFunc(int.Parse(gameObject.name)); });
		GetComponent<Button>().onClick.AddListener(delegate { s_o.SpawnPlatforms(int.Parse(gameObject.name)); });
		
    }

	private void Update()
	{
		transform.GetChild(0).GetComponent<Text>().text = gameObject.name;
	}

	public void HideMenu()
	{
		parentGO.SetActive(false);
	}
}
