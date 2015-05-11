using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

public class ShipBuilder : MonoBehaviour {

	private ShipController ShipC;

	void Awake () {
		ShipC = (ShipController) GetComponent(typeof(ShipController));
	}
	
	void AddBuilding (string path, Vector3 pos){
		//Adds an object found at 'path' at the position 'pos' relative to the ship
		
		GameObject newBuilding;
		
		newBuilding = Instantiate (Resources.Load (path) as GameObject);
		
		newBuilding.transform.parent = transform;
		newBuilding.transform.position = transform.position + pos;
	}
	
	void AddTurret (string path, Vector3 pos){
		//Adds an object found at 'path' at the position 'pos' relative to the ship
		
		GameObject newTurret;
		TurretController newTurretController;
		TurretBuilder newTurretBuilder;
		
		newTurret = Instantiate (Resources.Load ("Prefabs/Turret") as GameObject);
		newTurretController = (TurretController) newTurret.GetComponent(typeof(TurretController));
		newTurretBuilder = (TurretBuilder)newTurret.GetComponent (typeof(TurretBuilder));
		
		newTurret.transform.parent = transform;
		newTurret.transform.position = transform.position + pos;
		newTurretBuilder.Build (path);
		
		ShipC.TurretControllers.Add(newTurretController);
	}
	
	public void Build (string xmlPath) {
		TextAsset TurretXML= Resources.Load(xmlPath) as TextAsset;
		XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
		xmlDoc.LoadXml(TurretXML.text); // load the file.

		XmlNodeList buildingList = xmlDoc.GetElementsByTagName("building");
		//Builds the buildings
		foreach (XmlNode building in buildingList) {
			float xPos = 0.0f;
			float yPos = 0.0f;
			float zPos = 0.0f;
			string path = null;
			foreach (XmlNode buildingItens in building.ChildNodes) {
				if(buildingItens.Name == "position"){
					switch(buildingItens.Attributes["name"].Value){
					case "x": xPos = float.Parse(buildingItens.InnerText); break;
					case "y": yPos = float.Parse(buildingItens.InnerText); break;
					case "z": zPos = float.Parse(buildingItens.InnerText); break;
					}
				}
				if(buildingItens.Name == "path"){
					path = buildingItens.InnerText;
				}
				//AddBuilding(path,new Vector3(xPos,yPos,zPos));
			}
		}

		//Builds the turrets
		XmlNodeList turretsList = xmlDoc.GetElementsByTagName("turret");
		foreach (XmlNode turret in turretsList) {
			float xPos = 0.0f;
			float yPos = 0.0f;
			float zPos = 0.0f;
			string path = null;
			foreach (XmlNode turretItens in turret.ChildNodes) {
				if(turretItens.Name == "position"){
					switch(turretItens.Attributes["name"].Value){
					case "x": xPos = float.Parse(turretItens.InnerText); break;
					case "y": yPos = float.Parse(turretItens.InnerText); break;
					case "z": zPos = float.Parse(turretItens.InnerText); break;
					}
				}
				if(turretItens.Name == "path"){
					path = turretItens.InnerText;
				}
			}
			print ("BEEP");
			AddTurret(path,new Vector3(xPos,yPos,zPos));
		}
	}
}
