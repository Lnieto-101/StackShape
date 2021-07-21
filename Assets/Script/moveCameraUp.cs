using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCameraUp : MonoBehaviour
{
	public GameObject cam;
	public GameObject parent;
	public float speed;
	bool move = false;

	Vector3 camObjective;

	private void Update()
	{
		if (move)
		{
			Vector3 pos = cam.transform.position;
			camObjective = new Vector3(pos.x, pos.y + 0.1f, pos.z);
			cam.transform.position = Vector3.Lerp(cam.transform.position, camObjective, speed);
		}

		
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		move = false;
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		
		if (collision.gameObject.transform.parent.gameObject == parent.gameObject)
		{
			move = true;
		}
		else
		{
			move = false;
		}
	}
}
