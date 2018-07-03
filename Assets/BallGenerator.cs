using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour {
    [SerializeField]
    GameObject SphereInstance;
    GameObject[] SphereList;
    Vector3[] SphereLocations;
    int numberSpheres;
    // Use this for initialization
    void Start () {
        LoadLevel(1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ResetBalls ()
    {
        for (int i = 0; i < numberSpheres; i++)
        {
            SphereList[i].transform.SetPositionAndRotation(SphereLocations[i], Quaternion.identity);
            SphereList[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    public void LoadLevel(int levelNumber)
    {
        switch (levelNumber)
        {
               case 1:
                {
                    numberSpheres = 4;
                    SphereLocations = new Vector3[4];
                    SphereList = new GameObject[4];
                    SphereLocations[0] = new Vector3(1, 10, 3);
                    SphereLocations[1] = new Vector3(2, 10, 4);
                    SphereLocations[2] = new Vector3(3, 10, 5);
                    SphereLocations[3] = new Vector3(0, 10, 2);
                    for (int sphere = 0; sphere < 4; sphere++)
                    {
                        SphereList[sphere] = Instantiate(SphereInstance, SphereLocations[sphere], Quaternion.identity);
                    }
                }
                break;
            default:
                break;
        }
    }
}
