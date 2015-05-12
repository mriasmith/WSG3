using UnityEngine;
using System.Collections;

public class BarrelController : MonoBehaviour {

	public float ShellSpeed = 12.0f;

	public GameObject ShellObject = null;

	public float RotationSpeed = 1.0f;

	public float Thetaf;

	public float Theta = -45;

	public GameObject SubBarrelGO;

	private float recoilDisplacement;

	private GameObject Cylinder;

	public void Fire (){

		GameObject newBullet;
		Rigidbody newBulletRB;
		
		newBullet = Instantiate (Resources.Load ("Prefabs/Components/Turrets/Projectiles/Bullet") as GameObject);
		newBulletRB = newBullet.GetComponent<Rigidbody> ();
		
		newBullet.transform.position = transform.position + transform.forward;
		newBulletRB.velocity = ShellSpeed * transform.forward;

	}

	void RotateTowardsTheta () {

		float angle = transform.localEulerAngles.x;
		angle = (angle > 180) ? angle - 360 : angle;

		if (angle < Theta) {
			if (RotationSpeed > Theta - angle) {
				transform.Rotate(new Vector3(Theta - angle,0,0));
			} else {
				transform.Rotate(new Vector3(RotationSpeed,0,0));
			}
		}
		if (angle > Theta) {
			if (RotationSpeed > angle - Theta) {
				transform.Rotate(new Vector3(angle - Theta,0,0));
			} else {
				transform.Rotate(new Vector3(-1.0f * RotationSpeed,0,0));
			}
		}
		
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		//Debug.DrawRay (transform.position, transform.forward * 10);
		RotateTowardsTheta ();


	
	}
}
