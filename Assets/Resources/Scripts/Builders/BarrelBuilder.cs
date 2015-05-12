using UnityEngine;
using System.Collections;

public class BarrelBuilder : MonoBehaviour {

	public void Build (string path,float offset){
		//Adds an object found at 'path' at the position 'pos' relative to the ship
		GameObject newBarrel;
		
		newBarrel = Instantiate (Resources.Load (path) as GameObject);
		
		newBarrel.transform.parent = transform;
		newBarrel.transform.position = transform.position + new Vector3 (0,0,offset);
	}
}
