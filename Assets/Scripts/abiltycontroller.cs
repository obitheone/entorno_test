using UnityEngine;
using System.Collections;

public class abiltycontroller : MonoBehaviour {

	private string _key_press;
	private GameObject _object;
	private GameObject _beamobject;
	private GameObject _tractorobject;
	private Vector3 _hitpoint;
	private bool _beam=false;
	private bool _tractor=false;
	public float speed=0.02f;
	private float _lateral=0f;
	private float _horizontal=0f;
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

			//actualizamos la posicion del rayo
			GameObject hand = GameObject.Find("Beta:RightHand");
			LightningBolt script2 = hand.GetComponent("LightningBolt") as LightningBolt;
			script2.target=_beamobject.transform.position;
			//fin actualzacion posicion del rayo

			if (Input.GetMouseButton(0)) 
			{ 
				script.energy=script.energy+1000*Time.deltaTime;
			}

			if (Input.GetMouseButtonUp (1)) 
			{ 
				GameObject Beta = GameObject.Find("Beta");
				_beamobject.rigidbody.AddForce(Beta.transform.forward * script.energy);
				script.energy=50; //esto no es necesario pero bueno.
				_key_press = "f";
			}
		}
	}
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown("b")) _key_press = "b";
		if (Input.GetKeyDown("f")) _key_press = "f";
		if (Input.GetKeyDown("g")) _key_press = "g";
	
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
					if(Physics.Raycast(ray, out hit, 100))//,(1<<LayerMask.NameToLayer("Beamer"))))
					{
						if (hit.collider.gameObject.tag == "Beamer"){
							if(hit.collider.gameObject.GetComponent<Intertia>() == null)
							{
								_beamobject=hit.collider.gameObject;
								hit.collider.gameObject.AddComponent ("Intertia");	
								GameObject hand = GameObject.Find("Beta:RightHand");
								LightningBolt script = hand.GetComponent("LightningBolt") as LightningBolt;
								script.target=hit.collider.gameObject.transform.position;
								script.active=true;
								_beam=true;
							}
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
				script.active=false;
				_beam=false;
				_lateral=0;
				_horizontal=0;
				_key_press = "";
			}
			break;
		case "g":
			if (!_tractor){
				if (Input.GetMouseButtonDown (0)) {
					Ray ray =Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit;
					if(Physics.Raycast(ray, out hit, 100))
					{
						if (hit.collider.gameObject.tag == "Tractor"){
							if(hit.collider.gameObject.GetComponent<InverseInertia>() == null)
							{

								GameObject Beta = GameObject.Find("Beta");
								Beta.AddComponent("InverseInertia");
								InverseInertia script = Beta.GetComponent("InverseInertia") as InverseInertia;
								script.hitpoint=hit.point;
								_hitpoint=hit.point;
								_tractorobject=hit.collider.gameObject;
								_tractor=true;
								///
								GameObject hand = GameObject.Find("Beta:LeftHand");
								LightningBolt script2 = hand.GetComponent("LightningBolt") as LightningBolt;
								//Transform test=c
								//test.position=hit.point;
								script2.target=hit.point;
								script2.active=true;
							
							}
						}
					}
					_key_press = "";
				}
			}
			else
			{
				_tractor=false;
				GameObject Beta = GameObject.Find("Beta");
				InverseInertia script = Beta.GetComponent("InverseInertia") as InverseInertia;
				Destroy(script);
				GameObject hand = GameObject.Find("Beta:LeftHand");
				LightningBolt script2 = hand.GetComponent("LightningBolt") as LightningBolt;
				script2.active=false;
			}
		break;
		default:

			break;
		}	
	}

	void OnGUI()
	{
		if (_beam) {
						
			Intertia script = _beamobject.GetComponent("Intertia") as Intertia;
			string temp=script.energy.ToString();
			GUI.Label (new Rect (10, 10, 150, 20), "Push Force: "+temp);
				}
	}
}
