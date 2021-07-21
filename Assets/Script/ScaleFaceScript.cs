using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleFaceScript : MonoBehaviour
{
	private void Start()
	{
		float xScaleParent = transform.parent.localScale.x;
		float yScaleParent = transform.parent.localScale.y;

		if (xScaleParent > yScaleParent)
		{
			transform.localScale = new Vector3(yScaleParent / xScaleParent, 1, 1);
		}
		else if (xScaleParent < yScaleParent)
		{
			transform.localScale = new Vector3(1, xScaleParent / yScaleParent, 1);
		}
		else
		{
			transform.localScale = new Vector3(1, 1, 1);
		}
	}
}
