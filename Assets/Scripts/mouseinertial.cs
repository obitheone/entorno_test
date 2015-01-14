using UnityEngine;
using UnityEditor;
using System.Collections;

public class mouseinertial : MonoBehaviour
{
	//
	public float fuerza=300;
	//

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
	private GameObject hand ;
	private LightningBolt script;
	private Transform target;
	private float step;
	private float speed =5.0f;
	void start(){


	}
	
	void FixedUpdate()	{
		Screen.showCursor = true;
		hand = GameObject.Find("Beta:RightHand");
		script = hand.GetComponent("LightningBolt") as LightningBolt;	
		if (script.target!=gameObject.transform.position) 
		{
			_pressmouse=false;
		}
		if (_pressmouse) {
						//aqui vamos a hacer un movimiento en el eje de las y para simular que esta flotando

						/*	if (_noise == null) _noise = new Perlin ();
			Vector3 _prevPosition = _curPosition;
			_curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
			_curPosition = Camera.main.ScreenToWorldPoint(_curScreenPoint) + _offset;
			_velocity = _curPosition - _prevPosition;

			_curPosition = _curPosition + Vector3.Scale(SmoothRandom.GetVector3(speed), range);
			transform.position = _curPosition;

			rigidbody.velocity = Vector3.zero;	
			//dirigir el haz de particulas al objeto.
			_temp=Camera.main.transform.position;
		*/
			GameObject Beta = GameObject.Find ("Beta");

			Vector3 localForward = Beta.transform.worldToLocalMatrix.MultiplyVector(Beta.transform.forward);

			step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (Beta.transform.position.x+Beta.transform.forward.x*2, 2.0f+Beta.transform.position.y ,Beta.transform.position.z+Beta.transform.forward.z*2), step);
				} 
			else {
				rigidbody.useGravity=true;
			}
		
	}
	
	void OnMouseOver () {

		if (Input.GetMouseButtonDown (1)) {
					if (_pressmouse) _pressmouse = false;
					rigidbody.AddForce(Camera.main.transform.forward * 500);
					hand = GameObject.Find("Beta:RightHand");
					script = hand.GetComponent("LightningBolt") as LightningBolt;
					//script.target=null;
					rigidbody.useGravity=true;
		}
		if (Input.GetMouseButtonDown (2)) {
					if (_pressmouse)_pressmouse = false;
					rigidbody.AddForce(-Camera.main.transform.forward * 500);
					hand = GameObject.Find("Beta:RightHand");
					script = hand.GetComponent("LightningBolt") as LightningBolt;	
					//script.target=null;
					rigidbody.useGravity=true;
		}
	}


	void OnMouseDown()
	{

		if (!_pressmouse) {
						_pressmouse = true;	
						rigidbody.useGravity = false;
						_screenPoint = Camera.main.WorldToScreenPoint (gameObject.transform.position);
						_offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));
						Screen.showCursor = false;
						rigidbody.velocity = Vector3.zero;
						//rigidbody.AddTorque (new Vector3 (10, 10, 0) * fuerza); //rotacion al cogerlo en vueloç
						//emitimos particulas
						hand = GameObject.Find("Beta:RightHand");
						script = hand.GetComponent("LightningBolt") as LightningBolt;
						script.target=gameObject.transform.position;
						//hand.particleEmitter.emit = true;
						////
				}
		else {
				rigidbody.AddForce(_velocity * fuerza); //fuerza de inercia.
				hand = GameObject.Find("Beta:RightHand");
				script = hand.GetComponent("LightningBolt") as LightningBolt;	
				//script.target=null;
				_pressmouse = false;
				rigidbody.useGravity=true;
			}

	}
}
