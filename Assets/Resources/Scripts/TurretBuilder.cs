using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

public class TurretBuilder : MonoBehaviour {

	private TurretController theTC;



	void Awake () {
		theTC = (TurretController) GetComponent(typeof(TurretController));
	}

	void AddBuilding (string path, Vector3 pos){
		//Adds an object found at 'path' at the position 'pos' relative to the ship
		
		GameObject newBarrel;
		
		newBarrel = Instantiate (Resources.Load (path) as GameObject);
		
		newBarrel.transform.parent = transform;
		newBarrel.transform.position = transform.position + pos;
	}

	void AddBarrel (string path, Vector3 pos){
		//Adds an object found at 'path' at the position 'pos' relative to the ship
		
		GameObject newBarrel;
		
		BarrelController newBarrelController;
		
		newBarrel = Instantiate (Resources.Load (path) as GameObject);
		newBarrelController = (BarrelController) newBarrel.GetComponent(typeof(BarrelController));
		
		newBarrel.transform.parent = transform;
		newBarrel.transform.position = transform.position + pos;
		
		theTC.BarrelControllers.Add(newBarrelController);
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
			
			XmlNodeList buildingInfo = building.ChildNodes;
			foreach (XmlNode buildingItens in buildingInfo) {
				
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
				
				
				
			}
			AddBuilding(path,new Vector3(xPos,yPos,zPos));
		}
		//Builds the barrels
		XmlNodeList barrelsList = xmlDoc.GetElementsByTagName("barrel");
		foreach (XmlNode barrel in barrelsList) {
			float xPos = 0.0f;
			float yPos = 0.0f;
			float zPos = 0.0f;
			string path = null;

			XmlNodeList barrelInfo = barrel.ChildNodes;
			foreach (XmlNode barrelItens in barrelInfo) {

				if(barrelItens.Name == "position"){

					switch(barrelItens.Attributes["name"].Value){
					case "x": xPos = float.Parse(barrelItens.InnerText); break;
					case "y": yPos = float.Parse(barrelItens.InnerText); break;
					case "z": zPos = float.Parse(barrelItens.InnerText); break;

					}
				}

				if(barrelItens.Name == "path"){
					path = barrelItens.InnerText;
				}
			}
			AddBarrel(path,new Vector3(xPos,yPos,zPos));
		}
	}
}
