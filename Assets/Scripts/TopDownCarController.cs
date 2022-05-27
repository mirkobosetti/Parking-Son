using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCarController : MonoBehaviour
{
	[Header("Car Settings")]

	public float driftFactor = 0.05f;
	public float accelerationFactor = 10f;
	public float turnFactor = 6f;
	public float maxSpeed = 5;
	public float tireFriction = 10f;

	float accelerationInput = 0.0f;
	float steeringInput = 0.0f;
	float rotationAngle = 0.0f;
	float velocityVsUp = 0;

	Rigidbody2D rb;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Start()
	{

	}

	void Update() 
	{
		Vector2 inputVector = Vector2.zero;

		inputVector.x = Input.GetAxis("Horizontal");
		inputVector.y = Input.GetAxis("Vertical");

		SetInputVector(inputVector);
	}

	void FixedUpdate()
	{
		velocityVsUp = Vector2.Dot(transform.up, rb.velocity);
		ApplyEngineForce(velocityVsUp);
		KillOrthogonalVelocity();
		ApplySteering(velocityVsUp);
	}

	void ApplyEngineForce(float velocityVsUp)
	{
		if (velocityVsUp > maxSpeed && accelerationInput > 0)
			return;

		if (velocityVsUp < -maxSpeed * 0.3f && accelerationInput < 0)
			return;

		if (rb.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
			return;

		if (accelerationInput == 0)
			rb.drag = Mathf.Lerp(rb.drag, tireFriction, Time.fixedDeltaTime * 3);
		else
			rb.drag = 0;

		Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;
		rb.AddForce(engineForceVector, ForceMode2D.Force);
	}

	void ApplySteering(float velocityVsUp)
	{
		float minSpeedBeforeAllowTurningFactor = (rb.velocity.magnitude / 8);
		minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

		rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor * (Mathf.Clamp01(velocityVsUp) > 0 ? 1 : -1);
		rb.MoveRotation(rotationAngle);
	}

	public void SetInputVector(Vector2 inputVector)
	{
		steeringInput = inputVector.x;
		accelerationInput = inputVector.y;
	}

	void KillOrthogonalVelocity()
	{
		Vector2 forwardVelocity = transform.up * Vector2.Dot(rb.velocity, transform.up);
		Vector2 rightVelocity = transform.right * Vector2.Dot(rb.velocity, transform.right);

		rb.velocity = forwardVelocity + rightVelocity * driftFactor;
	}
}
