using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject PlayerGameObject;
	public GameObject TargetingCursor;

	public float cameraSensitivity = 90;

	public Vector3 cameraOffset;
	
	private float rotationX = 0.0f;
	private float rotationY = 0.0f;
	private Vector3 target;
	private Vector3 mousetarget;

	

	private ShipController ShipControls;
	private TargetingCursorController TargetControls;
	// Use this for initialization
	void Start () {

		SetUpControllable ();

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		ShipControls.throttle = Input.GetAxis ("Vertical");
		ShipControls.turn = Input.GetAxis ("Horizontal");

		if(Input.GetMouseButtonDown(0)){
			ShipControls.fire = Input.GetAxis("Fire1");
		}

		//GetCameraTarget ();
		GetMouseTarget();
		//ShipControls.target = target;
		ShipControls.mousetarget = mousetarget;
		TargetControls.mousetarget = mousetarget;


		UpdateCamera ();
	
	}

	void UpdateCamera() { //TODO we need to fix the way the camera targets



		rotationX += Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
		rotationY -= Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;
		rotationY = Mathf.Clamp (rotationY, -90, 90);

		//Fix the camer to being mostly behind the boat
		//FIXME this causes a weird snap when the player rotates through the disconuity
		float snapangle = 90;
		float minus=PlayerGameObject.transform.eulerAngles.y-snapangle;
		float plus=PlayerGameObject.transform.eulerAngles.y+snapangle;
		rotationX = Mathf.Clamp(rotationX,minus,plus);

		Vector3 rotatedOffset = Quaternion.Euler(0, rotationX, 0) * cameraOffset;

		Camera.main.transform.position = PlayerGameObject.transform.position + rotatedOffset;

		Camera.main.transform.LookAt (PlayerGameObject.transform.position);

		Camera.main.transform.Rotate (new Vector3 (rotationY, 0, 0));



	}

	void SetUpControllable () {

		PlayerGameObject = Instantiate (Resources.Load ("Prefab/Ship") as GameObject);

		ShipControls = (ShipController) PlayerGameObject.GetComponent(typeof(ShipController));

		TargetingCursor = Instantiate (Resources.Load("Prefab/Targeting Cursor") as GameObject);

		TargetControls = (TargetingCursorController) TargetingCursor.GetComponent(typeof(TargetingCursorController));

		ShipControls.AddSubObject ("Prefab/Turret",new Vector3 (0, 1, 1));

		ShipControls.AddSubObject ("Prefab/Turret",new Vector3 (0, 1, -1));

		ShipControls.AddSubObject ("Prefab/Turret",new Vector3 (0, 1, 2));
	}

	void GetMouseTarget(){

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit)){
			mousetarget=hit.point;
			mousetarget.y=1.001f;
		}
		//old & super broken
		//mousetarget = Input.mousePosition;
		//mousetarget.z =PlayerGameObject.transform.position.z-Camera.main.transform.position.z;
		//mousetarget = Camera.main.ScreenToWorldPoint(mousetarget);
		//mousetarget.y=1.01f;

	}
	/*
	void GetCameraTarget () {

		float h;
		if (Camera.main.transform.forward.y < 0) {
			h = Camera.main.transform.position.y / Camera.main.transform.forward.y;
			target = Camera.main.transform.position + -1 * h * Camera.main.transform.forward;
		} else {
			target = new Vector3 (0, 0, 0);
		}


	}*/

}
