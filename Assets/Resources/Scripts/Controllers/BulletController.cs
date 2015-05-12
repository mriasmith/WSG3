using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	// Use this for initialization
	void Start () {


	}

	void OnCollisionEnter(Collision collision) {
		foreach (ContactPoint contact in collision.contacts) {
			Debug.DrawRay (contact.point, contact.normal*20, Color.white);
		}
		print ("SQUWEEEEEE");
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < 0) {
			Object.Destroy(this.gameObject);
		}
	
	}
}
