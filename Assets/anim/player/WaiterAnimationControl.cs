using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaiterAnimationControl : MonoBehaviour {

	public Waiter waiter;
	public Animator animator;
	public Rigidbody2D body;

	// Use this for initialization
	void Start () {
		waiter = GetComponent<Waiter>();
		animator = GetComponent<Animator>();
		body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		animator.SetFloat("speed", body.velocity.magnitude);
	}
}
