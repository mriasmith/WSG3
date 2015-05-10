using UnityEngine;
using System.Collections;

public class BarrelController : MonoBehaviour {

	float ShellSpeed = 12.0f;

	public GameObject ShellObject = null;

	public float RotationSpeed = 1.0f;

	public float Thetaf;

	public float Theta = -45;

	public void Fire (){

		GameObject newBullet;
		Rigidbody newBulletRB;
		
		newBullet = Instantiate (Resources.Load ("Prefab/Bullet") as GameObject);
		newBulletRB = newBullet.GetComponent<Rigidbody> ();
		
		newBullet.transform.position = transform.position + transform.forward * 2.0f;
		newBulletRB.velocity = ShellSpeed * transform.forward;

	}

	void RotateTowardsTheta () {

		float angle = transform.localEulerAngles.x;
		angle = (angle > 180) ? angle - 360 : angle;

		print (angle);
		print (Theta);
		print (angle < Theta);

		if (angle < Theta) {
			print ("1");
			if (RotationSpeed > Theta - angle) {
				transform.Rotate(new Vector3(Theta - angle,0,0));
			} else {
				transform.Rotate(new Vector3(RotationSpeed,0,0));
			}
		}
		if (angle > Theta) {
			print ("2");
			if (RotationSpeed > angle - Theta) {
				transform.Rotate(new Vector3(angle - Theta,0,0));
			} else {
				transform.Rotate(new Vector3(-1.0f * RotationSpeed,0,0));
			}
		}
		
	}

	// Use this for initialization
	void Start () {

		transform.Rotate(new Vector3(-1,0,0));
	}
	
	// Update is called once per frame
	void Update () {

		Debug.DrawRay (transform.position, transform.forward * 10);
		RotateTowardsTheta ();


	
	}
}
