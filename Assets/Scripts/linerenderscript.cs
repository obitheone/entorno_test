using UnityEngine;
using System.Collections;

public class linerenderscript : MonoBehaviour {

	private LineRenderer lineRenderer;
	private float counter=0.0f;
	private float dist;

	public Transform origin;
	public Transform destination;

	public float lineDrawSpeed=10f;

	// Use this for initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.SetPosition (0,origin.position);
		lineRenderer.SetWidth (1.45f,1.45f);
		dist = Vector3.Distance (origin.position,destination.position);
	}
	
	// Update is called once per frame
	void Update () {
		if (counter < dist) {
			counter += .1f / lineDrawSpeed;
			float x = Mathf.Lerp (0, dist, counter);

			Vector3 pointA = origin.position;
			pointA.y = pointA.y - 1;
			Vector3 pointB = destination.position;

			Vector3 pointAlongline = x * Vector3.Normalize (pointB - pointA) + pointA;

			lineRenderer.SetPosition (1, pointAlongline);
		} 
		//else Destroy (lineRenderer);
	}
}
