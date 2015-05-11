using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretController : MonoBehaviour {

	public Vector3 Target;

	float ShellSpeed = 10; 

	float RotationSpeed = 1.0f;

	public float ReloadSpeed = 0.5f;

	private float MaxRange;

	private float NextFire = 0;

	public List<BarrelController> BarrelControllers = new List<BarrelController>();
	
	public void Fire(){
		if (CanFire ()) {
			for (int i = 0; i < BarrelControllers.Count; i++) {
				BarrelControllers[i].Fire();
			}
		}
	}

	void Awake () {

		// I am not sure if I trust this math here at all I think it is BullShit
		//MaxRange = Mathf.Cos ( Mathf.Deg2Rad * 45 ) * ShellSpeed; // 1/sqrt(2)??? vs all that trig?

		ShellSpeed = 15;
		MaxRange = -1f*ShellSpeed*ShellSpeed/Physics.gravity.y;
	}
	// Use this for initialization
	void Start () {
		//AddBarrel("Prefab/Barrel",new Vector3 (0, 0, 0));
		//AddBarrel("Prefab/Barrel",new Vector3 (0.3f, 0, 0));
	}
	
	// Update is called once per frame
	void Update () {
		RotateTowardsTarget ();

		SetBarrelAngle (AngleToTarget ());

	}

	void SetBarrelAngle(float angle){
		for (int i = 0; i < BarrelControllers.Count; i++) {
			BarrelControllers[i].Theta = angle;
		}
	}

	bool CanFire () {
		if (Time.time < NextFire) {
			return false;
		} else {
			return true;
		}
	}



	void RotateTowardsTarget () {

		float a = AngleToRotate ();
		Debug.DrawRay (transform.position, transform.forward * 5);
		Debug.DrawRay (transform.position, transform.up * 5);

		Vector3 b = Quaternion.Euler (0, a, 0) * transform.forward;

		Debug.DrawRay (transform.position, b * 5);



		if (Mathf.Abs (a) > RotationSpeed) {

			transform.Rotate (new Vector3 (0, RotationSpeed * Mathf.Sign (a), 0));

		} else {

			transform.Rotate (new Vector3 (0, a, 0));
		}

	}


	float AngleToRotate(){
		// Sets up the two vectors
		Vector3 v1 = transform.forward;
		Vector3 v2 = Target - transform.position;
		// Flattens the vectors
		v1.y = 0;
		v2.y = 0;
		// Normalizes the vectors
		Vector3.Normalize (v1);
		Vector3.Normalize (v2);
		// Returns the angle between the vectors
		return Vector3.Angle (v1,v2) * Mathf.Sign(v2.x * v1.z - v2.z * v1.x);
	}

	float AngleToTarget() {
		// Sets up the two vectors

		Vector3 v1 = Target - transform.position;
		float y = v1.y;
		//flattens the vector
		v1.y = 0;
		float x = v1.magnitude;
		float g = Physics.gravity.y *-1.0f;
		float v = ShellSpeed;

		float sqrtInner = (v * v * v * v) - g * (g * x * x + 2 * y * v * v);

		if (sqrtInner < 0) {
			return -45.0f;
		}

		float sqrtOuter = Mathf.Sqrt (sqrtInner);

		float arctInner = -1.0f * ((v * v) - sqrtOuter) ;

		//print (-1.0f * Mathf.Rad2Deg * Mathf.Atan2 (arctInner,g*x));

		return Mathf.Rad2Deg * Mathf.Atan2 (arctInner,g*x);

	}


}

