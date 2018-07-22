using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	Rigidbody2D body;
	public float lifetime;
	public float stuntime;
	public float bulletSpeed;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();
		body.AddForce(transform.up * bulletSpeed);
		StartCoroutine(Fly());
	}
	
	IEnumerator Fly()
	{
		yield return new WaitForSeconds(lifetime);
		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		var potential_waiter = collision.gameObject.GetComponentInParent<Waiter>();
		if (potential_waiter == null) return;

		potential_waiter.BeginStun();
		Destroy(gameObject);
	}
}
