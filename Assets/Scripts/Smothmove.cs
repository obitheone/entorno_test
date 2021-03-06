﻿using UnityEngine;
using System.Collections;

public class Smothmove : MonoBehaviour {

	public float speed =0.0f;
	private Vector3 range=new Vector3 (0.15f, 0.15f, 0.15f); 
	private Perlin noise;
	private Vector3 position;

	// Use this for initialization
	void Start () {
		position = transform.position;
		noise = new Perlin ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		transform.position += Vector3.Scale(SmoothRandom.GetVector3(speed), range);
	}
}