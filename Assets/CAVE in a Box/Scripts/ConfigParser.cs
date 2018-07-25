using System.Collections;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;
using UnityEngine;

public class ConfigParser : MonoBehaviour {
	public string configFilePath, backupPath;
	public GameObject cameraDataObj;

	void Awake () {
		parseJSON(readJSONFile(configFilePath));	
	}
	
	private JSONNode readJSONFile(string path) {
		var sr = new StreamReader(Application.dataPath + "/" + configFilePath);
		var fileContents = sr.ReadToEnd();
		sr.Close();
		
		return JSON.Parse(fileContents);
	}

	public void newConfig(CameraData[] cd) {
		// Write New Config
		var sr = new StreamReader(Application.dataPath + "/" + configFilePath);
		var fileContents = sr.ReadToEnd();
		sr.Close();

		JSONNode data = JSON.Parse(fileContents);
		for (int i = 0; i < cd.Length; i++) {
			data ["screens"] [i] ["screen"] = cd [i].screen;
			data ["screens"] [i] ["display"] = cd [i].display.ToString ();

			JSONArray tlJArray = data["screens"][i]["viewport"]["tl"].AsArray;
			JSONArray trJArray = data["screens"][i]["viewport"]["tr"].AsArray;
			JSONArray blJArray = data["screens"][i]["viewport"]["bl"].AsArray;
			JSONArray brJArray = data["screens"][i]["viewport"]["br"].AsArray;

			tlJArray [0] = cd [i].viewport.tl.x;
			trJArray [0] = cd [i].viewport.tr.x;
			blJArray [0] = cd [i].viewport.bl.x;
			brJArray [0] = cd [i].viewport.br.x;

			tlJArray [1] = cd [i].viewport.tl.y;
			trJArray [1] = cd [i].viewport.tr.y;
			blJArray [1] = cd [i].viewport.bl.y;
			brJArray [1] = cd [i].viewport.br.y;

			data ["screens"] [i] ["viewport"] ["tl"] = tlJArray;
			data ["screens"] [i] ["viewport"] ["tr"] = trJArray;
			data ["screens"] [i] ["viewport"] ["bl"] = blJArray;
			data ["screens"] [i] ["viewport"] ["br"] = brJArray;

			data ["screens"] [i] ["viewport"] ["t"] = cd [i].viewport.t;
			data ["screens"] [i] ["viewport"] ["b"] = cd [i].viewport.b;
			data ["screens"] [i] ["viewport"] ["l"] = cd [i].viewport.l;
			data ["screens"] [i] ["viewport"] ["r"] = cd [i].viewport.r;
		}

		// Backup Config File
		if (File.Exists (Application.dataPath + "/" + backupPath))
			File.Delete (Application.dataPath + "/" + backupPath);
		File.Copy (Application.dataPath + "/" + configFilePath, Application.dataPath + "/" + backupPath);

		// Overwrite Data
		Debug.Log ("New JSON : \n" + data.ToString());
		File.WriteAllText(Application.dataPath + "/" + configFilePath, data.ToString());
	}

	private void parseJSON(JSONNode json) {
		for (var i = 0; i < json["screens"].Count; i++) {
			string cameraDataName = json["screens"][i]["screen"] + " CameraData";
			CameraData cameraData = spawnCameraData(cameraDataName);

			JSONArray tlJArray = json["screens"][i]["viewport"]["tl"].AsArray;
			JSONArray trJArray = json["screens"][i]["viewport"]["tr"].AsArray;
			JSONArray blJArray = json["screens"][i]["viewport"]["bl"].AsArray;
			JSONArray brJArray = json["screens"][i]["viewport"]["br"].AsArray;

			cameraData.viewport.tl = new Vector2(tlJArray[0], tlJArray[1]);
			cameraData.viewport.tr = new Vector2(trJArray[0], trJArray[1]);
			cameraData.viewport.bl = new Vector2(blJArray[0], blJArray[1]);
			cameraData.viewport.br = new Vector2(brJArray[0], brJArray[1]);

			cameraData.viewport.t = json["screens"][i]["viewport"]["t"];
			cameraData.viewport.b = json["screens"][i]["viewport"]["b"];
			cameraData.viewport.l = json["screens"][i]["viewport"]["l"];
			cameraData.viewport.r = json["screens"][i]["viewport"]["r"];
			
			cameraData.display = json["screens"][i]["display"].AsInt;
			cameraData.screen = json["screens"][i]["screen"];		
		}
	}
	
	private CameraData spawnCameraData(string dataName) {
		Transform dataParent = GameObject.Find("Cameras/Camera Data").transform;

		// Destroy all old camera data
		for (int i = 0; i < dataParent.childCount; i++) {
			if (dataParent.GetChild(i).name.Contains(dataName)) {
				Destroy(dataParent.GetChild(i).gameObject);
			}
		}
		
		GameObject instancedData = Instantiate(cameraDataObj, dataParent) as GameObject;
		instancedData.name = dataName;
		
		return instancedData.GetComponent<CameraData>();
	}
}
