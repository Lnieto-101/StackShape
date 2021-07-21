using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
		PlayerPrefs.GetInt("test", 1);
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.V))
		{
			ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "/Screen" + PlayerPrefs.GetInt("test", 1).ToString() + ".png");
			PlayerPrefs.SetInt("test", PlayerPrefs.GetInt("test", 1) + 1);
		}
    }
}
