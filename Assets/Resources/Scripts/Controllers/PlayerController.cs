using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Vector3 SpawnPos = new Vector3 (0, 0, 0);
	public GameObject PlayerGameObject;
	public GameObject TargetingCursor;
	public float cameraSensitivity = 90;
	public Vector3 cameraOffset;
	private float rotationX = 0.0f;
	private float rotationY = 0.0f;
	private Vector3 target;
	private Vector3 mousetarget;
	private ShipController ShipControls;
	private ShipBuilder playerShipBuilder;
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
			ShipControls.FireTurrets();
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
		PlayerGameObject = Instantiate (Resources.Load ("Prefabs/Defaults/Ship") as GameObject);

		ShipControls = (ShipController) PlayerGameObject.GetComponent(typeof(ShipController));
		playerShipBuilder = (ShipBuilder) PlayerGameObject.GetComponent(typeof(ShipBuilder));

		playerShipBuilder.Build ("Designs/Ships/Ship1");

		TargetingCursor = Instantiate (Resources.Load("Prefabs/Targeting Cursor") as GameObject);
		TargetControls = (TargetingCursorController) TargetingCursor.GetComponent(typeof(TargetingCursorController));

		PlayerGameObject.transform.position = SpawnPos;
	}

	void GetMouseTarget(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit)){
			mousetarget=hit.point;
			mousetarget.y=1.001f;
		}
	}


}
