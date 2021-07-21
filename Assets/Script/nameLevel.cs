using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nameLevel : MonoBehaviour
{

	List<GameObject> levelsHolder;

    // Start is called before the first frame update
    void OnEnable()
    {
		levelsHolder = SaveSystem.ChildToList(gameObject);
		for (int i = 0; i < transform.childCount; i++)
		{
			levelsHolder[i].transform.GetChild(0).name = (1 + (4 * i)).ToString();
			levelsHolder[i].transform.GetChild(1).name = (2 + (4 * i)).ToString();
			levelsHolder[i].transform.GetChild(2).name = (3 + (4 * i)).ToString();
			levelsHolder[i].transform.GetChild(3).name = (4 + (4 * i)).ToString();
		}
    }
}
