using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class OutputJsonScript : MonoBehaviour
{
	private List<CameraData> cameraDatas = new List<CameraData>();

	public void WriteNewJSON () 
	{
		CameraData cd = new CameraData();

		cd.screen = "Left";
		cd.display = 1;
		cd.viewport.tl = new Vector2(-0.5f, 0.5f);
		cd.viewport.tr = new Vector2(0.5f, 0.5f);
		cd.viewport.bl = new Vector2(-0.5f, -0.5f);
		cd.viewport.br = new Vector2(0.5f, -0.5f);
		cameraDatas.Add(cd);

		CameraData cd1 = new CameraData();
		cd1.screen = "Front";
		cd1.display = 0;
		cd1.viewport.tl = new Vector2(-0.5f, 0.5f);
		cd1.viewport.tr = new Vector2(0.5f, 0.5f);
		cd1.viewport.bl = new Vector2(-0.5f, -0.5f);
		cd1.viewport.br = new Vector2(0.5f, -0.5f);
		cameraDatas.Add(cd1);

		CameraData cd2 = new CameraData();
		cd2.screen = "Right";
		cd2.display = 2;
		cd2.viewport.tl = new Vector2(-0.5f, 0.5f);
		cd2.viewport.tr = new Vector2(0.5f, 0.5f);
		cd2.viewport.bl = new Vector2(-0.5f, -0.5f);
		cd2.viewport.br = new Vector2(0.5f, -0.5f);
		cameraDatas.Add(cd2);

		CameraData cd3 = new CameraData();
		cd3.screen = "Floor";
		cd3.display = 3;
		cd3.viewport.tl = new Vector2(0, 0);
		cd3.viewport.tr = new Vector2(0, 0);
		cd3.viewport.bl = new Vector2(0, 0);
		cd3.viewport.br = new Vector2(0, 0);
		cameraDatas.Add(cd3);

		string jsonString = "{\"screens\":[";

		for (int i = 0; i < cameraDatas.Count - 1; i++) 
		{
			jsonString += JsonUtility.ToJson(cameraDatas[i]);
			jsonString += ",";
		}

		jsonString += JsonUtility.ToJson(cameraDatas[cameraDatas.Count - 1]);
		jsonString += "]}";

		System.IO.File.WriteAllText(Application.dataPath + "/JsonFiles/config-different.json", jsonString);
	}
		
}
