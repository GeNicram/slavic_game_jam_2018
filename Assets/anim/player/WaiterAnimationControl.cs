using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaiterAnimationControl : MonoBehaviour {

	protected Waiter waiter;
	protected Animator animator;
	protected Rigidbody2D body;

	protected bool isGoingRight = false;

	// Use this for initialization
	void Start () {
		waiter = GetComponent<Waiter>();
		animator = GetComponent<Animator>();
		body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		animator.SetFloat("speed", body.velocity.magnitude);

		bool shouldUpdateDirection = false;
		if (body.velocity.x > 0.5 && !isGoingRight)
		{
			shouldUpdateDirection = true;
			isGoingRight = true;
		}
		else if (body.velocity.x < -0.5 && isGoingRight)
		{
			shouldUpdateDirection = true;
			isGoingRight = false;
		}
		animator.SetBool("shouldUpdateRotation", shouldUpdateDirection);
		if(shouldUpdateDirection)
		{
			StartCoroutine(StopUpdatingRotation());
		}
		animator.SetBool("isRight", isGoingRight);
	}

	IEnumerator StopUpdatingRotation()
	{
		yield return new WaitForEndOfFrame();
		animator.SetBool("shouldUpdateRotation", false);
	}
}
