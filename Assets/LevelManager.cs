using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    [SerializeField]
    GameObject SphereInstance;
    [SerializeField]
    Text timerDisplay;
    [SerializeField]
    Text equationDisplay;
    [SerializeField]
    Slider[] sliders;
    GameObject[] SphereList;
    Vector3[] SphereLocations;
    Vector3[] SliderLocations;
    int numberSpheres;
    bool timerOn;
    int currentLevel;
    float levelTimeLimit;
    float timer;
    string[] equation;
    int equationLength;
    // Use this for initialization
    void Start () {
        LoadLevel(1);
	}
	
	// Update is called once per frame
	void Update () {
        if (timerOn)
        {
            //for (int i = 0; i < numberSpheres; i++)
            //    if (SphereList[i].transform.position.y < -14)
            //        ResetBalls();
            timer -= Time.deltaTime;

            timerDisplay.text = "Level Time: " + (Mathf.Round(timer*100)/100).ToString(); //couldn't find a better way of rounding to nearest 100th.
            if (timer < 0)
            {
                if (CheckWin(currentLevel))
                {
                    Debug.Log("Win!");
                    LoadLevel(currentLevel + 1);
                }
                else
                    ResetBalls();
            }
        }
    }

    public void ResetBalls ()
    {
        for (int i = 0; i < numberSpheres; i++)
        {
            SphereList[i].transform.SetPositionAndRotation(SphereLocations[i], Quaternion.identity);
            SphereList[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            SphereList[i].GetComponent<Rigidbody>().useGravity = false;
            SphereList[i].GetComponent<Rigidbody>().isKinematic = true;
        }
        timerOn = false;
        timer = levelTimeLimit;
        timerDisplay.text = "Level Time: " + timer.ToString();
    }

    public void LoadLevel(int levelNumber)
    {
        timerOn = false;
        switch (levelNumber)
        {
               case 1:
                {
                    numberSpheres = 4;
                    SphereLocations = new Vector3[numberSpheres];
                    SphereList = new GameObject[numberSpheres];
                    SphereLocations[0] = new Vector3(1, 10, 3);
                    SphereLocations[1] = new Vector3(2, 10, 4);
                    SphereLocations[2] = new Vector3(3, 10, 5);
                    SphereLocations[3] = new Vector3(0, 10, 2);
                    for (int sphere = 0; sphere < 4; sphere++)
                    {
                        SphereList[sphere] = Instantiate(SphereInstance, SphereLocations[sphere], Quaternion.identity);
                    }
                    currentLevel = 1;
                    levelTimeLimit = 3;
                    equationLength = 4;
                    SliderLocations = new Vector3[equationLength];
                    SliderLocations[0] = new Vector3(-176, 130, 0);
                    SliderLocations[1] = new Vector3(-19, 106, 0);
                    SliderLocations[2] = new Vector3(111, 130, 0);
                    SliderLocations[3] = new Vector3(219, 106, 0);
                    for (int i = 0; i<equationLength; i++)
                    {
                        sliders[i].transform.localPosition = SliderLocations[i];
                    }
                    equation = new string[equationLength];
                    equation[0] = "Z=(1/";
                    equation[1] = ")*((X-";
                    equation[2] = ")²+(Y-";
                    equation[3] = ")²)+";
                    UpdateEquation();
                    timer = levelTimeLimit;
                    timerDisplay.text = "Level Time: " + timer.ToString();
                }
                break;
                case 2:
                {
                    numberSpheres = 4;
                    SphereLocations = new Vector3[4];
                    SphereList = new GameObject[4];
                    SphereLocations[0] = new Vector3(1, 10, 7);
                    SphereLocations[1] = new Vector3(2, 10, 1);
                    SphereLocations[2] = new Vector3(3, 10, 8);
                    SphereLocations[3] = new Vector3(0, 10, 4);
                    for (int sphere = 0; sphere < 4; sphere++)
                    {
                        SphereList[sphere] = Instantiate(SphereInstance, SphereLocations[sphere], Quaternion.identity);
                    }
                    currentLevel = 2;
                    levelTimeLimit = 3;
                }
                break;
            default:
                break;
        }
    }

    public void StartLevel()
    {
        for (int i = 0; i< numberSpheres;i++)
        {
            SphereList[i].GetComponent<Rigidbody>().useGravity = true;
            SphereList[i].GetComponent<Rigidbody>().isKinematic = false;
            timerOn = true;
        }
    }

    public void UpdateEquation()
    {
        equationDisplay.text = "";
        for (int i = 0; i<equationLength; i++)
        {
            equationDisplay.text = equationDisplay.text + equation[i] + sliders[i].value.ToString();
        }
    }

    public bool CheckWin(int levelNumber)
    {
        bool winning = true;
        switch (levelNumber)
        {
            case 1:
                {                 
                    for (int i = 0; i < numberSpheres; i++)
                        if (SphereList[i].transform.position.y < -10)
                            winning = false;
                }
                return winning;
            default:
                return false;
        }
    }
}
