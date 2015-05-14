using UnityEngine;
using System.Collections;

public class PlayerEditorController : MonoBehaviour {
	private Vector3 mousetarget;
	private GameObject selected;

	// Use this for initialization
	void Start () {
		selected=Instantiate (Resources.Load ("Prefabs/Components/Turrets/TurretBuildings/LargeTurretBuilding") as GameObject);
		selected.transform.position= new Vector3 (0,1.5f,0);
		Debug.Log(selected.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		GetMouseTarget();
		MeshFilter selectedmesh = (MeshFilter) selected.GetComponent<MeshFilter>();

		selected.transform.position=mousetarget + new Vector3 (0,selectedmesh.mesh.bounds.extents.y,0);

		if(Input.GetMouseButtonDown(0)){
			Placepart();
		}

	
	}

	void GetMouseTarget(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit)){
			mousetarget=hit.point;
		}

	}

	void Placepart(){



		//This is obviously temporary eventually it will branch based on if the selected component collides(this will also determine the colour of the mesh
		bool flagged=true;
		if(flagged){
			selected.AddComponent<BoxCollider>();
			selected.GetComponent<BoxCollider>().isTrigger =true;

			GameObject newselected = Instantiate (Resources.Load ("Prefabs/Components/Turrets/TurretBuildings/LargeTurretBuilding") as GameObject);
			selected =newselected;
		}
	}

}
