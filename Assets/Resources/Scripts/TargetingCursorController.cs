using UnityEngine;
using System.Collections;


//TODO Set cursor colour depending on validity of target
//TODO this should be only one of many methods of targeting 
public class TargetingCursorController : MonoBehaviour {

	public Vector3 mousetarget;

	private Transform Cursor_Position;

	// Use this for initialization
	void Start () {
		Cursor_Position = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		set_position();

	}


	void set_position(){

		Cursor_Position.position=mousetarget;

	}
}
