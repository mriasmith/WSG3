using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipController : MonoBehaviour {

	public float maxSpeed = 5;

	public float throttle;

	public float turn;

	public float fire;

	public Vector3 mousetarget;

	private Rigidbody shipBR;

	public List<TurretController>TurretControllers = new List<TurretController>();

	public void FireTurrets(){
		for (int i = 0; i < TurretControllers.Count; i++) {
			TurretControllers[i].Fire();
		}
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
		//Updates the target for allTurretControllers

		for (int i = 0; i <TurretControllers.Count; i++) // Loop with for.
		{
			TurretControllers[i].Target = mousetarget;
		}
	}


	
}
