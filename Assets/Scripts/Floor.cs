using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
	public Camera camera;

	void Start()
	{

	}

	void Update()
	{
		float pos = (camera.nearClipPlane + 10.0f);

		transform.position = camera.transform.position + camera.transform.forward * pos;
		transform.LookAt(camera.transform);
		float h = (Mathf.Tan(camera.fieldOfView * Mathf.Deg2Rad * 0.5f) * pos * 2f) * camera.aspect / 10.0f;
		float w = h * Screen.height / Screen.width;
		transform.localScale = new Vector3(h, w, 1);
	}
}
