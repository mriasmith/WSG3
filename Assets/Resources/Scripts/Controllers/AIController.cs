using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {

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

	void SetUpControllable () {
		PlayerGameObject = Instantiate (Resources.Load ("Prefabs/Defaults/Ship") as GameObject);
		
		ShipControls = (ShipController) PlayerGameObject.GetComponent(typeof(ShipController));
		playerShipBuilder = (ShipBuilder) PlayerGameObject.GetComponent(typeof(ShipBuilder));
		
		playerShipBuilder.Build ("Designs/Ships/SmallShip");
		
		TargetingCursor = Instantiate (Resources.Load("Prefabs/Targeting Cursor") as GameObject);
		TargetControls = (TargetingCursorController) TargetingCursor.GetComponent(typeof(TargetingCursorController));
		PlayerGameObject.transform.position = SpawnPos;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		ShipControls.throttle = 1;
		ShipControls.turn = 1;
		
	}
}
