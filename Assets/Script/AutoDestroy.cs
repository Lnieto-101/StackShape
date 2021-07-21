using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
	public float countdown = 4f;

    // Countdown to destroy gameobjects
    void Update()
    {
		countdown -= Time.deltaTime;
		if (countdown <= 0)
		{
			Destroy(gameObject);
		}
    }
}
