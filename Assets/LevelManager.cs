using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    [SerializeField]
    MeshFilter filter;
    [SerializeField]
    MeshCollider meshCollider;
    [SerializeField]
    Slider qualitySlider;
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
    float qualityOfMesh; //smaller is better, increases or decreases the number of mesh points by powers of 2
    public float meshScaler = 1f;
    int numberSpheres;
    bool timerOn;
    int currentLevel;
    float levelTimeLimit;
    float timer;
    string[] equation;
    int equationLength;
    float[][] sliderMaps;
    // Use this for initialization
    void Start () {
        LoadLevel(1);
        updateMesh();
    }

    public void updateMesh()
    {
        filter.mesh = GenerateMesh();
    }

    // Update is called once per frame
    void Update () {
        if (timerOn)
        {
            timer -= Time.deltaTime;

            timerDisplay.text = "Level Time: " + (Mathf.Round(timer*100)/100).ToString(); //couldn't find a better way of rounding to nearest 100th.
            if (timer < 0)
            {
                if (CheckWin(currentLevel))
                {
                    Debug.Log("Win!");
                    for (int i = 0; i < numberSpheres; i++)
                    {
                        Destroy(SphereList[i]);
                    }
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
                    SphereLocations[0] = new Vector3(1, 15, 3);
                    SphereLocations[1] = new Vector3(2, 15, 4);
                    SphereLocations[2] = new Vector3(3, 15, 5);
                    SphereLocations[3] = new Vector3(0, 15, 2);
                    for (int sphere = 0; sphere < 4; sphere++)
                    {
                        SphereList[sphere] = Instantiate(SphereInstance, SphereLocations[sphere], Quaternion.identity);
                    }
                    currentLevel = 1;
                    levelTimeLimit = 3;
                    equationLength = 4;
                    SliderLocations = new Vector3[equationLength];
                    sliderMaps = new float[equationLength][];
                    SliderLocations[0] = new Vector3(-176, 130, 0);
                    sliderMaps[0] = new float[10] { -1, -1/2f, -1/3f, -1/4f, -1/5f, 1/5f, 1/4f, 1/3f, 1/2f, 1 };
                    SliderLocations[1] = new Vector3(-19, 106, 0);
                    SliderLocations[2] = new Vector3(111, 130, 0);
                    SliderLocations[3] = new Vector3(219, 106, 0);
                    for (int i = 0; i<equationLength; i++)
                    {
                        sliders[i].transform.localPosition = SliderLocations[i];
                    }
                    equation = new string[equationLength];
                    equation[0] = "Z=";
                    equation[1] = "*((X-";
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
                    UpdateEquation();
                    timer = levelTimeLimit;
                    timerDisplay.text = "Level Time: " + timer.ToString();
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
        if (currentLevel==1)
        {
            if (sliders[1].value < 0 )
                equation[1] = ")*((X";
            else
                equation[1] = ")*((X+";

            if (sliders[2].value < 0 )
                equation[2] = ")²+(Y";
            else
                equation[2] = ")²+(Y+";
        }
        equationDisplay.text = "";
        for (int i = 0; i<equationLength; i++)
        {
            float value;
            if (sliderMaps[i] != null)
            {
                value = sliderMaps[i][(int)sliders[i].value];
                                
            }
            else
            {
                value = sliders[i].value;
            }
            value = Mathf.Round(value * 100f) / 100f;
            equationDisplay.text = equationDisplay.text + equation[i] + value.ToString();
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

    Mesh GenerateMesh()
    {
        qualityOfMesh = Mathf.Pow(2f, -qualitySlider.value); //this creates 5 possible values for the quality.  0.125, 0.25, 0.5, 1, and 2
        Mesh mesh = new Mesh();
        List<Vector3> vectorList = new List<Vector3>();
        List<Vector3> normalsList = new List<Vector3>();
        List<int> trianglesList = new List<int>();
        int columnSize = (int)(8 / qualityOfMesh) + 1;
        int i = 0;

        //adding vertex and shaders for top of curve(which ends up being the bottom for the player)
        for (float x = meshScaler * -4; x <= meshScaler * 4; x = x + meshScaler * qualityOfMesh)
        {
            for (float y = meshScaler * -4; y <= meshScaler * 4; y = y + meshScaler * qualityOfMesh)
            {
                float z = (x * x + y * y);
                float xUnityLocation; //since I forced Z to be up, these section will translate to the unity values
                float yUnityLocation;
                float zUnityLocation;

                if (sliderMaps != null && sliderMaps[1] != null)
                    xUnityLocation = x + sliderMaps[1][(int)sliders[1].value];
                else
                    xUnityLocation = x + sliders[1].value;

                if (sliderMaps != null && sliderMaps[0] != null)  //first calculates the 1/a value at the start of the whole equation
                    yUnityLocation = sliderMaps[0][(int)sliders[0].value] * z;
                else
                    yUnityLocation = sliders[0].value * z;

                if (sliderMaps != null && sliderMaps[3] != null) //then adds d to it to incorporate the last of the equation
                    yUnityLocation = yUnityLocation + sliderMaps[3][(int)sliders[3].value];
                else
                    yUnityLocation = yUnityLocation + sliders[3].value;

                if (sliderMaps != null && sliderMaps[2] != null)
                    zUnityLocation = y + sliderMaps[2][(int)sliders[2].value];
                else
                    zUnityLocation = y + sliders[2].value;


                vectorList.Add(new Vector3(xUnityLocation, yUnityLocation, zUnityLocation));
                Vector3 tangentX = new Vector3(0.0f, (y * 2), 1.0f);
                Vector3 tangentY = new Vector3(1.0f, (x * 2), 0.0f);
                Vector3 normal = -Vector3.Cross(tangentX, tangentY);
                normalsList.Add(normal);
            }
        }

        for (float x = -4; x < 4; x = x + qualityOfMesh)
        {
            for (float y = -4; y < 4; y = y + qualityOfMesh)
            {
                trianglesList.Add(i);
                trianglesList.Add(i + 1 + columnSize);
                trianglesList.Add(i + columnSize);

                trianglesList.Add(i);
                trianglesList.Add(i + 1);
                trianglesList.Add(i + 1 + columnSize);

                //trianglesList.Add(i + 1 + columnSize);
                //trianglesList.Add(i);
                //trianglesList.Add(i + columnSize);

                //trianglesList.Add(i + 1);
                //trianglesList.Add(i);
                //trianglesList.Add(i + 1 + columnSize);

                i++;
            }
            i++; //skip the top x value that won't have it's triangle drawn
        }
        i = i + columnSize;
        for (float x = meshScaler * -4; x <= meshScaler * 4; x = x + meshScaler * qualityOfMesh)
        {
            for (float y = meshScaler * -4; y <= meshScaler * 4; y = y + meshScaler * qualityOfMesh)
            {
                //adding vertex and normals for bottom of curve (which ends up being the top for the player)
                float z = (x * x + y * y);
                float xUnityLocation; //since I forced Z to be up, these section will translate to the unity values
                float yUnityLocation;
                float zUnityLocation;

                if (sliderMaps != null && sliderMaps[1] != null)
                    xUnityLocation = x + sliderMaps[1][(int)sliders[1].value];
                else
                    xUnityLocation = x + sliders[1].value;

                if (sliderMaps != null && sliderMaps[0] != null)  //first calculates the 1/a value at the start of the whole equation
                    yUnityLocation = sliderMaps[0][(int)sliders[0].value] * z;
                else
                    yUnityLocation = sliders[0].value * z;

                if (sliderMaps != null && sliderMaps[3] != null) //then adds d to it to incorporate the last of the equation
                    yUnityLocation = yUnityLocation + sliderMaps[3][(int)sliders[3].value];
                else
                    yUnityLocation = yUnityLocation + sliders[3].value;

                if (sliderMaps != null && sliderMaps[2] != null)
                    zUnityLocation = y + sliderMaps[2][(int)sliders[2].value];
                else
                    zUnityLocation = y + sliders[2].value;


                vectorList.Add(new Vector3(xUnityLocation, yUnityLocation, zUnityLocation));
                Vector3 tangentX = new Vector3(0.0f, (y * 2), 1.0f);
                Vector3 tangentY = new Vector3(1.0f, (x * 2), 0.0f);
                Vector3 normal = Vector3.Cross(tangentX, tangentY);
                normalsList.Add(normal);
                //normalsList.Add(Vector3.Cross(new Vector3(1.0f, (x * 2), 0.0f), new Vector3(0.0f, (y * 2), 1.0f)));
            }
        }

        for (float x = -4; x < 4; x = x + qualityOfMesh)
        {
            for (float y = -4; y < 4; y = y + qualityOfMesh)
            {
                //trianglesList.Add(i);
                //trianglesList.Add(i + 1 + columnSize);
                //trianglesList.Add(i + columnSize);

                //trianglesList.Add(i);
                //trianglesList.Add(i + 1);
                //trianglesList.Add(i + 1 + columnSize);

                trianglesList.Add(i + 1 + columnSize);
                trianglesList.Add(i);
                trianglesList.Add(i + columnSize);

                trianglesList.Add(i + 1);
                trianglesList.Add(i);
                trianglesList.Add(i + 1 + columnSize);

                i++;
            }
            i++; //skip the top x value that won't have it's triangle drawn
        }

        mesh.SetVertices(vectorList);
        mesh.SetNormals(normalsList);
        mesh.SetTriangles(trianglesList, 0);
        meshCollider.sharedMesh = mesh;

        return mesh;
    }
}
