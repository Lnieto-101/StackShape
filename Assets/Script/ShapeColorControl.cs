using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeColorControl : MonoBehaviour
{
	Vector3 lastPos;
	Vector3 lastRot;
	float threshold = 0.06f;
	Rigidbody2D rb;
	public bool moving = false;
	bool shake = false;
	public bool toFreeze = false;
	float countdownFreeze = 2;
	float bonusCount = 0;
	public GameObject platformParent;
	// Start is called before the first frame update
	void Start()
    {
		
		lastPos = transform.position;
		rb = gameObject.GetComponent<Rigidbody2D>();
		StartCoroutine(MovingTestCoroutine());
	}

	private void Update()
	{
		if (toFreeze)
		{
			if (countdownFreeze + bonusCount <= 0)
			{
				FreezeShape();
			}
			else
			{
				countdownFreeze -= Time.deltaTime;
			}
		}
	}

	public void FreezeShape()
	{
		Destroy(gameObject.GetComponent<Rigidbody2D>());
		transform.parent = platformParent.transform;
		gameObject.GetComponent<SpriteRenderer>().color = new Color32(150, 150, 150, 255);
	}

	IEnumerator MovingTestCoroutine()
	{
		while (true && GetComponent<Rigidbody2D>())
		{
			if (TestMove())
			{
				moving = true;
				bonusCount = 3;
				gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 100, 100, 255);//soft red
			}
			else
			{
				moving = false;
				bonusCount = 0;
				gameObject.GetComponent<SpriteRenderer>().color = Color.white;
				
			}
			lastPos = transform.position;
			lastRot = transform.rotation.eulerAngles;
			yield return new WaitForSeconds(0.3f);
		}
	}

	bool TestMove()
	{
		if (lastPos.x > transform.position.x + threshold)
			return true;
		if (lastPos.x < transform.position.x - threshold)
			return true;
		if (lastPos.y > transform.position.y + threshold)
			return true;
		if (lastPos.y < transform.position.y - threshold)
			return true;
		if (lastRot.z > transform.rotation.eulerAngles.z + threshold)
			return true;
		if (lastRot.z < transform.rotation.eulerAngles.z - threshold)
			return true;

		return false;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (rb != null)
		{
			if (rb.mass > 1.3 && !shake)
			{
				StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake(0.3f, 0.05f * rb.mass));
				shake = true;
			}
		}
	}
}
