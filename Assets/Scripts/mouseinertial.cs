using UnityEngine;
using UnityEditor;
using System.Collections;

public class mouseinertial : MonoBehaviour
{
	//
	public float fuerza=300;
	//
	public float speed =1.0f;
	private Vector3 range=new Vector3 (0.3f, 0.3f, 0.3f); 
	private Vector3 _screenPoint;
	private Vector3 _offset;
	private Vector3 _curScreenPoint;
	private Vector3 _curPosition;
	private Vector3 _velocity;
	private bool _pressmouse;
	private float _time = 0.0f;
	private Vector3 _temp;
	Perlin _noise;
	private bool bajando=false;


	void FixedUpdate()	{
		if (_pressmouse) {
			//aqui vamos a hacer un movimiento en el eje de las y para simular que esta flotando

			if (_noise == null) _noise = new Perlin ();
			/////
			Vector3 _prevPosition = _curPosition;
			_curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
			_curPosition = Camera.main.ScreenToWorldPoint(_curScreenPoint) + _offset;
			_velocity = _curPosition - _prevPosition;

			_curPosition = _curPosition + Vector3.Scale(SmoothRandom.GetVector3(speed), range);
			transform.position = _curPosition;

			rigidbody.velocity = Vector3.zero;	
			//dirigir el haz de particulas al objeto.
			_temp=Camera.main.transform.position;
		}

	}

	void OnMouseOver () {

		if (Input.GetMouseButtonDown (1)) {
					if (_pressmouse) _pressmouse = false;
					rigidbody.AddForce(Camera.main.transform.forward * 500);
				}
		if (Input.GetMouseButtonDown (2)) {
					if (_pressmouse)_pressmouse = false;
					rigidbody.AddForce(-Camera.main.transform.forward * 500);
				}
	}


	void OnMouseDown()
	{

		if (!_pressmouse) {
						_pressmouse = true;			
						_screenPoint = Camera.main.WorldToScreenPoint (gameObject.transform.position);
						_offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));
						Screen.showCursor = false;
						rigidbody.velocity = Vector3.zero;
						//rigidbody.AddTorque (new Vector3 (10, 10, 0) * fuerza); //rotacion al cogerlo en vueloç
						//emitimos particulas
						
						GameObject hand = GameObject.Find("Beta:RightHand");
						LightningBolt script = hand.GetComponent("LightningBolt") as LightningBolt;
			            script.target=gameObject.transform;
						script = hand.GetComponent("LightningBolt") as LightningBolt;
						//hand.particleEmitter.emit = true;
						////
				}
		else {
				Screen.showCursor = true;
				rigidbody.AddForce(_velocity * fuerza); //fuerza de inercia.
				GameObject hand = GameObject.Find("Beta:RightHand");
				//hand.particleEmitter.emit = false;
				LightningBolt script = hand.GetComponent("LightningBolt") as LightningBolt;
				script.target=null;
				_pressmouse = false;
			}

	}
}
