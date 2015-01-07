using UnityEngine;
using System.Collections;



public class blackhole : MonoBehaviour {
	
	public float radius = 10.0f;
	public float power = -100f; //implosion
	void Start () 
	{
		//GameObject gravityObject = GameObject.FindGameObjectWithTag("GravitySwell");
		//gravSlime = gravityObject.transform;      
	}
	
	void Update () 
	{
		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
		foreach (Collider hit in colliders)
		{
			if (( hit) &&( hit.rigidbody))
			{
				hit.rigidbody.AddExplosionForce(power, explosionPos, radius, 3);
			}
		}

	}
}


