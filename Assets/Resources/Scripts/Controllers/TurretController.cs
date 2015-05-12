using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretController : MonoBehaviour {

	public Vector3 Target;
	public float ShellSpeed = 100; 
	public float RotationSpeed = 1.0f;
	public float ReloadSpeed = 3.0f;
	public float maxAngle = 100.0f;
	public float minAngle = -100.0f;
	private float NextFire = 0;
	private float totalRotation = 0;
	private bool targetIsNotInArc = true;
	public List<BarrelController> BarrelControllers = new List<BarrelController>();

	public void Fire(){
		if (CanFire ()) {
			for (int i = 0; i < BarrelControllers.Count; i++) {
				BarrelControllers[i].Fire();
			}
			NextFire = Time.time + ReloadSpeed;
		}
	}

	void Awake () {
	}
	
	void Start () {
		SetShellSpeed ();
	}
	
	// Update is called once per frame
	void Update () {
		float ang;
		ang = AngleToRotate ();
		if (totalRotation + ang < maxAngle && totalRotation + ang > minAngle) {
			targetIsNotInArc = false;
			SetBarrelAngle (AngleToTarget ());
			RotateTowardsAngle (ang);
		} else {
			targetIsNotInArc = true;
			SetBarrelAngle (0);
			RotateTowardsAngle (-1.0f*totalRotation);
		}
	}

	void SetBarrelAngle(float angle){
		for (int i = 0; i < BarrelControllers.Count; i++) {
			BarrelControllers[i].Theta = angle;
		}
	}

	void SetShellSpeed(){
		for (int i = 0; i < BarrelControllers.Count; i++) {
			print (ShellSpeed);
			BarrelControllers[i].ShellSpeed = ShellSpeed;
		}
	}

	bool CanFire () {
		if (Time.time < NextFire) {
			return false;
		} else if (targetIsNotInArc) {
			return false;
		} else {
			return true;
		}
	}



	void RotateTowardsAngle (float a) {

		Debug.DrawRay (transform.position, transform.forward * 5);
		Debug.DrawRay (transform.position, transform.up * 5);

		Vector3 b = Quaternion.Euler (0, a, 0) * transform.forward;

		Debug.DrawRay (transform.position, b * 5);
	
		if (Mathf.Abs (a) > RotationSpeed) {
			totalRotation += RotationSpeed * Mathf.Sign (a);

			transform.Rotate (new Vector3 (0, RotationSpeed * Mathf.Sign (a), 0));
		} else {
			totalRotation += a;

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
		Vector3 v1 = Target - transform.position + new Vector3(0,10,0);
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
		return Mathf.Rad2Deg * Mathf.Atan2 (arctInner,g*x);

	}


}

