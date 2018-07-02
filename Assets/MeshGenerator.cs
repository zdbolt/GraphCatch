using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeshGenerator : MonoBehaviour {
    // Use this for initialization
    [SerializeField]
    MeshFilter filter;
    [SerializeField]
    MeshCollider meshCollider;
    [SerializeField]
    Slider aSlider;
    [SerializeField]
    Slider bSlider;
    [SerializeField]
    Slider cSlider;
    [SerializeField]
    Slider dSlider;
    public float meshScaler =1f;
    void Start () {
        filter.mesh = GenerateMesh();
    }


    // Update is called once per frame
    void Update () {
		
	}

    public void updateMesh()
    {
        filter.mesh = GenerateMesh();
    }
    // Calculates the equation z = ((1/d)(x-a)^2 + (y-b)^2 + c))
    Mesh GenerateMesh()
    {

        Mesh mesh = new Mesh();
        List<Vector3> vectorList = new List<Vector3>();
        List<Vector3> normalsList = new List<Vector3>();
        List<int> trianglesList = new List<int>();
        int columnSize = 11;
        int i = 0;
        //adding vertex and shaders for top of curve(which ends up being the bottom for the player)
                for (float x = meshScaler * -5; x <= meshScaler * 5; x = x + meshScaler)
                    for (float y = meshScaler * -5; y <= meshScaler * 5; y = y + meshScaler)
                    {

                    float z = (x * x + y * y);
                vectorList.Add(new Vector3(x + aSlider.value, (1/dSlider.value)* (z + cSlider.value), y + bSlider.value));
                normalsList.Add(Vector3.Cross(new Vector3(0.0f, (y * 2), 1.0f), new Vector3(1.0f, (x * 2), 0.0f )));
                    }

        for (int x = -5; x < 5; x++)
        {
            for (int y = -5; y < 5; y++)
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
        for (float x = meshScaler * -5; x <= meshScaler * 5; x = x + meshScaler)
            for (float y = meshScaler * -5; y <= meshScaler * 5; y = y + meshScaler)
            
                //adding vertex and shaders for bottom of curve (which ends up being the top for the player)
                {
                    float z = (x * x + y * y);
                vectorList.Add(new Vector3(x + aSlider.value, (1 / dSlider.value) * (z + cSlider.value), y + bSlider.value));
                normalsList.Add(Vector3.Cross(new Vector3(1.0f, (x * 2), 0.0f), new Vector3(0.0f, (y * 2), 1.0f)));
                }
            

        for (int x = -5; x < 5; x++)
        {
            for (int y = -5; y < 5; y++)
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
