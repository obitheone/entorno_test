using UnityEngine;
using System.Collections;

public class abiltycontroller : MonoBehaviour {

	private string _key_press;
	private GameObject _object;
	private GameObject _beamobject;
	private bool _beam=false;
	public float speed=0.02f;
	private float _lateral=0f;
	private float _horizontal=0f;
	private float _energy=50;
	// Use this for initialization
	void Start () {
	
	}
	void FixedUpdate()	
	{
		if (_beam) {

			if (Input.GetKey(KeyCode.Keypad6)) 
			{
				_lateral=_lateral+speed;
			}
			if (Input.GetKey(KeyCode.Keypad4)) 
			{
				_lateral=_lateral-speed;
			}
			if (Input.GetKey(KeyCode.Keypad8)) 
			{
				_horizontal=_horizontal+speed;
			}
			if (Input.GetKey(KeyCode.Keypad2)) 
			{
				_horizontal=_horizontal-speed;
			}

			Intertia script = _beamobject.GetComponent("Intertia") as Intertia;
			script.offset_lateral=_lateral;
			script.offset_horizontal=_horizontal;

			if (Input.GetMouseButton(0)) 
			{ 

				//if (_energy==50)
				//{
				//	_beamobject.AddComponent ("Smothmove");
				//}
				_energy=_energy+10;
				//Smothmove smoothscript = _beamobject.GetComponent("Smothmove") as Smothmove;
				//smoothscript.speed=(_energy - 60)/100;
				//_beamobject.light.intensity=_energy/800;
			}

			if (Input.GetMouseButtonUp (1)) 
			{ 
				GameObject Beta = GameObject.Find("Beta");
				_beamobject.rigidbody.AddForce(Beta.transform.forward * _energy);
				_energy=50;
				//Destroy(_beamobject.Smothmove);
				_key_press = "f";
			}
		}
	}
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown("b")) _key_press = "b";
		if (Input.GetKeyDown("f")) _key_press = "f";
	
		switch (_key_press)
		{
		case "b":
			if (Input.GetMouseButtonDown (0)) {
				Ray ray =Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if(Physics.Raycast(ray, out hit, 100))
				{
					_object = new GameObject("BlackHole");
					_object.AddComponent ("Blackhole");
					_object.transform.position = hit.point + new Vector3 (0,0.5f,0);

				}
				_key_press = "";
			}
			break;
		case "f":
			if (!_beam){
				if (Input.GetMouseButtonDown (0)) {
					Ray ray =Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit;
					if(Physics.Raycast(ray, out hit, 100,(1<<LayerMask.NameToLayer("Beamer"))))
					{
						if(hit.collider.gameObject.GetComponent<Intertia>() == null)
						{
							_beamobject=hit.collider.gameObject;
							hit.collider.gameObject.AddComponent ("Intertia");	
							GameObject hand = GameObject.Find("Beta:RightHand");
							LightningBolt script = hand.GetComponent("LightningBolt") as LightningBolt;
							script.target=hit.collider.gameObject.transform;
							_beam=true;
						}
					}
					_key_press = "";
					}
			}
			else
			{
				//retiramos la inercia y el rayo del objecto
				Destroy (_beamobject.GetComponent("Intertia"));
				GameObject hand = GameObject.Find("Beta:RightHand");
				LightningBolt script = hand.GetComponent("LightningBolt") as LightningBolt;
				script.target=null;
				_beam=false;
				_lateral=0;
				_horizontal=0;
				_key_press = "";
			}

			break;
		case "z":
			break;
		case "x":
			break;
		case "c":
			break;
		default:

			break;
		}	
	}
}
