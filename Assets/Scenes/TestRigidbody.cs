using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRigidbody : MonoBehaviour
{
	// Start is called before the first frame update
	public Rigidbody rd;
	public float _friction = 3;
	public Transform target;
	public float finalVelocity = 10;

	private void Awake()
	{
		_friction = _friction < 0 ? _friction : -1 * _friction;
	}

	void Start()
    {
		//rd.velocity = Vector3.right * 1f;

		float power = FindPower(transform.position, target.position, finalVelocity);
		KickToPoint(target.position, power);
	}

	private void FixedUpdate()
	{
		ApplyFriction();
	}
	
	// Update is called once per frame
	void Update()
    {
        
    }

	public void ApplyFriction()
	{
		//get the direction the ball is travelling
		var _frictionVector = rd.velocity.normalized;
		//_frictionVector.y = 0f;

		//calculate the actual friction
		_frictionVector *= _friction;

		////calculate the raycast start positiotn
		//_rayCastStartPosition = transform.position + SphereCollider.radius * Vector3.up;

		////check if the ball is touching with the pitch
		////if yes apply the ground friction force
		////else apply the air friction
		//_isGrounded = Physics.Raycast(_rayCastStartPosition,
		//	Vector3.down,
		//	out _hit,
		//	_rayCastDistance,
		//	_groundMask);

		////apply friction if grounded
		//if (_isGrounded)
			rd.AddForce(_frictionVector);

//#if UNITY_EDITOR
//		Debug.DrawRay(_rayCastStartPosition,
//			Vector3.down * _rayCastDistance,
//			Color.red);
//#endif

	}

	public float FindPower(Vector3 from, Vector3 to, float finalVelocity)
	{
		// v^2 = u^2 + 2as => u^2 = v^2 - 2as => u = root(v^2 - 2as)
		return Mathf.Sqrt(Mathf.Pow(finalVelocity, 2f) - (2 * _friction * Vector3.Distance(from, to)));
	}

	public void KickToPoint(Vector3 to, float power)
	{
		Vector3 direction = to - transform.position;
		direction.Normalize();

		//change the velocity
		direction.y = 0.015f;
		rd.velocity = direction * power;

		////invoke the ball launched event
		//BallLaunched temp = OnBallLaunched;
		//if (temp != null)
		//	temp.Invoke(0f, power, NormalizedPosition, to);
	}

	public void DisablePhysics()
	{
		rd.isKinematic = true;
		rd.useGravity = false;
	}

	public void EnablePhysics()
	{
		rd.isKinematic = false;
		rd.useGravity = true;
	}
}
