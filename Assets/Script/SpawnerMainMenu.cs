using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMainMenu : MonoBehaviour
{
	public float spawnRate = 0.5f;
	public GameObject[] prefabToSpawn;
	public Vector3 size;
	float countdown;
	private void Start()
	{
		countdown = spawnRate;
	}

	// Update is called once per frame
	void Update()
    {
        if (countdown <= 0 || Input.GetMouseButtonDown(0))
		{
			//spawn here
			GameObject MyGo = Instantiate(prefabToSpawn[Random.Range(0, prefabToSpawn.Length)], new Vector3(Random.Range(-3f, 3f), transform.position.y, 0), new Quaternion(0, 0, 0, 0));
			MyGo.GetComponent<Rigidbody2D>().gravityScale = 0.4f;
			MyGo.transform.localScale = size;
			MyGo.gameObject.AddComponent<AutoDestroy>();
			Destroy(MyGo.gameObject.GetComponent<ShapeColorControl>());
			countdown = spawnRate;
		}
		else
		{
			countdown -= Time.deltaTime;
		}
    }
}
