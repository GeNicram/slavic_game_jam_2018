﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour {

	public Animation anim;
	public int numberOfShots;
	public float gapBetweenShots;
	public float shootingAngleFromForward;
	public Transform hand;
	public Transform firePoint;
	public SpriteRenderer handRenderer;

	protected float starting_angle;
    public AudioClip shootSFX;
    AudioSource audioSC;

    public GameObject bulletPrefab;
    public Animator animatorrr;
    public float animSpeed;

	void Start()
	{
		handRenderer.enabled = false;
        audioSC = GetComponent<AudioSource>();
        if (animatorrr != null)
        {
            animatorrr.speed = animSpeed;
        }
		starting_angle = hand.rotation.eulerAngles.z;
	}

	public void ExpressDissapointment()
	{
		handRenderer.enabled = true;
		StartCoroutine(ExpressingDissapointmentCoroutine());
	}

	IEnumerator ExpressingDissapointmentCoroutine()
	{
		for(int i = 0; i < numberOfShots; i++)
		{
			var random_rot = Quaternion.AngleAxis(Random.Range(starting_angle - shootingAngleFromForward, starting_angle + shootingAngleFromForward), new Vector3(0, 0, -1));
			hand.localRotation = random_rot;
			//anim.Play();
			Fire();
			yield return new WaitForSeconds(gapBetweenShots);
		}
		handRenderer.enabled = false;
	}

	void Fire()
	{
		var bullet = Instantiate(bulletPrefab);
		var hand_euler = hand.rotation.eulerAngles;
		var bullet_direction = new Vector3(hand_euler.x, hand_euler.y, hand_euler.z + 90);
        audioSC.PlayOneShot(shootSFX);
		bullet.transform.rotation = Quaternion.Euler(bullet_direction);
		bullet.transform.position = firePoint.position;
	}

}
