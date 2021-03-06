﻿using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour {
	
	public float breakThreshhold = 150f;
	public float forceThreshhold = 100f;
	public AudioClip breakSound;
	public float shatterThreshhold;
	public float healthTotal;
	WallShatterAnimationHandler wallscript;
	bool isDead;
	bool isWall = true;
	bool isCollisionDisabled=false;
	private AudioSource audioSource;
	// Use this for initialization
	void Start () {
		audioSource = gameObject.GetComponent<AudioSource>();
		this.GetComponent<Rigidbody2D>().isKinematic = true;
		shatterThreshhold = -breakThreshhold;
		healthTotal = -shatterThreshhold + breakThreshhold;
		wallscript=GetComponentInChildren<WallShatterAnimationHandler>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate()
	{
		if(!isCollisionDisabled && !isWall)
		{
			audioSource.PlayOneShot(breakSound);
			//this.GetComponent<Rigidbody2D>().GetComponent<Collider2D>().isTrigger=true;
			isCollisionDisabled=true;
		}
		if(isDead)
		{
			DestroyImmediate(gameObject);
		}
	
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag != "Platform")
		{
			if(col.gameObject.tag == "Player")
			{
				if(!col.gameObject.GetComponent<PlayerMovementScript>().isFlinching())
					return;
			}
			Vector2 force = Vector2.zero;
			if(col.rigidbody != null)
				force = col.rigidbody.mass * col.rigidbody.velocity / Time.fixedDeltaTime;
			if(force.magnitude > forceThreshhold)
			{
				breakThreshhold -= force.magnitude;
				int frame = (int)Mathf.Clamp(4-(int)Mathf.Ceil((breakThreshhold - shatterThreshhold)/(healthTotal/4)),0,3);
				wallscript.setDamage(frame);
				if (breakThreshhold < 0 && isWall ) {
					isWall = false;
					Rigidbody2D body = this.GetComponent<Rigidbody2D>();
					body.isKinematic = false;
					body.AddForce(new Vector2(-force.x,force.y));
					body.AddTorque(Random.Range(-25f,25f));
				}
				else if(breakThreshhold < shatterThreshhold)
				{
					isDead=true;
				}
				else
				{
					//col.collider.rigidbody2D.AddForce(-2*force);
				}
			}
		}
	}

	public void dramaticFall()
	{
		Invoke("DramaFall",Mathf.Abs(transform.position.magnitude)/30.0f);
	}
	public void DramaFall() {

		isWall=false;

		Rigidbody2D body = this.GetComponent<Rigidbody2D>();
		body.isKinematic = false;

		Vector2 force = new Vector2(Random.Range(-20000,20000),Random.Range(-40000,-80000));
		body.AddForce(new Vector2(-force.x,force.y));
		body.AddTorque(Random.Range(-25f,25f));


	}
}