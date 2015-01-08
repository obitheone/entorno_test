using UnityEngine;
using System.Collections;

public class Smothmove : MonoBehaviour {

	public float speed =1.0f;
	private Vector3 range=new Vector3 (1.0f, 1.0f, 1.0f); 
	private Perlin noise;
	private Vector3 position;

	// Use this for initialization
	void Start () {
		position = transform.position;
		noise = new Perlin ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = position + Vector3.Scale(SmoothRandom.GetVector3(speed), range);
	}
}