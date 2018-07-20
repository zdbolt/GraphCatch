using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    GameObject canvasObject;
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
    GameObject[] sphereLines;
    bool timerOn;
    int currentLevel;
    float levelTimeLimit;
    float timer;
    string[] equation;
    int equationLength; //this determines how many sliders are used.  each time the equation is broken up, a slider is inserted to fill the gap
    float[][] sliderMaps; //maps slider integars into values the equation can hold, such as (0,1,2,3) to (-1,-.5,-.33,-.2)
    float[] sliderValues; //takes the value from the sliderMap if there is one, or from the slider if there isn't one
    // Use this for initialization
    void Start()
    {
        LoadLevel(1);
    }

    public void UpdateMesh()
    {
        filter.mesh = GenerateMesh();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerOn)
        {
            timer -= Time.deltaTime;

            timerDisplay.text = "Level Time: " + (Mathf.Round(timer * 100) / 100).ToString(); //couldn't find a better way of rounding to nearest 100th.
            if (timer < 0)
            {
                if (CheckWin(currentLevel))
                {
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
        canvasObject.transform.LookAt(Camera.main.transform);
        canvasObject.transform.rotation = Quaternion.Euler(0, canvasObject.transform.eulerAngles.y+180, 0);
    }

    public void ResetBalls()
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

        for (int sphere = 0; sphere < numberSpheres; sphere++) //redraw lines
        {
            var sphereLineEnd = new Vector3(SphereLocations[sphere].x, 0, SphereLocations[sphere].z);
            sphereLines[sphere] = DrawLine(SphereLocations[sphere], sphereLineEnd);
        }
    }

    public void LoadLevel(int levelNumber)
    {
        timerOn = false;
        switch (levelNumber)
        {
            case 1:
                {
                    numberSpheres = 4; //this could all be xml
                    SphereLocations = new Vector3[numberSpheres];
                    SphereList = new GameObject[numberSpheres];
                    SphereLocations[0] = new Vector3(1.01f, 15, 3.01f);
                    SphereLocations[1] = new Vector3(2.01f, 15, 4.01f);
                    SphereLocations[2] = new Vector3(3.01f, 15, 5.01f);
                    SphereLocations[3] = new Vector3(0.01f, 15, 2.01f);
                    sphereLines = new GameObject[numberSpheres];
                    DrawBalls();
                    currentLevel = 1;
                    levelTimeLimit = 6;
                    equationLength = 4;
                    SliderLocations = new Vector3[equationLength];
                    sliderMaps = new float[equationLength][];
                    SliderLocations[0] = new Vector3(-176, 130, 0);
                    sliderMaps[0] = new float[10] { -1, -1 / 2f, -1 / 3f, -1 / 4f, -1 / 5f, 1 / 5f, 1 / 4f, 1 / 3f, 1 / 2f, 1 };
                    SliderLocations[1] = new Vector3(-19, 106, 0);
                    SliderLocations[2] = new Vector3(111, 130, 0);
                    SliderLocations[3] = new Vector3(219, 106, 0);
                    for (int i = 0; i < equationLength; i++)
                    {
                        sliders[i].transform.localPosition = SliderLocations[i];
                    }
                    equation = new string[equationLength];
                    equation[0] = "Z=";
                    equation[1] = "*((X-";
                    equation[2] = ")²+(Y-";
                    equation[3] = ")²)+";
                    timer = levelTimeLimit;
                    timerDisplay.text = "Level Time: " + timer.ToString();
                    UpdateMesh();
                    UpdateEquation();
                }
                break;
            case 2:
                {
                    numberSpheres = 4;
                    SphereLocations = new Vector3[4];
                    SphereList = new GameObject[4];
                    SphereLocations[0] = new Vector3(1.01f, 10, 7.01f);
                    SphereLocations[1] = new Vector3(2.01f, 10, 1.01f);
                    SphereLocations[2] = new Vector3(3.01f, 10, 8.01f);
                    SphereLocations[3] = new Vector3(0.01f, 10, 4.01f);
                    DrawBalls();
                    currentLevel = 2;
                    levelTimeLimit = 6;
                    timer = levelTimeLimit;
                    timerDisplay.text = "Level Time: " + timer.ToString();
                    UpdateMesh();
                    UpdateEquation();
                }
                break;
            default:
                break;
        }

    }

    public void StartLevel()
    {
        for (int i = 0; i < numberSpheres; i++)
        {
            SphereList[i].GetComponent<Rigidbody>().useGravity = true;
            SphereList[i].GetComponent<Rigidbody>().isKinematic = false;
            timerOn = true;
            Destroy(sphereLines[i]);
        }
    }

    public void UpdateEquation()
    {
        if (currentLevel < 3)
        {
            if (sliders[1].value < 0)
                equation[1] = "*((X";
            else
                equation[1] = "*((X+";

            if (sliders[2].value < 0)
                equation[2] = ")²+(Y";
            else
                equation[2] = ")²+(Y+";

            if (sliders[3].value < 0)
                equation[3] = ")²)";
            else
                equation[3] = ")²)+";
        }
        equationDisplay.text = "";
        for (int i = 0; i < equationLength; i++)
        {
            float value = sliderValues[i];
            value = Mathf.Round(value * 100f) / 100f; //round this to 3 decimal places
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
        List<Vector2> uvList = new List<Vector2>();
        int columnSize = (int)(8 / qualityOfMesh) + 1;
        int i = 0; //carries on through generation of both sides of mesh, do not use for local counter variable
        sliderValues = new float[equationLength];

        for (int j = 0; j < equationLength; j++) //get slider values from the map if it exists, or directly from the slider if no map exists
        {
            if (sliderMaps != null && sliderMaps[j] != null)
                sliderValues[j] = sliderMaps[j][(int)sliders[j].value];
            else
                sliderValues[j] = sliders[j].value;
        }

        //adding vertex and shaders for top of curve(which ends up being the bottom for the player)
        for (float x = meshScaler * -4; x <= meshScaler * 4; x = x + meshScaler * qualityOfMesh) //set mesh vertices, normals, and uvs for top of curve
        {
            for (float y = meshScaler * -4; y <= meshScaler * 4; y = y + meshScaler * qualityOfMesh)
            {
                float z = (x * x + y * y);
                float xUnityLocation; //since I forced Z to be up, these section will translate from the equation to the unity values
                float yUnityLocation;
                float zUnityLocation;

                xUnityLocation = x - sliderValues[1];
                yUnityLocation = sliderValues[0] * z;//first calculates the 1/a value at the start of the whole equation
                yUnityLocation = yUnityLocation + sliderValues[3]; //then adds d to it to incorporate the last of the equation
                zUnityLocation = y - sliderValues[2];

                vectorList.Add(new Vector3(xUnityLocation, yUnityLocation, zUnityLocation));
                Vector3 tangentY = new Vector3(0.0f, (2 * sliderValues[0] * (y - sliderValues[2])), 1.0f * sliderValues[0]);
                Vector3 tangentX = new Vector3(1.0f * sliderValues[0], (2 * sliderValues[0] * (x - sliderValues[1])), 0.0f);
                Vector3 normal;
                if (sliderValues[0] > 0)
                    normal = Vector3.Cross(tangentY, tangentX);
                else
                    normal = -Vector3.Cross(tangentY, tangentX);
                normalsList.Add(normal);
                uvList.Add(new Vector2(x, y));
                //Debug.DrawRay(new Vector3(xUnityLocation, yUnityLocation, zUnityLocation), normal, Color.black, 2); draws a debug ray for the top normal
            }
        }

        for (float x = -4; x < 4; x = x + qualityOfMesh) //draw top side triangles 
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
        for (float x = meshScaler * -4; x <= meshScaler * 4; x = x + meshScaler * qualityOfMesh) //set mesh vertices, normals, and uvs for top of curve
        {
            for (float y = meshScaler * -4; y <= meshScaler * 4; y = y + meshScaler * qualityOfMesh)
            {
                //adding vertex and normals for bottom of curve (which ends up being the top for the player)
                float z = (x * x + y * y);
                float xUnityLocation; //since I forced Z to be up, these section will translate to the unity values
                float yUnityLocation;
                float zUnityLocation;

                xUnityLocation = x - sliderValues[1];
                yUnityLocation = sliderValues[0] * z;//first calculates the 1/a value at the start of the whole equation
                yUnityLocation = yUnityLocation + sliderValues[3]; //then adds d to it to incorporate the last of the equation
                zUnityLocation = y - sliderValues[2];

                vectorList.Add(new Vector3(xUnityLocation, yUnityLocation, zUnityLocation));
                Vector3 tangentY = new Vector3(0.0f, (2 * sliderValues[0] * (y - sliderValues[2])), 1.0f * sliderValues[0]);
                Vector3 tangentX = new Vector3(1.0f * sliderValues[0], (2 * sliderValues[0] * (x - sliderValues[1])), 0.0f);
                Vector3 normal;
                if (sliderValues[0] > 0)
                {
                    normal = -Vector3.Cross(tangentY, tangentX);
                }
                else
                    normal = Vector3.Cross(tangentY, tangentX);
                normalsList.Add(normal);

                uvList.Add(new Vector2(x, y));
                //Debug.DrawRay(new Vector3(xUnityLocation, yUnityLocation, zUnityLocation), normal, Color.red, 2); draws a debug ray for the bottom normal
            }
        }

        for (float x = -4; x < 4; x = x + qualityOfMesh) //draw bottom side triangles
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
        mesh.SetUVs(0, uvList);
        meshCollider.sharedMesh = mesh;

        return mesh;
    }


    GameObject DrawLine(Vector3 start, Vector3 end)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        //lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.startColor = Color.red;
        lr.startWidth = 0.1f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        return myLine;
        //GameObject.Destroy(myLine);
    }

    void DrawBalls()
    {
        for (int sphere = 0; sphere < numberSpheres; sphere++)
        {
            SphereList[sphere] = Instantiate(SphereInstance, SphereLocations[sphere], Quaternion.identity);
            var sphereLineEnd = new Vector3(SphereLocations[sphere].x, 0, SphereLocations[sphere].z);
            sphereLines[sphere] = DrawLine(SphereLocations[sphere], sphereLineEnd);
        }
    }
}

