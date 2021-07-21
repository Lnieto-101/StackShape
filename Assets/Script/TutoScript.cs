using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoScript : MonoBehaviour
{
	public GameObject buttonFirst;
	public GameObject listHollow;
	public GameObject textHollow;

	public GameObject textShape;
	public GameObject ImageShape;

	public GameObject releaseText;

	public GameObject resetText;
	public GameObject resetHollow;

	public GameObject winText;

	int part = 0;

	private void Start()
	{
		PlayerPrefs.SetInt("alreadyPlay", 1);
	}

	public void FirstTuto()
	{
		buttonFirst.SetActive(false);
		listHollow.SetActive(false);
		textHollow.SetActive(false);

		textShape.SetActive(true);
		ImageShape.SetActive(true);
		part = 1;
	}

	private void Update()
	{
		if (!(Input.touchCount >= 1))
			return;
		if (part == 1)
		{
			if (Input.GetTouch(0).phase == TouchPhase.Began)
			{
				textShape.SetActive(false);
				ImageShape.SetActive(false);

				releaseText.SetActive(true);
				part = 2;
			}
		}
		else if (part == 2)
		{
			if (Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				releaseText.SetActive(false);

				resetHollow.SetActive(true);
				resetText.SetActive(true);
				part = 3;
			}
		}
		else if (part == 3)
		{
			if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Began)
			{
				resetHollow.SetActive(false);
				resetText.SetActive(false);

				winText.SetActive(true);
				part = 4;
			}
		}
		else if (part == 4)
		{
			if (Input.GetTouch(0).phase == TouchPhase.Began)
			{
				winText.SetActive(false);
				part = 5;
			}
		}

	}
}
