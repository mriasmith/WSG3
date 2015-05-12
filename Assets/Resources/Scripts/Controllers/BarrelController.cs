using UnityEngine;
using System.Collections;

public class BarrelController : MonoBehaviour {

	public bool isDebug = false;
	public float ShellSpeed = 12.0f;
	public GameObject ShellObject = null;
	public float RotationSpeed = 1.0f;
	public float Theta = -45;
	public float offset;
	public float recoil = 0.3f;
	public float reload;
	private float recoilDisplacement;
	private GameObject BarrelObject;
	private float recoilAmount;
	private float recoilRecoveryRate;
	private float NextFire;


	public void Fire (){

		GameObject newBullet;
		Rigidbody newBulletRB;
		
		newBullet = Instantiate (Resources.Load ("Prefabs/Components/Turrets/Projectiles/Bullet") as GameObject);
		newBulletRB = newBullet.GetComponent<Rigidbody> ();
		
		newBullet.transform.position = transform.position + transform.forward;
		newBulletRB.velocity = ShellSpeed * transform.forward;

		BarrelObject.transform.position = transform.position + transform.forward * offset;
		NextFire = Time.time + reload/2;


	}

	//void Recoil

	void RotateTowardsTheta () {

		float angle = transform.localEulerAngles.x;
		angle = (angle > 180) ? angle - 360 : angle;
		if (isDebug) {

			print ((angle-Theta) > RotationSpeed);
			print (1);
		}



		if (angle < Theta) {
			if (RotationSpeed > Theta - angle) {
				transform.Rotate(new Vector3(angle - Theta,0,0));
			} else {
				transform.Rotate(new Vector3(RotationSpeed,0,0));
			}
		} else if (angle > Theta) {
			if (RotationSpeed > angle - Theta) {
				transform.Rotate(new Vector3(Theta - angle,0,0));
			} else {
				transform.Rotate(new Vector3(-1.0f * RotationSpeed,0,0));
			}
		}
		
	}

	void UpdateRecoil (){
		if (Time.time < NextFire) {
			recoilAmount = (NextFire - Time.time) * recoil;
		} else {
			recoilAmount = 0;
		}
		BarrelObject.transform.position = transform.position + transform.forward * (offset - recoilAmount);

	}

	// Use this for initialization
	void Start () {
		BarrelObject = this.gameObject.transform.GetChild (0).gameObject;
		print (BarrelObject.transform.position - transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		UpdateRecoil ();
		//Debug.DrawRay (transform.position, transform.forward * 10);
		RotateTowardsTheta ();


	
	}
}
