using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipController : MonoBehaviour {

	public float maxSpeed = 5;

	public float throttle;

	public float turn;

	public float fire;

	//public Vector3 target;

	public Vector3 mousetarget;

	private Rigidbody shipBR;

	private List<TurretController> Turrets = new List<TurretController>();

	public void FireTurrets(){
		for (int i = 0; i < Turrets.Count; i++) {
			Turrets[i].Fire();
		}
	}

	public void BuildShip(){
		//TODO
	}

	public void AddSubObject (string path, Vector3 pos){
		//Adds an object found at 'path' at the position 'pos' relative to the ship

		GameObject ObjectToAdd;

		TurretController ballTC;

		ObjectToAdd = Instantiate (Resources.Load (path) as GameObject);

		ObjectToAdd.transform.parent = transform;

		ObjectToAdd.transform.position = transform.position + pos;

		ballTC = (TurretController) ObjectToAdd.GetComponent(typeof(TurretController));

		Turrets.Add(ballTC);
	}

	// Use this for initialization
	void Start () {


		shipBR = GetComponent<Rigidbody> ();
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Move ();
		Rotate ();
		UpdateTurrets ();

	}

	void Move(){
		//Moves the Ship

		shipBR.velocity = (transform.forward * throttle * maxSpeed);
	}

	void Rotate(){
		//Rotates the Ship
		transform.Rotate (new Vector3 (0, turn, 0));
	}

	void UpdateTurrets(){
		//Updates the target for all turrets

		for (int i = 0; i < Turrets.Count; i++) // Loop with for.
		{
			Turrets[i].Target = mousetarget;
		}
	}


	
}
