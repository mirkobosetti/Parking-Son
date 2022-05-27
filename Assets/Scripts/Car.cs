using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
	public float movmentSpeed = 10.0f;
	public float steeringAngle = 250.0f;
	public float maxSpeed = 10.0f;
	public float acceleration = 0.1f;

	// actual speed at the moment
	public float speed = 0;

	void Start()
	{
		// random position
		transform.position = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));

		//random color
		GetComponent<SpriteRenderer>().color = new Color(Random.Range(0, 255f), Random.Range(0, 255f), Random.Range(0, 255f));

		//random z axes rotation
		transform.Rotate(0, 0, Random.Range(0, 360));
	}

	// Update is called once per frame
	void Update()
	{
		var forward = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow));
		var back = (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow));
		var left = (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow));
		var right = (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow));

		Movment(forward, back, left, right);
	}

	void Movment(bool forward, bool back, bool left, bool right)
	{
		if (forward)
		{
			//transform.Translate(Vector2.up * movmentSpeed * Time.deltaTime);
			this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * acceleration);
		}

		if (back)
		{
			//transform.Translate(Vector2.down * movmentSpeed * Time.deltaTime);
			this.GetComponent<Rigidbody2D>().AddForce(Vector2.down * acceleration);
		}

		if (forward || back)
		{
			if (left)
			{
				transform.Rotate(0, 0, steeringAngle * Time.deltaTime * (back ? -1 : 1));
			}

			if (right)
			{
				transform.Rotate(0, 0, -steeringAngle * Time.deltaTime * (back ? -1 : 1));
			}
		}
	}
}

