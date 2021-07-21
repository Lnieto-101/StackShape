using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuto : MonoBehaviour
{
	public SpawnObject s_o;
	public ListShape l_s;
    // Start is called before the first frame update
    void Start()
    {
		s_o = gameObject.GetComponent<SpawnObject>();
		s_o.SpawnPlatforms(0);
		l_s.LoadLevelFunc(0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
