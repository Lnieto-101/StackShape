using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
	[HideInInspector]
	public float zRotCursor = 0.25f;
	public float xScaleCursor = 0.4f;
	public float yScaleCursor = 0.4f;
	public bool isTriggered = false;
	public bool firstReset = true;

	[Header("Shapes")]
	public Sprite[] shapeSprite;

	public int index_ = 0;

	PolygonCollider2D poly;

	private void Start()
	{
		poly = gameObject.GetComponent<PolygonCollider2D>();
	}

	float xOffset;
	float yOffsetValue;
	float yOffset;
	float bonusOffset;
	public float bonusOffsetSet = 0.3f;

	// Update is called once per frame
	void Update()
    {
	    //set the right shape and collider
		if (gameObject.GetComponent<SpriteRenderer>().sprite != shapeSprite[index_])
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = shapeSprite[index_];
			ResetPolygon();
		}

		//offset on cursor
		if (yScaleCursor < 0.5f)
			bonusOffset = bonusOffsetSet;

		xOffset = xScaleCursor;
		yOffset = yScaleCursor + 0.2f + bonusOffset;

		if (yOffset > 1.7)
			yOffset = 1.7f;
		if (Input.touchCount >= 1)
		{
			Vector3 posObject = new Vector3(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).x, Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).y + (yOffset * 2), 0);
			transform.position = posObject;
			
		}
		Debug.Log(Input.touchCount);
		transform.rotation = Quaternion.Euler(0, 0, zRotCursor);
		transform.localScale = new Vector3(xScaleCursor, yScaleCursor, 0.4f);

		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (index_ < shapeSprite.Length - 1)
				gameObject.GetComponent<SpriteRenderer>().sprite = shapeSprite[++index_];
			else
				index_ = 0;
		}

		//Valid location
		if (isTriggered)
		{
			gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 100, 100, 100);
		}
		else
		{
			gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 100);
		}
		gameObject.GetComponent<SpriteRenderer>().enabled = true;
	}

	private void OnEnable()
	{
		gameObject.GetComponent<SpriteRenderer>().enabled = false;

		yOffsetValue = yScaleCursor + bonusOffset;

		ResetPolygon();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		isTriggered = true;
		Debug.Log("trigger");
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		isTriggered = false;
		Debug.Log("false");
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		isTriggered = true;
	}

	public void ResetPolygon()
	{
		Destroy(poly);
		poly = gameObject.AddComponent<PolygonCollider2D>();
		poly.isTrigger = true;
	}
}
