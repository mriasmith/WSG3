using UnityEngine;
using System.Collections;

public class TurretController : MonoBehaviour {

	public Vector3 Target;

	public float ShellSpeed; // changing shell speed seemed to have no effect I have no idea how it'
							   // its behaviour is controlled so I just replaced it with a new float
	public float fixedShellSpeed; // now the behaviour has moved to here I am at a total loss and at the end for tonight

	public float RotationSpeed;

	private float MaxRange;

	private float MinRange=0;


	public void Fire(){

		GameObject Bullet;

		Bullet = Instantiate (Resources.Load ("Prefab/Bullet") as GameObject);

		Bullet.transform.position = transform.position + transform.forward*1.7f;
		
		Rigidbody BulletRB;

		BulletRB = Bullet.GetComponent<Rigidbody> ();

		BulletRB.velocity = fixedShellSpeed*transform.forward;

		Debug.Log(360 - transform.eulerAngles.x);

	}
	void Awake () {
		// I am not sure if I trust this math here at all I think it is BullShit
		//MaxRange = Mathf.Cos ( Mathf.Deg2Rad * 45 ) * fixedShellSpeed; // 1/sqrt(2)??? vs all that trig?
		fixedShellSpeed = 15;
		MaxRange = -1f*fixedShellSpeed*fixedShellSpeed/Physics.gravity.y;
		//Debug.Log(fixedShellSpeed);
	}
	// Use this for initialization
	void Start () {	
	}
	
	// Update is called once per frame
	void Update () {
		RotateTowardsTarget ();

		//if (Input.GetMouseButtonDown(0)) {
		//	Fire ();
		//}

	
	}

	void RotateTowardsTarget () {//TODO make this point towards the mouse cursor instead of what ever it is doing now;
								 //TODO I REALLY FUCKED THIS UP
		Vector3 RelativePos = Target - gameObject.transform.position - gameObject.transform.forward*1.7f;

		//rotate towards target
		float temp47;
		temp47 = Quaternion.LookRotation(RelativePos).eulerAngles.y-transform.eulerAngles.y;
		transform.Rotate(new Vector3(0, temp47,0));

		float distance;

		float height = Target.y -(gameObject.transform.position+gameObject.transform.forward*1.7f).y;

		if (RelativePos.magnitude < MaxRange && RelativePos.magnitude > MinRange) {
			distance = RelativePos.magnitude;
		} 

		else if(RelativePos.magnitude > MinRange ) {
			distance = MaxRange;
		}

		else{

			distance=MinRange;

		}


		 
		float v=fixedShellSpeed;
		float y= height;
		float x= distance;
		float g = -Physics.gravity.y;

		float arcUp = Mathf.Atan((v*v -Mathf.Sqrt(v*v*v*v -1*g*(g*x+2*y*v*v)))/(g*x));
		arcUp = Mathf.PI/4;
		transform.Rotate(new Vector3(Mathf.Rad2Deg *-1*arcUp ,0,0));

	}



}

