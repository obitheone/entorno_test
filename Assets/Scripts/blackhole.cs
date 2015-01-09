using UnityEngine;
using System.Collections;



public class Blackhole : MonoBehaviour {
	
	public float time = 5.0f;
	public float radius = 10.0f;
	public float power = -100f; //implosion

	void Start () 
	{
	}
	
	void Update () 
	{
		time -= Time.deltaTime; 

		if (time > 0) {
			Vector3 explosionPos = transform.position;
			Collider[] colliders = Physics.OverlapSphere (explosionPos, radius);
			foreach (Collider hit in colliders) {
					if ((hit) && (hit.rigidbody)) {
							hit.rigidbody.AddExplosionForce (power, explosionPos, radius, 3);
					}
			}
		} 
		else {
			Destroy (gameObject);
				}
		
	}
}


