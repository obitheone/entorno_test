using UnityEngine;
using UnityEditor;
using System.Collections;

public class mouseinertial : MonoBehaviour
{
	//
	public float fuerza=300;
	//
	private GameObject[] _clonelinerender;
	private Vector3 _screenPoint;
	private Vector3 _offset;
	private Vector3 _curScreenPoint;
	private Vector3 _curPosition;
	private Vector3 _velocity;
	private bool _pressmouse;
	private float _time = 0.0f;
	private Vector3 _direccion,_temp;
	private float _desviationy=0.0f;
	private bool bajando=false;
	private int renderline;
	private GameObject hand;


	void Start()
	{
		_clonelinerender = new GameObject[20];
		renderline=0;
	}

	void FixedUpdate()	{
		if (_pressmouse) {
			//aqui vamos a hacer un movimiento en el eje de las y para simular que esta flotando

			if (bajando)_desviationy=_desviationy-0.01f;
			else _desviationy=_desviationy+0.01f;

			//if (_desviationy>0.25f) {bajando=true;}
			//if (_desviationy<=-0.25f) {bajando=false;}

			if (_desviationy>0.3f) {bajando=true;}
			if (_desviationy<=-0.3f) {bajando=false;}
			/////
			Vector3 _prevPosition = _curPosition;
			_curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
			_curPosition = Camera.main.ScreenToWorldPoint(_curScreenPoint) + _offset;
			_velocity = _curPosition - _prevPosition;
			_curPosition.y=_curPosition.y+_desviationy;
			transform.position = _curPosition;
			rigidbody.velocity = Vector3.zero;	
			//dirigir el haz de particulas al objeto.
			_temp=Camera.main.transform.position;
			_direccion=_curPosition-_temp;

			//mientras este pulsado creamos diferentes rayos
			Destroy(_clonelinerender[renderline]); //destruimos el rayo anterior
			/// creamos el nuevo rayo.
			Object linerender = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/linerender.prefab", typeof(GameObject));
			_clonelinerender[renderline] = (GameObject)Instantiate(linerender);		
			linerenderscript script = _clonelinerender[renderline].GetComponent("linerenderscript") as linerenderscript;

			hand = GameObject.Find("Beta:RightHand");
			script.origin=hand.transform;
			
			script.destination=gameObject.transform;

			if (renderline<_clonelinerender.Length-1) renderline++;
			else renderline=0;
		}

	}

	void OnMouseOver () {

		if (Input.GetMouseButtonDown (1)) {
					if (_pressmouse) _pressmouse = false;
					rigidbody.AddForce(Camera.main.transform.forward * 500);
					deleterays();
				}
		if (Input.GetMouseButtonDown (2)) {
					if (_pressmouse)_pressmouse = false;
					rigidbody.AddForce(-Camera.main.transform.forward * 500);
					deleterays();
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
						rigidbody.AddTorque (new Vector3 (10, 10, 0) * fuerza); //rotacion al cogerlo en vueloç
					
				}
		else {
				Screen.showCursor = true;
				rigidbody.AddForce(_velocity * fuerza); //fuerza de inercia.
				_pressmouse = false;
				deleterays();
			}

	}
	void deleterays()
	{
		//apagar las particulas
		int i=0;
		while (i < _clonelinerender.Length) 
		{
			Destroy(_clonelinerender[i]);
			i++;
		}
	}
}
